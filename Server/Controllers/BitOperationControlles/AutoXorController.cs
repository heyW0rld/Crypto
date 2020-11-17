using Microsoft.AspNetCore.Mvc;
using Crypto.Lab1;
using System;
using System.Threading.Tasks;
using System.Numerics;

namespace Server.Controllers.BitOperationControllers
{
    [Route("api/[controller]")]
    public class AutoXorController : Controller
    {
        public class XP
        {
            public string x { get; set; }
            public string p { get; set; }

        }

        [HttpPost()]
        public async Task<ActionResult<string>> Post(XP xp)
        {
            BigInteger x;
            uint p;

            try
            {
                if (xp.x == null || xp.p == null) throw new FormatException();
                x = BitOperation.BinStrToBigInteger(xp.x);
                p = Convert.ToUInt32(xp.p, 10);
            }
            catch (FormatException)
            {
                ModelState.AddModelError("Error", "Некорректные параметры");
                return BadRequest(ModelState);
            }

            return await Task.Run(() => BitOperation.AutoXOR(x, p).ToString());
        }
    }
}

