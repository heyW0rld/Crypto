using Microsoft.AspNetCore.Mvc;
using Crypto.Lab1;
using System;
using System.Threading.Tasks;
using System.Numerics;

namespace Server.Controllers.BitOperationControllers
{
    [Route("api/[controller]")]
    public class ZeroSmallBitsController : Controller
    {
        public class AM
        {
            public string a { get; set; }
            public string m { get; set; }
        }

        [HttpPost()]
        public async Task<ActionResult<string>> Post(AM am)
        {
            BigInteger a;
            uint m;

            try
            {
                if (am.a == null || am.m == null) throw new FormatException();

                a = BitOperation.BinStrToBigInteger(am.a);
                m = Convert.ToUInt32(am.m, 10);

                if (m > 32 || am.a.Length > 32) throw new FormatException();
            }
            catch (FormatException)
            {
                ModelState.AddModelError("Error", "Некорректные параметры");
                return BadRequest(ModelState);
            }

            return await Task.Run(() => BitOperation.BigIntegerToBinStr(BitOperation.ZeroSmallBits(a, m)));
        }
    }
}

