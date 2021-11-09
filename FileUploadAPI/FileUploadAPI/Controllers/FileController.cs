using API_Test;
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
        FileHelper FH = new FileHelper();
        public FileController(ILogger<FileController> logger)
        {
            _logger = logger;
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("Upload")]
        public async Task<IActionResult> Upload()
        {
            //List<SavedFile> created = new List<SavedFile>();
            try
            {
                var formCollection = await Request.ReadFormAsync();
                foreach (IFormFile file in formCollection.Files)
                {
                    var folderName = "Files";
                    var directoryToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                    if (file.Length > 0)
                    {            
                        var name = ContentDispositionHeaderValue.Parse(file.ContentDisposition).Name.Trim('"');
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                      
                        var fullPath = Path.Combine(directoryToSave, fileName);
                        var dbPath = Path.Combine(folderName, fileName);

                        byte[] bytes = FH.ReadFully(file.OpenReadStream());
                        Console.WriteLine(bytes.Length);
                        //File.WriteAllBytes(bytes, fullPath);

                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                            
                        }
                        //return Ok(new { dbPath });
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
