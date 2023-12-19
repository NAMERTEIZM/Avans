using Avans.Core.Mappers;
using Avans.DAL.Concrete;
using Avans.DTOs;
using Avans.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avans.BLL.Concrete.Approval
{
    public class TitleService
    {
        TitleRepository titleRepository;
        private readonly MyMapper<Title, TitleDTO> _titleMapper;

        public TitleService(TitleRepository titleRepository, MyMapper<Title, TitleDTO> titleMApper)
        {
            this.titleRepository = titleRepository;
            _titleMapper = titleMApper;
        }

        public List<TitleDTO> GetAll()
        {
            List<TitleDTO> titleDTOs = new List<TitleDTO>();
            try
            {
                List<Title> title = titleRepository.GetAll().ToList();

                titleDTOs = _titleMapper.Map<List<Title>, List<TitleDTO>>(title);

                
            }
            catch (Exception ex)
            {
                // Hata yönetimi
            }

            return titleDTOs;
        }
    }
}
