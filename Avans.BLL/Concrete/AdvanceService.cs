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
            List<Advance> products = new List<Advance>();
            try
            {
                products = advanceRepository.GetAll().ToList();
            }
            catch (Exception ex)
            {
            }

            return products;
        }

        public Advance Get(int Id)
        {
            Advance product = new Advance();
            try
            {
                product = advanceRepository.GetById(Id);
            }
            catch (Exception ex)
            {
            }

            return product;
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
