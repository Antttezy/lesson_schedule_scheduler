using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class TeachersController : Controller
    {
        private readonly ITeacherRepository teacherRepository;

        public TeachersController(ITeacherRepository teacherRepository)
        {
            this.teacherRepository = teacherRepository;
        }

        [Authorize(Roles = "Teacher")]
        [HttpGet("teachers/me")]
        public async Task<TeacherResponse> MyAccount()
        {
            if (!int.TryParse(User.Claims.First(claim => claim.Type == "Id").Value, out int id))
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                return new TeacherResponse("Invalid token id");
            }

            Teacher requester = await teacherRepository.GetTeacherById(id);

            if (requester == null)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                return new TeacherResponse("Account with this id was not found. Please contact an administrator");
            }

            return new TeacherResponse
            {
                IsOk = true,
                Id = requester.Id,
                FirstName = requester.FirstName,
                SecondName = requester.SecondName,
                Position = requester.Position,
                Degree = requester.Degree.ToString(),
                CareerStarted = requester.CareerStarted
            };
        }
    }
}
