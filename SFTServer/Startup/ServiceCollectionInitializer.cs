﻿using AutoMapper;
using Core;
using Core.Commands.Locations;
using Core.Commands.LocationTags;
using Core.Commands.TagGroups;
using Core.Commands.Tags;
using Core.Commands.Thumbnail;
using Core.Importers;
using Core.Queries;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace SFTServer.Startup
{
    public static class ServiceCollectionInitializer
    {
        public static IServiceCollection AddSwagger(
             this IServiceCollection services)
        {
            services.AddSwaggerDocument();

            return services;
        }

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
            services.AddTransient<GetTagGroupsQuery>();
            services.AddTransient<GetLocationDataQuery>();
            services.AddTransient<GetAllLocationsDataQuery>();
            services.AddTransient<GetThumbnailQuery>();
            services.AddTransient<SearchLocationsQuery>();

            return services;
        }

        public static IServiceCollection AddCommands(
             this IServiceCollection services)
        {
            services.AddTransient<LegacyDataImporter>();
            
            services.AddTransient<VerifyLocationsCommand>();
            services.AddTransient<MarkLocationsNotFoundCommand>();

            services.AddTransient<CreateTagCommand>();
            services.AddTransient<UpdateTagCommand>();
            services.AddTransient<RemoveTagCommand>();
            services.AddTransient<MergeTagsCommand>();

            services.AddTransient<AddOrUpdateGroupCommand>();
            services.AddTransient<RemoveGroupCommand>(); 
            services.AddTransient<AddTagToGroupCommand>();
            services.AddTransient<RemoveTagFromGroupCommand>();
            
            services.AddTransient<CreateLocationCommand>();
            services.AddTransient<AddLocationTagCommand>();
            services.AddTransient<SetLocationTagsCommand>();
            services.AddTransient<RemoveLocationCommand>();
            services.AddTransient<RemoveLocationTagCommand>();

            services.AddTransient<SetThumbnailCommand>();
            services.AddTransient<RemoveThumbnailCommand>();

            return services;
        }
    }
}
