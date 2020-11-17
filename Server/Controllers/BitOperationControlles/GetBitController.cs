using System;
using Microsoft.AspNetCore.Mvc;
using Crypto.Lab1;
using System.Threading.Tasks;
using System.Numerics;

namespace Server.Controllers.BitOperationControllers
{


    [Route("api/[controller]")]
    public class GetBitController : Controller
    {
        public class AK
        {
            public string a { get; set; }
            public string k { get; set; }

        }

        [HttpPost()]
        public async Task<ActionResult<string>> Post(AK ak)
        {
            BigInteger a;
            uint k;

            try
            {
                if(ak.a == null || ak.k == null) throw new FormatException();
                a = BitOperation.BinStrToBigInteger(ak.a);
                k = Convert.ToUInt32(ak.k, 10);

                if (k > 31 || ak.a.Length > 32) throw new FormatException();
            }
            catch(FormatException)
            {
                ModelState.AddModelError("Error", "Некорректные параметры");
                return BadRequest(ModelState);
            }

            return await Task.Run(() => BitOperation.BigIntegerToBinStr(BitOperation.GetBit(a, k)));
        }
    }
}
