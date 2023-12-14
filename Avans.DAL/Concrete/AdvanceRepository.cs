using Avans.Core.Contexts;
using Avans.DTOs;
using Avans.Models.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Avans.DAL.Concrete
{
    public class AdvanceRepository : GenericRepository<Advance>
    {
        
      ConnectionHelper connectionHelper;

        public AdvanceRepository(ConnectionHelper connectionHelper)
        {
            this.connectionHelper = connectionHelper;
        }

        public List<AdvanceDTO> GetAdvanceAll()
        {
            var sqlQuery = "select adv.AdvanceAmount,adv.RequestDate,adv.DesiredDate,prj.ProjectName,sts.StatusName,emp.[Name],ttl.TitleName,adh.Date,adh.ApprovedAmount,pym.DeterminedPaymentDate,rcp.ReceiptDate,rcp.isRefundReceipt from AdvanceHistory adh\r\ninner join Advance adv ON adv.ID = adh.AdvanceID\r\ninner join Project prj ON prj.ID=adv.ProjectID\r\ninner join [Status] sts ON sts.ID = adv.StatusID \r\ninner join Employee emp ON emp.ID = adh.TransactorID\r\ninner join Title ttl ON ttl.ID = emp.TitleID\r\ninner join Payment pym ON pym.ID = adv.PaymentID\r\ninner join Receipt rcp ON rcp.AdvanceID =adv.ID";
            var conn = connectionHelper.CreateConnection();
            List<AdvanceDTO> advances = conn.Query<AdvanceDTO>(sqlQuery).ToList();

            return advances;
        }

    }
}
