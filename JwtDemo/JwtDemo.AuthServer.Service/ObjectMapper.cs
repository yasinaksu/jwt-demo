using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwtDemo.AuthServer.Service
{
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> _mapper = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DtoMapper>();
            });

            return config.CreateMapper();
        });

        public static IMapper Mapper => _mapper.Value;
    }
}
