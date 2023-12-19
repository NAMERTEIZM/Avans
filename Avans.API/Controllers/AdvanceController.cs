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
        [HttpGet("~/api/getadvance/{EmployeeID}")]
        public List<AdvanceDTO> GetAdvance(int EmployeeID)
        {
           var data=  _repository.GetAdvanceAll(EmployeeID);
            return data;
        }
       
        [HttpGet("~/api/getadvancebyid/{advanceID}")]
        public List<AdvanceDTO> GetAdvanceByID(int advanceID)
        {
            var data = _repository.GetAdvanceByID(advanceID);
            return data;

        }

        [HttpGet("~/api/getadvanceforpendingapprovaldetailbyid/{advanceID}")]
        public List<AdvancesPendingApprovalSelectDTO> GetAdvanceForPendingApprovalDetailByID(int advanceID)
        {
            var data = _repository.GetAdvanceForPendingApprovalDetailByID(advanceID);
            return data;

        }
        [HttpGet("~/api/getadvancepending/{StatusID}")]
        public List<AdvancesPendingApprovalSelectDTO> GetAdvancePending(int StatusID)
        {
            var data = _service.GetPending(StatusID);
            return data;

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
