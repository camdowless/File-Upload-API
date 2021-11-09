using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
namespace BulkAPITest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        private readonly ILogger<FileController> _logger;
        public FileController(ILogger<FileController> logger)
        {
            _logger = logger;
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("Upload")]
        public async Task<IActionResult> Upload(IList<IFormFile> files)
        {
            var folderName = "Files";
            try
            {
                foreach (IFormFile file in files)
                {
                    var directoryToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                    if (file.Length > 0)
                    {            
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        var fullPath = Path.Combine(directoryToSave, fileName);

                        using (Stream fileStream = new FileStream(fullPath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal Server Error: {e}");
            }
            return Ok();
        }
    }
}
