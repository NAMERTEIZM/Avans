using Avans.BLL.Abstract;
using Avans.Core.Mappers;
using Avans.DAL.Concrete;
using Avans.DTOs;
using Avans.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avans.BLL.Concrete
{
    public class ApprovalOperation
    {
        private IApproval _approve;
        AdvanceRepository advanceRepository;
        public ApprovalOperation(IApproval _approve)
        {
            this._approve = _approve;

        }
        public bool MakeApprove(AdvanceUpdateDTO advanceupdate)
        {
            bool isAdded = false;
            try
            {
               var data = _approve.MakeApprove(advanceupdate);
                 advanceRepository = new AdvanceRepository(new SqlConnection("Server=localhost\\SQLEXPRESS;Database=AvansDatabase;Trusted_Connection=True;TrustServerCertificate=True;"));
                isAdded = advanceRepository.UpdateAdvanceWithHistory(data);
                
            }
            catch (Exception ex)
            {
            }
            return isAdded;
        }
    }
}
