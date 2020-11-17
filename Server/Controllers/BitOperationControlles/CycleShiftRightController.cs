using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using Crypto.Lab1;

namespace Server.Controllers.BitOperationControlles
{
    [Route("api/[controller]")]
    public class CycleShiftRightController : Controller
    {
        public class APN
        {
            public string a { get; set; }
            public string p { get; set; }
            public string n { get; set; }
        }

        [HttpPost()]
        public async Task<ActionResult<string>> Post(APN apn)
        {
            BigInteger a;
            uint p, n;

            try
            {
                if (apn.a == null || apn.p == null || apn.n == null) throw new FormatException();

                a = BitOperation.BinStrToBigInteger(apn.a);
                p = Convert.ToUInt32(apn.p, 10);
                n = Convert.ToUInt32(apn.n, 10);
            }
            catch (FormatException)
            {
                ModelState.AddModelError("Error", "Некорректные параметры");
                return BadRequest(ModelState);
            }

            return await Task.Run(() => BitOperation.BigIntegerToBinStr(BitOperation.CycleShiftRight(a, p, n)));
        }
    }
}
