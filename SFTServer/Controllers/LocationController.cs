using Contracts.CommandModels;
using Contracts.Models;
using Contracts.QueryModel;
using Core.Commands;
using Core.Commands.LocationTags;
using Core.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SFTServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        public LocationController(GetLocationDataQuery getLocationDataQuery, GetAllLocationsDataQuery getAllLocationsDataQuery, AddLocationTagCommand addLocationTagCommand, SetLocationTagsCommand setLocationTagsCommand, RemoveLocationTagCommand removeLocationTagCommand)
        {
            GetLocationDataQuery = getLocationDataQuery;
            GetAllLocationsDataQuery = getAllLocationsDataQuery;
            AddLocationTagCommand = addLocationTagCommand;
            SetLocationTagsCommand = setLocationTagsCommand;
            RemoveLocationTagCommand = removeLocationTagCommand;
        }

        private GetLocationDataQuery GetLocationDataQuery { get; }
        private GetAllLocationsDataQuery GetAllLocationsDataQuery { get; }
        private AddLocationTagCommand AddLocationTagCommand { get; }
        private SetLocationTagsCommand SetLocationTagsCommand { get; }
        private RemoveLocationTagCommand RemoveLocationTagCommand { get; }

        [HttpGet]
        public TaggerDirectoryInfo Get(string path)
        {
            var data = GetLocationDataQuery.Run(path);

            return data;
        }

        [HttpGet]
        public IEnumerable<TaggerDirectoryInfo> All()
        {
            var data = GetAllLocationsDataQuery.Run(new EmptyQueryModel());

            return data;
        }

        [HttpPut]
        public void AddTags(UpdateTagsCommandModel model)
        {
            AddLocationTagCommand.Run(model);
        }

        [HttpPut]
        public void SetTags(UpdateTagsCommandModel model)
        {
            SetLocationTagsCommand.Run(model);
        }

        [HttpPut]
        public void RemoveTags(UpdateTagsCommandModel model)
        {
            RemoveLocationTagCommand.Run(model);
        }
    }
}
