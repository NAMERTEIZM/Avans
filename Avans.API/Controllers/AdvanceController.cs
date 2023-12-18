using AutoMapper;
using Avans.BLL.Concrete;
using Avans.BLL.Concrete.Approval;
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
        private readonly ApprovalService _approvalService; 
        private readonly IMapper _mapper;

        public AdvanceController(AdvanceService service, AdvanceRepository repository, IMapper mapper, ApprovalService approvalService)
        {
            _service = service;
            _repository = repository;
            _mapper = mapper;
            _approvalService = approvalService;

        }
        [HttpGet("~/api/getadvance")]
        public List<AdvanceDTO> GetAdvance()
        {
            return _repository.GetAdvanceAll();

        }
       
        [HttpGet("~/api/getadvancebyid/{advanceID}")]
        public List<AdvanceDTO> GetAdvanceByID(int advanceID)
        {
            var data = _repository.GetAdvanceByID(advanceID);
            return data;

        }
        [HttpGet("~/api/getadvancepending")]
        public List<AdvancesPendingApprovalSelectDTO> GetAdvancePending()
        {
            return _service.GetPending();

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
        [HttpPost("~/api/updateadvance")]
        public ActionResult POST([FromBody] AdvanceUpdateDTO dto)
        {

            if (dto == null)
                return BadRequest();
            if (_approvalService.ApplyAmountApprovalLogic(dto))
            {
                return Ok(dto);
            }
            return BadRequest();
        }

        //burada aldııgn dto yu bll'e gonder

    }
}
