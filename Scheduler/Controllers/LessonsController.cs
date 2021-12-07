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
    public class LessonsController : Controller
    {
        private static DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private readonly ILessonRepository lessonRepository;

        public LessonsController(ILessonRepository lessonRepository)
        {
            this.lessonRepository = lessonRepository;
        }

        [Authorize(Roles = "Student")]
        [HttpGet("lessons/my")]
        public async Task<LessonsResponse> MyLessons(long timestampStart, long timestampEnd)
        {
            if (!int.TryParse(User.Claims.First(claim => claim.Type == "Id").Value, out int id))
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                return new LessonsResponse("Invalid token id");
            }

            Student student = new()
            {
                Id = id
            };

            IEnumerable<Lesson> lessons = await lessonRepository.LessonsForStudent(student, MillisToTime(timestampStart), MillisToTime(timestampEnd));

            return new LessonsResponse
            {
                IsOk = true,
                Lessons = lessons.Select(lesson => new LessonObject
                {
                    Id = lesson.Id,
                    GroupName = lesson.Group.Name,
                    LessonTime = lesson.LessonTime,
                    Teacher = new TeacherObject
                    {
                        Id = lesson.Teacher.Id,
                        FirstName = lesson.Teacher.FirstName,
                        SecondName = lesson.Teacher.SecondName,
                        Degree = lesson.Teacher.Degree.ToString(),
                        Position = lesson.Teacher.Position,
                        CareerStarted = lesson.Teacher.CareerStarted
                    },
                    Workload = new WorkloadObject
                    {
                        Id = lesson.Workload.Id,
                        Description = lesson.Workload.Description,
                        Subject = new SubjectObject
                        {
                            Id = lesson.Workload.Subject.Id,
                            Name = lesson.Workload.Subject.Name
                        },
                        Hours = lesson.Workload.Hours
                    }
                })
            };
        }

        [Authorize(Roles = "Teacher")]
        [HttpGet("lessons")]
        public async Task<LessonsResponse> Lessons(long start, long end)
        {
            IEnumerable<Lesson> lessons = await lessonRepository.GetLessons(MillisToTime(start), MillisToTime(end));

            return new LessonsResponse
            {
                IsOk = true,
                Lessons = lessons.Select(lesson => new LessonObject
                {
                    Id = lesson.Id,
                    GroupName = lesson.Group.Name,
                    LessonTime = lesson.LessonTime,
                    Teacher = new TeacherObject
                    {
                        Id = lesson.Teacher.Id,
                        FirstName = lesson.Teacher.FirstName,
                        SecondName = lesson.Teacher.SecondName,
                        Degree = lesson.Teacher.Degree.ToString(),
                        Position = lesson.Teacher.Position,
                        CareerStarted = lesson.Teacher.CareerStarted
                    },
                    Workload = new WorkloadObject
                    {
                        Id = lesson.Workload.Id,
                        Description = lesson.Workload.Description,
                        Subject = new SubjectObject
                        {
                            Id = lesson.Workload.Subject.Id,
                            Name = lesson.Workload.Subject.Name
                        },
                        Hours = lesson.Workload.Hours
                    }
                })
            };
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost("lessons/create")]
        public async Task<CreateLessonResponse> CreateLesson([FromBody] CreateLessonRequest request)
        {
            if (!int.TryParse(User.Claims.First(claim => claim.Type == "Id").Value, out int id))
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                return new CreateLessonResponse("Invalid token id");
            }

            Lesson lesson = new()
            {
                TeacherId = id,
                GroupId = request.GroupId,
                WorkloadId = request.WorkloadId,
                LessonTime = MillisToTime(request.LessonTime)
            };

            try
            {
                await lessonRepository.AddLesson(lesson);
                return new CreateLessonResponse
                {
                    IsOk = true,
                    Id = lesson.Id,
                    GroupName = lesson.Group.Name,
                    LessonTime = lesson.LessonTime,
                    Teacher = new TeacherObject
                    {
                        Id = lesson.Teacher.Id,
                        FirstName = lesson.Teacher.FirstName,
                        SecondName = lesson.Teacher.SecondName,
                        Degree = lesson.Teacher.Degree.ToString(),
                        Position = lesson.Teacher.Position,
                        CareerStarted = lesson.Teacher.CareerStarted
                    },
                    Workload = new WorkloadObject
                    {
                        Id = lesson.Workload.Id,
                        Description = lesson.Workload.Description,
                        Subject = new SubjectObject
                        {
                            Id = lesson.Workload.Subject.Id,
                            Name = lesson.Workload.Subject.Name
                        },
                        Hours = lesson.Workload.Hours
                    }
                };
            }
            catch (RepositoryException)
            {
                return new CreateLessonResponse("Error while processing request");
            }
        }


        private static DateTime MillisToTime(long millis)
        {
            return epoch.AddMilliseconds(millis);
        }
    }
}
