using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class WorkloadsController : Controller
    {
        private static readonly int itemsOnPage = 20;
        private readonly IWorkloadRepository workloadRepository;

        public WorkloadsController(IWorkloadRepository workloadRepository)
        {
            this.workloadRepository = workloadRepository;
        }

        [Authorize(Roles = "Teacher")]
        [HttpGet("workloads")]
        public async Task<WorkloadsResponse> Workloads()
        {
            IEnumerable<Workload> workloads = await workloadRepository.GetWorkloads();

            return new()
            {
                IsOk = true,
                Workloads = workloads.Select(w => new WorkloadObject
                {
                    Id = w.Id,
                    Description = w.Description,
                    Hours = w.Hours,
                    Subject = new SubjectObject
                    {
                        Id = w.Subject.Id,
                        Name = w.Subject.Name
                    }
                })
            };
        }

        [Authorize(Roles = "Teacher")]
        [HttpGet("workloads/page/{page}")]
        public async Task<WorkloadListResponse> Workloads(int page)
        {
            int count = await workloadRepository.WorkloadsCount();

            if (count == 0)
            {
                return new()
                {
                    IsOk = true,
                    Page = 0,
                    MaxPage = 0,
                    Workloads = Enumerable.Empty<WorkloadObject>()
                };
            }

            int maxPage = (int)Math.Ceiling((double)count / itemsOnPage);

            if (page > maxPage || page < 1)
            {
                return new("Page is out of bounds")
                {
                    Page = page,
                    MaxPage = maxPage
                };
            }

            IEnumerable<Workload> workloads = await workloadRepository.TakeWorkloads(itemsOnPage, (page - 1) * itemsOnPage);
            return new()
            {
                IsOk = true,
                Page = page,
                MaxPage = maxPage,
                Workloads = workloads.Select(w => new WorkloadObject
                {
                    Id = w.Id,
                    Description = w.Description,
                    Hours = w.Hours,
                    Subject = new SubjectObject
                    {
                        Id = w.Subject.Id,
                        Name = w.Subject.Name
                    }
                })
            };
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost("workloads/create")]
        public async Task<CreateWorkloadResponse> CreateWorkload([FromBody] CreateWorkloadRequest request)
        {
            Workload workload = new()
            {
                Description = request.Description,
                Hours = request.Hours,
                SubjectId = request.SubjectId
            };

            await workloadRepository.AddWorkload(workload);
            return new CreateWorkloadResponse
            {
                IsOk = true,
                Description = workload.Description,
                Id = workload.Id,
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
