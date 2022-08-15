using AutoMapper;
using System;

namespace DefaultGenericProject.Service.Mapping
{
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> lazy = new(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapProfile>();
            });
            return config.CreateMapper();
        });
        public static IMapper Mapper => lazy.Value;
    }
}