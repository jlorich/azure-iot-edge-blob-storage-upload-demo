using System;
using Microsoft.Azure.WebJobs;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.Storage;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace MicrosoftSolutions.IoT.Edge.BlobUploadFunction.Functions
{
    /// <summary>
    ///  This class handles file uploads to the local blob storage from http
    /// </summary>
    
    public static class EdgeBlobUploadFunction {
        /// <summary>
        ///  Azure Functions entrypoint for handling file upload to local blob storage
        /// </summary>
        /// <param name="request">The Http Request</param>
        /// <param name="output">The blob stream to write to</param>
        /// <param name="logger">A logger to use</param>
        [FunctionName("EdgeBlobUpload")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "upload/{fileName:regex(^[a-zA-Z0-9_\\-\\.]+$)}")] HttpRequest request,
            [Blob("edgeupload/{fileName}", FileAccess.Write), StorageAccount("EdgeStorage")] Stream output,
            ILogger logger
        ) {
            // Write to blob
            try {
                await request.Body.CopyToAsync(output);
            } catch (Exception ex) {
                logger.LogError(ex.ToString());
                return new ObjectResult(ex);
            }
            
            return new CreatedResult("", null);
        }
    }
}
