using Microsoft.AspNetCore.Authorization;
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
    public class SubjectsController : Controller
    {
        private static readonly int itemsOnPage = 20;
        private readonly ISubjectRepository subjectRepository;

        public SubjectsController(ISubjectRepository subjectRepository)
        {
            this.subjectRepository = subjectRepository;
        }


        [Authorize(Roles = "Teacher")]
        [HttpGet("subjects")]
        public async Task<SubjectsResponse> Subjects()
        {
            IEnumerable<Subject> subjects = await subjectRepository.GetSubjects();
            return new()
            {
                IsOk = true,
                Subjects = subjects.Select(s => new SubjectObject
                {
                    Id = s.Id,
                    Name = s.Name
                })
            };
        }


        [Authorize(Roles = "Teacher")]
        [HttpGet("subjects/{page}")]
        public async Task<SubjectsListResponse> Subjects(int page)
        {
            int count = await subjectRepository.SubjectsCount();

            if (count == 0)
            {
                return new()
                {
                    IsOk = true,
                    Page = 0,
                    MaxPage = 0,
                    Subjects = Enumerable.Empty<SubjectObject>()
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

            IEnumerable<Subject> subjects = await subjectRepository.TakeSubjects(itemsOnPage, (page - 1) * itemsOnPage);
            return new()
            {
                IsOk = true,
                Page = page,
                MaxPage = maxPage,
                Subjects = subjects.Select(s => new SubjectObject
                {
                    Id = s.Id,
                    Name = s.Name
                })
            };
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost("subjects/create")]
        public async Task<CreateSubjectResponse> CreateSubject([FromBody] CreateSubjectRequest request)
        {
            Subject subject = new()
            {
                Name = request.Name
            };

            try
            {
                await subjectRepository.AddSubject(subject);
                return new()
                {
                    IsOk = true,
                    Id = subject.Id,
                    Name = subject.Name
                };
            }
            catch (RepositoryException)
            {
                return new("An error occered while creating a subject");
            }
        }
    }
}
