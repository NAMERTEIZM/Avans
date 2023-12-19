using Avans.BLL.Concrete.Approval;
using Avans.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Avans.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TitleController : ControllerBase
    {
        private readonly TitleService _titleService;

        public TitleController(TitleService titleService)
        {
            _titleService = titleService;
        }
        [HttpGet("~/api/gettitle")]
        public List<TitleDTO> GetTitle()
        {
            return _titleService.GetAll();

        }
    }
}
