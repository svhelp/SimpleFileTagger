using Contracts.CommandModels;
using Contracts.Models.Complex;
using Contracts.QueryModel;
using Core.Commands;
using Core.Commands.Locations;
using Core.Commands.LocationTags;
using Core.Queries;
using Microsoft.AspNetCore.Mvc;

namespace SFTServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        public LocationController(GetLocationDataQuery getLocationDataQuery, GetAllLocationsDataQuery getAllLocationsDataQuery, AddLocationTagCommand addLocationTagCommand, SetLocationTagsCommand setLocationTagsCommand, RemoveLocationTagCommand removeLocationTagCommand, RemoveLocationCommand removeLocationCommand, CreateLocationCommand createLocationCommand, MarkLocationsNotFoundCommand markLocationsNotFoundCommand)
        {
            GetLocationDataQuery = getLocationDataQuery;
            GetAllLocationsDataQuery = getAllLocationsDataQuery;
            AddLocationTagCommand = addLocationTagCommand;
            SetLocationTagsCommand = setLocationTagsCommand;
            RemoveLocationTagCommand = removeLocationTagCommand;
            RemoveLocationCommand = removeLocationCommand;
            CreateLocationCommand = createLocationCommand;
            MarkLocationsNotFoundCommand = markLocationsNotFoundCommand;
        }

        private GetLocationDataQuery GetLocationDataQuery { get; }
        private GetAllLocationsDataQuery GetAllLocationsDataQuery { get; }
        private CreateLocationCommand CreateLocationCommand { get; }
        private AddLocationTagCommand AddLocationTagCommand { get; }
        private SetLocationTagsCommand SetLocationTagsCommand { get; }
        private RemoveLocationTagCommand RemoveLocationTagCommand { get; }
        private RemoveLocationCommand RemoveLocationCommand { get; }
        private MarkLocationsNotFoundCommand MarkLocationsNotFoundCommand { get; }

        [HttpGet]
        public LocationModel Get([FromQuery] string path)
        {
            return GetLocationDataQuery.Run(path);
        }

        [HttpGet]
        public IEnumerable<LocationModel> All()
        {
            return GetAllLocationsDataQuery.Run(new EmptyQueryModel());
        }

        [HttpPost]
        public CommandResultWith<UpdateLocationCommandResultModel> Create(SimpleNamedModel model)
        {
            return CreateLocationCommand.Run(model);
        }

        [HttpPut]
        public CommandResultWith<UpdateLocationCommandResultModel> AddTags(UpdateLocationCommandModel model)
        {
            return AddLocationTagCommand.Run(model);
        }

        [HttpPut]
        public CommandResultWith<UpdateLocationCommandResultModel> SetTags(UpdateLocationCommandModel model)
        {
            return SetLocationTagsCommand.Run(model);
        }

        [HttpPut]
        public CommandResult RemoveTags(UpdateLocationCommandModel model)
        {
            return RemoveLocationTagCommand.Run(model);
        }

        [HttpDelete]
        public CommandResult Remove([FromQuery] Guid id)
        {
            return RemoveLocationCommand.Run(id);
        }

        [HttpPatch]
        public CommandResult MarkNotFound([FromQuery] List<Guid> locationIds)
        {
            return MarkLocationsNotFoundCommand.Run(locationIds);
        }
    }
}
