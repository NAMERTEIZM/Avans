using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avans.Core.Mappers
{
    public class MyMapper<TEntity, TDto> : Profile
    {
        private readonly IMapper mapper;

        public MyMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEntity, TDto>();
                cfg.CreateMap<TDto, TEntity>();
            });

            mapper = config.CreateMapper();
        }


        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return mapper.Map<TSource, TDestination>(source);
        }
    }
}
