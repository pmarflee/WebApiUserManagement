using AutoMapper;
using System;
using System.Linq;

namespace WebApiUserManagement.App_Start
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration Configure()
        {
            var profiles =
            from t in typeof(AutoMapperConfig).Assembly.GetTypes()
            where typeof(Profile).IsAssignableFrom(t)
            select (Profile)Activator.CreateInstance(t);

            var config = new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
            });

            return config;
        }
    }
}