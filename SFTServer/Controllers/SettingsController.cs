using Core.Commands;
using Core.Importers;
using Microsoft.AspNetCore.Mvc;

namespace SFTServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        public SettingsController(LegacyDataImporter legacyDataImporter)
        {
            LegacyDataImporter = legacyDataImporter;
        }

        private LegacyDataImporter LegacyDataImporter { get; }

        [HttpPatch]
        public CommandResult ImportDirectory([FromQuery] string path)
        {
            try
            {
                LegacyDataImporter.ImportRootDitectory(path);
            }
            catch (Exception e)
            {
                return new CommandResult(e.Message);
            }

            return new CommandResult();
        }
    }
}
