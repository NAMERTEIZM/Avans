﻿using Avans.BLL.Abstract;
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
            if (advanceupdate.isApproved != false)
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
