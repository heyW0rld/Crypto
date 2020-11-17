using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Crypto.Lab1;


namespace Server.Controllers.BitOperationControlles
{
    [Route("api/[controller]")]
    public class RC4Controller : ControllerBase
    {
        public static IWebHostEnvironment _environment;

        public RC4Controller(IWebHostEnvironment environment)
        {
            _environment = environment;
        }


        public async Task<ActionResult<string>> Post(IFormFile message, IFormFile key)
        {
            byte[] fileBytesMessage, fileBytesKey;

            if (message == null || key == null)
            {
                ModelState.AddModelError("Error", "Некорректные параметры");
                return BadRequest(ModelState);
            }

            if (message.Length > 0 && key.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    message.CopyTo(ms);
                    fileBytesMessage = ms.ToArray();
                }
                using (var ms = new MemoryStream())
                {
                    key.CopyTo(ms);
                    fileBytesKey = ms.ToArray();
                }
 
                var rc4 = new RC4(fileBytesKey);
                var result = rc4.Encode(fileBytesMessage);
                var path = "(rc4)" + message.FileName;
                using (var fs = System.IO.File.Create(_environment.WebRootPath + "/" + path))
                {
                    fs.Write(result, 0, result.Count());
                    fs.Flush();
                    return path;
                }
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
