using Contracts.CommandModels;
using Contracts.Models;
using Core.Commands;
using Core.Commands.Tags;
using Core.Commands.Thumbnail;
using Core.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using SFTServer.Utilities;
using System.Globalization;
using System.Net;

namespace SFTServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ThumbnailController : ControllerBase
    {
        private const int MultipartBoundaryLengthLimit = 15000000;

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
        public ThumbnailModel Get([FromQuery] Guid id)
        {
            return GetThumbnailQuery.Run(id);
        }

        [HttpPost]
        public CommandResultWith<Guid> Add(SimpleNamedModel model)
        {
            //if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
            //{
            //    return new CommandResultWith<Guid>("Not multipart content type.");
            //}

            //// Accumulate the form data key-value pairs in the request (formAccumulator).
            //var formAccumulator = new KeyValueAccumulator();
            //var trustedFileNameForDisplay = string.Empty;
            //var untrustedFileNameForStorage = string.Empty;
            //var streamedFileContent = Array.Empty<byte>();

            //var boundary = MultipartRequestHelper.GetBoundary(
            //    MediaTypeHeaderValue.Parse(Request.ContentType),
            //    MultipartBoundaryLengthLimit);
            //var reader = new MultipartReader(boundary, HttpContext.Request.Body);

            //var section = await reader.ReadNextSectionAsync();

            //while (section != null)
            //{
            //    var hasContentDispositionHeader =
            //        ContentDispositionHeaderValue.TryParse(
            //            section.ContentDisposition, out var contentDisposition);

            //    if (hasContentDispositionHeader)
            //    {
            //        if (MultipartRequestHelper
            //            .HasFileContentDisposition(contentDisposition))
            //        {
            //            untrustedFileNameForStorage = contentDisposition.FileName.Value;
            //            // Don't trust the file name sent by the client. To display
            //            // the file name, HTML-encode the value.
            //            trustedFileNameForDisplay = WebUtility.HtmlEncode(
            //                    contentDisposition.FileName.Value);

            //            streamedFileContent =
            //                await FileHelpers.ProcessStreamedFile(section, contentDisposition,
            //                    ModelState, _permittedExtensions, _fileSizeLimit);

            //            if (!ModelState.IsValid)
            //            {
            //                return BadRequest(ModelState);
            //            }
            //        }
            //        else if (MultipartRequestHelper
            //            .HasFormDataContentDisposition(contentDisposition))
            //        {
            //            // Don't limit the key name length because the 
            //            // multipart headers length limit is already in effect.
            //            var key = HeaderUtilities
            //                .RemoveQuotes(contentDisposition.Name).Value;
            //            var encoding = GetEncoding(section);

            //            if (encoding == null)
            //            {
            //                ModelState.AddModelError("File",
            //                    $"The request couldn't be processed (Error 2).");
            //                // Log error

            //                return BadRequest(ModelState);
            //            }

            //            using (var streamReader = new StreamReader(
            //                section.Body,
            //                encoding,
            //                detectEncodingFromByteOrderMarks: true,
            //                bufferSize: 1024,
            //                leaveOpen: true))
            //            {
            //                // The value length limit is enforced by 
            //                // MultipartBodyLengthLimit
            //                var value = await streamReader.ReadToEndAsync();

            //                if (string.Equals(value, "undefined",
            //                    StringComparison.OrdinalIgnoreCase))
            //                {
            //                    value = string.Empty;
            //                }

            //                formAccumulator.Append(key, value);

            //                if (formAccumulator.ValueCount >
            //                    _defaultFormOptions.ValueCountLimit)
            //                {
            //                    // Form key count limit of 
            //                    // _defaultFormOptions.ValueCountLimit 
            //                    // is exceeded.
            //                    ModelState.AddModelError("File",
            //                        $"The request couldn't be processed (Error 3).");
            //                    // Log error

            //                    return BadRequest(ModelState);
            //                }
            //            }
            //        }
            //    }

            //    // Drain any remaining section body that hasn't been consumed and
            //    // read the headers for the next section.
            //    section = await reader.ReadNextSectionAsync();
            //}

            //// Bind form data to the model
            //var formData = new FormData();
            //var formValueProvider = new FormValueProvider(
            //    BindingSource.Form,
            //    new FormCollection(formAccumulator.GetResults()),
            //    CultureInfo.CurrentCulture);
            //var bindingSuccessful = await TryUpdateModelAsync(formData, prefix: "",
            //    valueProvider: formValueProvider);

            //if (!bindingSuccessful)
            //{
            //    ModelState.AddModelError("File",
            //        "The request couldn't be processed (Error 5).");
            //    // Log error

            //    return BadRequest(ModelState);
            //}





            var model1 = new SetThumbnailCommandModel();
            return SetThumbnailCommand.Run(model1);
        }

        [HttpDelete]
        public CommandResult Remove(Guid id)
        {
            return RemoveThumbnailCommand.Run(id);
        }
    }
}
