using Contracts.CommandModels;
using Contracts.Models.Plain;
using Core.Commands;
using Core.Commands.Tags;
using Core.Commands.Thumbnail;
using Core.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using SFTServer.Filters;
using SFTServer.Utilities;
using System.Globalization;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;

namespace SFTServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ThumbnailController : ControllerBase
    {
        private readonly string[] _permittedExtensions = { ".jpg", ".jpeg", ".png" };
        private readonly long _fileSizeLimit = 10 * 1000 * 1000;

        // Get the default form options so that we can use them to set the default 
        // limits for request body data.
        private static readonly FormOptions _defaultFormOptions = new FormOptions();

        public ThumbnailController(SetThumbnailCommand setThumbnailCommand, GetThumbnailQuery getThumbnailQuery, RemoveThumbnailCommand removeThumbnailCommand)
        {
            SetThumbnailCommand = setThumbnailCommand;
            GetThumbnailQuery = getThumbnailQuery;
            RemoveThumbnailCommand = removeThumbnailCommand;
        }

        private SetThumbnailCommand SetThumbnailCommand { get; }
        private RemoveThumbnailCommand RemoveThumbnailCommand { get; }
        private GetThumbnailQuery GetThumbnailQuery { get; }

        [HttpGet]
        public ThumbnailPlainModel Get([FromQuery] Guid id)
        {
            if (id == default)
            {
                return null;
            }

            return GetThumbnailQuery.Run(id);
        }

        [HttpPost]
        [DisableFormValueModelBinding]
        public async Task<CommandResultWith<Guid>> Add(Guid tagId)
        {
            if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
            {
                return new CommandResultWith<Guid>("Not multipart content type.");
            }

            //// Accumulate the form data key-value pairs in the request (formAccumulator).
            var streamedFileContent = Array.Empty<byte>();

            var boundary = MultipartRequestHelper.GetBoundary(
                MediaTypeHeaderValue.Parse(Request.ContentType),
                _defaultFormOptions.MultipartBoundaryLengthLimit);
            var reader = new MultipartReader(boundary, HttpContext.Request.Body);

            var section = await reader.ReadNextSectionAsync();

            while (section != null)
            {
                var hasContentDispositionHeader =
                    ContentDispositionHeaderValue.TryParse(
                        section.ContentDisposition, out var contentDisposition);

                if (!hasContentDispositionHeader)
                {
                    section = await reader.ReadNextSectionAsync();
                    continue;
                }


                if (MultipartRequestHelper
                    .HasFileContentDisposition(contentDisposition))
                {
                    streamedFileContent =
                        await FileHelpers.ProcessStreamedFile(section, contentDisposition,
                            ModelState, _permittedExtensions, _fileSizeLimit);

                    if (!ModelState.IsValid)
                    {
                        var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                        return new CommandResultWith<Guid>(string.Join(", ", errors));
                    }
                }

                // Drain any remaining section body that hasn't been consumed and
                // read the headers for the next section.
                section = await reader.ReadNextSectionAsync();
            }

            var commandModel = new SetThumbnailCommandModel
            {
                TagId = tagId,
                Thumbnail = streamedFileContent,
            };

            return SetThumbnailCommand.Run(commandModel);
        }

        [HttpDelete]
        public CommandResult Remove(Guid id)
        {
            return RemoveThumbnailCommand.Run(id);
        }

        private static Encoding GetEncoding(MultipartSection section)
        {
            var hasMediaTypeHeader =
                MediaTypeHeaderValue.TryParse(section.ContentType, out var mediaType);

            // UTF-7 is insecure and shouldn't be honored. UTF-8 succeeds in 
            // most cases.
            if (!hasMediaTypeHeader || Encoding.UTF7.Equals(mediaType.Encoding))
            {
                return Encoding.UTF8;
            }

            return mediaType.Encoding;
        }
    }
}
