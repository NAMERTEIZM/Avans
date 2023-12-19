using Avans.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avans.BLL.Abstract
{
    public interface IApproval
    {
        AdvanceUpdateDTO MakeApprove(AdvanceUpdateDTO advanceupdate);
    }
}
