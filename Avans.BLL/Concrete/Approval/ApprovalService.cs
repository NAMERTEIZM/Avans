using Avans.DTOs;
using Avans.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avans.BLL.Concrete.Approval
{
    public class ApprovalService
    {
        private ApprovalOperation approvalOperation = null;
        private GeneralManagerApprovalStrategy generalManagerApprovalStrategy = null;


        public bool ApplyAmountApprovalLogic(AdvanceUpdateDTO advanceupdate)
        {
            decimal advanceAmount = advanceupdate.ApprovedAmount;



            if (advanceupdate.StatusID == 208)
            {
                approvalOperation = new ApprovalOperation(new AccountantApprovalStrategy());
                
            }
            else if (advanceupdate.StatusID == 206)
            {
                approvalOperation = new ApprovalOperation(new FinancialManagerApprovalStrategy());
            }
           else if (advanceAmount >= 0 && advanceAmount <= 1000 || advanceupdate.StatusID == 201)
            {
                approvalOperation = new ApprovalOperation(new UnitManagerApprovalStrategy());


            }
            else if (advanceAmount >= 1001 && advanceAmount <= 5000 || advanceupdate.StatusID == 202)
            {
                approvalOperation = new ApprovalOperation(new DirectorApprovalStrategy());


            }
            else if (advanceAmount >= 5001 && advanceAmount <= 10000 || advanceupdate.StatusID == 203)
            {
                approvalOperation = new ApprovalOperation(new GeneralManagerAssistantApprovalStrategy());

                //approvalOperation.MakeApprove(advanceupdate);


            }
            else if (advanceAmount > 10000 || advanceupdate.StatusID == 204)
            {
                approvalOperation = new ApprovalOperation(new GeneralManagerApprovalStrategy());

            }


            approvalOperation.MakeApprove(advanceupdate);
            return true;

        }

        //void deneme()
        //{
        //    _approvalOperation = new ApprovalOperation(new GeneralManagerApprovalStrategy());
        //}
    }
}
