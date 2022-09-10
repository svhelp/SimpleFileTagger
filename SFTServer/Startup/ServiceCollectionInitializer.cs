﻿using AutoMapper;
using Core;
using Core.Commands.LocationTags;
using Core.Commands.Tags;
using Core.Queries;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace SFTServer.Startup
{
    public static class ServiceCollectionInitializer
    {
        public static IServiceCollection AddContext(
             this IServiceCollection services)
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var dbFile = Path.Combine(appData, "FileTagger", "tagger.db");

            services.AddDbContext<TaggerContext>(options =>
                options.UseLazyLoadingProxies()
                    .UseSqlite($"Filename={dbFile}"));

            return services;
        }

        public static IServiceCollection AddMapper(
             this IServiceCollection services)
        {

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CoreMapperProfile>();
            });

            var Mapper = mapperConfig.CreateMapper();

            services.AddSingleton(Mapper);

            return services;
        }

        public static IServiceCollection AddQueries(
             this IServiceCollection services)
        {
            services.AddTransient<GetTagsQuery>();
            services.AddTransient<GetLocationDataQuery>();
            services.AddTransient<GetAllLocationsDataQuery>();

            return services;
        }

        public static IServiceCollection AddCommands(
             this IServiceCollection services)
        {
            services.AddTransient<CreateTagCommand>();
            services.AddTransient<RemoveTagCommand>();
            services.AddTransient<MergeTagsCommand>();
            services.AddTransient<AddLocationTagCommand>();
            services.AddTransient<SetLocationTagsCommand>();
            services.AddTransient<RemoveLocationTagCommand>();

            return services;
        }
    }
}
