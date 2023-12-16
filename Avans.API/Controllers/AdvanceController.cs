using AutoMapper;
using Avans.BLL.Concrete;
using Avans.DAL.Concrete;
using Avans.DTOs;
using Avans.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Avans.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvanceController : ControllerBase
    {
        private readonly AdvanceService _service;
        private readonly AdvanceRepository _repository;
        private readonly IMapper _mapper;

        public AdvanceController(AdvanceService service, AdvanceRepository repository, IMapper mapper)
        {
            _service = service;
            _repository = repository;
            _mapper = mapper;

        }
        [HttpGet("~/api/getadvance")]
        public List<AdvanceDTO> GetAdvance()
        {
            return _repository.GetAdvanceAll();

        }
        [HttpPost("~/api/addadvance")]
        public ActionResult POST([FromBody] AdvanceInsertDTO dto)
        {

            if (dto == null)
                return BadRequest();
            if (_service.Add(dto))
            {
                return Ok(dto);
            }
            return BadRequest();
        }

        //burada aldııgn dto yu bll'e gonder

    }
}
