using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Core.Exceptions;
using Scheduler.Core.Objects;
using Scheduler.Core.Requests;
using Scheduler.Core.Responses;
using Scheduler.Data;
using Scheduler.Inetrfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Controllers
{
    [ApiController]
    public class GroupsController : Controller
    {
        private readonly IGroupRepository groupRepository;
        private readonly IStudentRepository studentRepository;
        private readonly IWorkloadRepository workloadRepository;

        public GroupsController(IGroupRepository groupRepository, IStudentRepository studentRepository, IWorkloadRepository workloadRepository)
        {
            this.groupRepository = groupRepository;
            this.studentRepository = studentRepository;
            this.workloadRepository = workloadRepository;
        }

        [Authorize(Roles = "Teacher")]
        [HttpGet("groups")]
        public async Task<GroupListResponse> GetGroups()
        {
            var groups = await groupRepository.GetGroupsSorted();

            return new GroupListResponse
            {
                IsOk = true,
                Groups = groups.Select(g => new GroupObject
                {
                    Id = g.Id,
                    Name = g.Name
                })
            };
        }

        [Authorize(Roles = "Teacher")]
        [HttpGet("groups/{id}")]
        public async Task<GroupInfoResponse> GetGroupInfo(int id)
        {
            var group = await groupRepository.GetGroupById(id);

            if (group == null)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                return new("Group with given id does not exists");
            }

            return new()
            {
                IsOk = true,
                Id = group.Id,
                Name = group.Name,
                Students = group.Students.Select(s => new StudentObject()
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    SecondName = s.SecondName,
                    GroupName = s.Group?.Name
                }),
                Workloads = group.Workloads.Select(w => new WorkloadObject()
                {
                    Id = w.Id,
                    Description = w.Description,
                    Subject = new SubjectObject
                    {
                        Id = w.Subject.Id,
                        Name = w.Subject.Name
                    },
                    Hours = w.Hours
                })
            };
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost("groups/create")]
        public async Task<CreateGroupResponse> CreateGroup([FromBody] CreateGroupRequest request)
        {
            Group group = new()
            {
                Name = request.Name
            };

            try
            {
                await groupRepository.AddGroup(group);
                return new CreateGroupResponse
                {
                    IsOk = true,
                    Id = group.Id,
                    Name = group.Name
                };
            }
            catch (RepositoryException)
            {
                return new CreateGroupResponse("Error while creating a group");
            }
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost("groups/rename")]
        public async Task<RenameGroupResponse> RenameGroup([FromBody] RenameGroupRequest request)
        {
            Group group = await groupRepository.GetGroupById(request.Id);

            if (group == null)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                return new RenameGroupResponse("Group with provided id was not found");
            }

            group.Name = request.Name;
            await groupRepository.UpdateGroup(group);
            return new RenameGroupResponse
            {
                IsOk = true,
                Id = group.Id,
                Name = group.Name
            };
        }

        [Authorize(Roles = "Teacher")]
        [HttpGet("group/{id}/members")]
        public async Task<GroupMembersResponse> GetGroupMembers(int id)
        {
            Group group = await groupRepository.GetGroupById(id);

            if (group == null)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                return new GroupMembersResponse("Group with provided id was not found");
            }

            IEnumerable<StudentObject> objects = group.Students.Select(st => new StudentObject
            {
                Id = st.Id,
                FirstName = st.FirstName,
                SecondName = st.SecondName,
                GroupName = group.Name
            });

            return new GroupMembersResponse
            {
                IsOk = true,
                Students = objects
            };
        }

        [Authorize(Roles = "Teacher")]
        [HttpGet("group/{id}/workloads")]
        public async Task<WorkloadsResponse> GetGroupWorkloads(int id)
        {
            Group group = await groupRepository.GetGroupById(id);

            if (group == null)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                return new WorkloadsResponse("Group with provided id was not found");
            }

            IEnumerable<WorkloadObject> objects = group.Workloads.Select(w => new WorkloadObject
            {
                Id = w.Id,
                Description = w.Description,
                Subject = new()
                {
                    Id = w.Subject.Id,
                    Name = w.Subject.Name
                },
                Hours = w.Hours
            });

            return new WorkloadsResponse
            {
                IsOk = true,
                Workloads = objects
            };
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost("groups/addStudent")]
        public async Task<SetStudentGroupResponse> AddStudent([FromBody] SetStudentGroupRequest request)
        {
            Group group = null;

            if (request.GroupId.HasValue)
            {
                group = await groupRepository.GetGroupById(request.GroupId.Value);

                if (group == null)
                {
                    HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                    return new SetStudentGroupResponse("Group with given id not found");
                }
            }

            Student student = await studentRepository.GetStudentById(request.StudentId);

            if (student == null)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                return new SetStudentGroupResponse("Student with given id not found");
            }

            student.GroupId = group?.Id;
            await studentRepository.UpdateStudent(student);

            return new SetStudentGroupResponse
            {
                IsOk = true,
                FirstName = student.FirstName,
                SecondName = student.SecondName,
                Id = student.Id,
                GroupId = group?.Id,
                GroupName = group?.Name
            };
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost("groups/addWorkload")]
        public async Task<SetWorkloadGroupResponse> AddWorkload([FromBody] SetWorkloadGroupRequest request)
        {
            Group group = await groupRepository.GetGroupById(request.GroupId);

            if (group == null)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                return new SetWorkloadGroupResponse("Group with given id not found");
            }

            Workload workload = await workloadRepository.GetWorkloadById(request.WorkloadId);

            if (workload == null)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                return new SetWorkloadGroupResponse("Workload with given id not found");
            }

            if (!group.Workloads.Any(w => w.Id == workload.Id))
            {
                group.Workloads.Add(workload);
                await groupRepository.UpdateGroup(group);
            }

            return new SetWorkloadGroupResponse
            {
                IsOk = true,
                GroupId = group.Id,
                GroupName = group.Name,
                WorkloadId = workload.Id,
                WorkloadDescription = workload.Description,
                Subject = new()
                {
                    Id = workload.Subject.Id,
                    Name = workload.Subject.Name
                },
                Hours = workload.Hours
            };
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost("groups/removeWorkload")]
        public async Task<SetWorkloadGroupResponse> RemoveWorkload([FromBody] SetWorkloadGroupRequest request)
        {
            Group group = await groupRepository.GetGroupById(request.GroupId);

            if (group == null)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                return new SetWorkloadGroupResponse("Group with given id not found");
            }

            Workload workload = await workloadRepository.GetWorkloadById(request.WorkloadId);

            if (workload == null)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                return new SetWorkloadGroupResponse("Workload with given id not found");
            }

            group.Workloads.Remove(workload);

            try
            {
                await groupRepository.UpdateGroup(group);
            }
            catch (Exception)
            {

            }
            return new SetWorkloadGroupResponse
            {
                IsOk = true,
                GroupId = group.Id,
                GroupName = group.Name,
                WorkloadId = workload.Id,
                WorkloadDescription = workload.Description,
                Subject = new()
                {
                    Id = workload.Subject.Id,
                    Name = workload.Subject.Name
                },
                Hours = workload.Hours
            };
        }
    }
}
