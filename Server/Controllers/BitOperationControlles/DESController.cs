using System;
using Microsoft.AspNetCore.Mvc;
using Crypto.Lab1;
using System.Threading.Tasks;
using System.Numerics;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Crypto.Lab1.DESAlgo;
using ModeWork;
using Crypto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Server.Controllers.BitOperationControllers
{
    [Route("api/[controller]")]
    public class DESController : Controller
    {
        IWebHostEnvironment _environment;

        public DESController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public class KACD
        {
            public string key { get; set; }
            public string mode { get; set; }
            public string c0 { get; set; }
            public string decode { get; set; }
        }

        [HttpPost()]
        public async Task<ActionResult<string>> Post(IFormFile message, KACD kacd)
        {
            byte[] byteMessage = null;

            if (message == null)
            {
                ModelState.AddModelError("Error", "Некорректные параметры");
                return BadRequest(ModelState);
            }

            ICoder modeWork = null;
           
            try
            {
                if (kacd.key == null || kacd.mode == null) throw new FormatException();

                var byteKey = Encoding.Default.GetBytes(kacd.key);

                byte[] bytec0 = null;
                if (kacd.mode != "ecb")
                {
                    if(kacd.c0 == null) throw new FormatException();
                    bytec0 = Encoding.Default.GetBytes(kacd.c0);
                    if (bytec0.Length != 8) throw new FormatException();
                }

                if (byteKey.Length != 8) throw new FormatException();

                using (var ms = new MemoryStream())
                {
                    message.CopyTo(ms);
                    byteMessage = ms.ToArray();
                }
                if (byteMessage.Length % 8 != 0)
                {
                    var addByte = new List<byte>();
                    for (int i = 0; i < 8 - byteMessage.Length % 8; i++)
                        addByte.Add(0);

                    byteMessage = byteMessage.Concat(addByte.ToArray()).ToArray();
                }
      

                var des = new DES(BitConverter.ToUInt64(byteKey));

                switch (kacd.mode)
                {
                    case "ecb":
                        modeWork = new ECB(des);
                        break;
                    case "cbc":
                        modeWork = new CBC(des, bytec0);
                        break;
                    case "cfb":
                        modeWork = new CFB(des, bytec0);
                        break;
                    case "ofb":
                        modeWork = new OFB(des, bytec0);
                        break;
                }


            }
            catch (FormatException)
            {
                ModelState.AddModelError("Error", "Некорректные параметры");
                return BadRequest(ModelState);
            }

            var result = kacd.decode != null ? modeWork.Decode(byteMessage) : modeWork.Encode(byteMessage);
            var path = "(DES)" + message.FileName;
            using (var fs = System.IO.File.Create(_environment.WebRootPath + "/" + path))
            {
                fs.Write(result, 0, result.Count());
                fs.Flush();
            }

            return path;
        }
    }
}
