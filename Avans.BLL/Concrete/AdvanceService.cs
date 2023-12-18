using Avans.Core.Mappers;
using Avans.DAL.Abstract;
using Avans.DAL.Concrete;
using Avans.DTOs;
using Avans.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avans.BLL.Concrete
{
    public class AdvanceService
    {
        AdvanceRepository advanceRepository;

        public AdvanceService(AdvanceRepository advanceRepository )
        {
            this.advanceRepository = advanceRepository;
        }
        public bool Add(AdvanceInsertDTO advancedto) //entity yolla 
        {
            

            //mapper yazcam entitiyyi dto ya  ye maple
            bool isAdded = false;
            try
            {
                isAdded = advanceRepository.AddAdvanceWithHistory(advancedto); //advance yerine entity yi mapleyerek olusacak advance dto yu gonder
            }
            catch (Exception ex)
            {
            }
            return isAdded;
        }

        public List<Advance> GetAll()
        {
            List<Advance> advances = new List<Advance>();
            try
            {
                advances = advanceRepository.GetAll().ToList();
            }
            catch (Exception ex)
            {
            }

            return advances;
        }
        public List<AdvancesPendingApprovalSelectDTO> GetPending()
        {
            List<AdvancesPendingApprovalSelectDTO> advances = new List<AdvancesPendingApprovalSelectDTO>();
            try
            {
                advances = advanceRepository.GetAdvanceForPendingApproval().ToList();
            }
            catch (Exception ex)
            {
            }

            return advances;
        }


        public Advance Get(int Id)
        {
            Advance advances = new Advance();
            try
            {
                advances = advanceRepository.GetById(Id);
            }
            catch (Exception ex)
            {
            }

            return advances;
        }

        public bool Update(Advance advance)
        {
            bool isUpdated = false;
            try
            {
                isUpdated = advanceRepository.Update(advance);
            }
            catch (Exception ex)
            {
            }

            return isUpdated;
        }

        public bool Delete(Advance advance)
        {
            bool isDeleted = false;
            try
            {
                isDeleted = advanceRepository.Delete(advance);
            }
            catch (Exception ex)
            {
            }
            return isDeleted;
        }
    }
}
