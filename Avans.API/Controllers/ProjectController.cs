using Avans.BLL.Concrete;
using Avans.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Avans.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectService _projectService;
        public ProjectController(ProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet("~/api/getproject")]
        public List<ProjectSelectDTO> GetProject()
        {
            return _projectService.GetAll();

        }
    }
}
