using Avans.BLL.Abstract;
using Avans.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avans.BLL.Concrete.Approval
{
    public class DirectorApprovalStrategy : IApproval
    {
        

        public AdvanceUpdateDTO MakeApprove(AdvanceUpdateDTO advanceupdate)
        {
            if(advanceupdate.ApprovedAmount >= 1001 && advanceupdate.ApprovedAmount <= 5000 && advanceupdate.StatusID == 202 && advanceupdate.isApproved != false)
            {
                advanceupdate.StatusID = 206;

            }
            else if (advanceupdate.isApproved != false)
            {
                advanceupdate.StatusID = 203;

            }
            else
            {
                advanceupdate.StatusID = 103;

            }
            return advanceupdate;

        }
    }
}
