using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class StudentsController : Controller
    {
        private static readonly int itemsOnPage = 20;
        private readonly IStudentRepository studentRepository;

        public StudentsController(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        [Authorize(Roles = "Student")]
        [HttpPost("students/register")]
        public async Task<InitializeStudentResponse> Register([FromBody] InitializeStudentRequest request)
        {
            if (!int.TryParse(User.Claims.First(claim => claim.Type == "Id").Value, out int id))
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                return new InitializeStudentResponse("Invalid token id");
            }

            Student student = await studentRepository.GetStudentById(id);

            if (student != null)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                return new InitializeStudentResponse("This user is already created, no need to register");
            }

            student = new Student()
            {
                Id = id,
                FirstName = request.FirstName,
                SecondName = request.SecondName,
                GroupId = null
            };

            await studentRepository.AddStudent(student);
            return new InitializeStudentResponse
            {
                IsOk = true,
                Id = student.Id,
                FirstName = student.FirstName,
                SecondName = student.SecondName,
                GroupId = student.GroupId
            };
        }

        [Authorize(Roles = "Student")]
        [HttpGet("students/me")]
        public async Task<StudentResponse> MyAccount()
        {
            if (!int.TryParse(User.Claims.First(claim => claim.Type == "Id").Value, out int id))
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                return new StudentResponse("Invalid token id");
            }

            Student requester = await studentRepository.GetStudentById(id);

            if (requester == null)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                return new StudentResponse("Account with this id was not found. Maybe you forgot to register on this service");
            }

            return new StudentResponse
            {
                IsOk = true,
                Id = requester.Id,
                FirstName = requester.FirstName,
                SecondName = requester.SecondName,
                GroupName = requester.Group?.Name
            };
        }

        [Authorize(Roles = "Teacher")]
        [HttpGet("/students")]
        public async Task<StudentListResponse> Students(int? page, string searchPattern)
        {
            if (!page.HasValue)
                page = 1;

            int count = await studentRepository.StudentCount();

            if (count == 0)
            {
                return new()
                {
                    IsOk = true,
                    Page = 0,
                    MaxPage = 0,
                    Students = Enumerable.Empty<StudentObject>()
                };
            }

            int maxPage = (int)Math.Ceiling((double)count / itemsOnPage);

            if (page > maxPage || page < 1)
            {
                return new("Page is out of bounds")
                {
                    Page = page.Value,
                    MaxPage = maxPage
                };
            }

            IEnumerable<Student> students;

            if (searchPattern != null)
            {
                students = await studentRepository.FindStudents(itemsOnPage, (page.Value - 1) * itemsOnPage, searchPattern);
            }
            else
            {
                students = await studentRepository.FindStudents(itemsOnPage, (page.Value - 1) * itemsOnPage);
            }

            return new()
            {
                IsOk = true,
                Page = page.Value,
                MaxPage = maxPage,
                Students = students.Select(st => new StudentObject()
                {
                    Id = st.Id,
                    FirstName = st.FirstName,
                    SecondName = st.SecondName,
                    GroupName = st.Group?.Name
                })
            };
        }
    }
}
