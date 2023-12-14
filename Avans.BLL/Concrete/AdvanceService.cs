using Avans.Core.Mappers;
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
        public bool Add(AdvanceDTO advancedto) //entity yolla 
        {
            

            //mapper yazcam entitiyyi dto ya  ye maple
            bool isAdded = false;
            try
            {
                AdvanceRepository advanceRepository = new AdvanceRepository();
                
                
                isAdded = advanceRepository.Add(new MyMapper<AdvanceDTO, Advance>().Map<AdvanceDTO, Advance>(advancedto)); //advance yerine entity yi mapleyerek olusacak advance dto yu gonder
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
                AdvanceRepository advanceRepository = new AdvanceRepository();
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
                AdvanceRepository advanceRepository = new AdvanceRepository();
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
                AdvanceRepository advanceRepository = new AdvanceRepository();
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
                AdvanceRepository advanceRepository = new       AdvanceRepository();
                isDeleted = advanceRepository.Delete(advance);
            }
            catch (Exception ex)
            {
            }
            return isDeleted;
        }
    }
}
