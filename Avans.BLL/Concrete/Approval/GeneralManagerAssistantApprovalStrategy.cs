using Avans.BLL.Abstract;
using Avans.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avans.BLL.Concrete.Approval
{
    public class GeneralManagerAssistantApprovalStrategy : IApproval
    {
       
        public AdvanceUpdateDTO MakeApprove(AdvanceUpdateDTO advanceupdate)
        {
            if (advanceupdate.ApprovedAmount >= 5001 && advanceupdate.ApprovedAmount <= 10000 && advanceupdate.StatusID == 203 && advanceupdate.isApproved != false)
            {
                advanceupdate.StatusID = 206;


            }
            else if (advanceupdate.isApproved != false) 
            {
                advanceupdate.StatusID = 204;

            }
            else
            {
                advanceupdate.StatusID = 103;

            }
            return advanceupdate;
        }
    }
}
