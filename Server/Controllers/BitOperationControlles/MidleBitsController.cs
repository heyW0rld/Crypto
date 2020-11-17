using Microsoft.AspNetCore.Mvc;
using Crypto.Lab1;
using System;
using System.Numerics;
using System.Threading.Tasks;

namespace Server.Controllers.BitOperationControllers
{
    [Route("api/[controller]")]
    public class MidleBitsController : Controller
    {
        public class AIL
        {
            public string a { get; set; }
            public string i { get; set; }
            public string l { get; set; }

        }

        [HttpPost()]
        public async Task<ActionResult<string>> Post(AIL ail)
        {
            BigInteger a;
            uint i, l;

            try
            {
                if (ail.a == null || ail.i == null || ail.l == null) throw new FormatException();
                a = BitOperation.BinStrToBigInteger(ail.a);
                i = Convert.ToUInt32(ail.i, 10);
                l = Convert.ToUInt32(ail.l, 10);

                if (i > l) throw new FormatException();
            }
            catch (FormatException)
            {
                ModelState.AddModelError("Error", "Некорректные параметры");
                return BadRequest(ModelState);
            }

            return await Task.Run(() => BitOperation.BigIntegerToBinStr(BitOperation.MidleBits(a, i, l)));
        }
    }
}
