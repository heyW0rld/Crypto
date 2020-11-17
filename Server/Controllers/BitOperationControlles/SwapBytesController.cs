using System;
using Microsoft.AspNetCore.Mvc;
using Crypto.Lab1;
using System.Threading.Tasks;
using System.Numerics;

namespace Server.Controllers.BitOperationControllers
{
    [Route("api/[controller]")]
    public class SwapBytesController : Controller
    {
        public class AIJ
        {
            public string a { get; set; }
            public string i { get; set; }
            public string j { get; set; }
        }

        [HttpPost()]
        public async Task<ActionResult<string>> Post(AIJ aij)
        {
            BigInteger a;
            uint i, j;

            try
            {
                if (aij.a == null || aij.i == null || aij.j == null) throw new FormatException();

                a = BitOperation.BinStrToBigInteger(aij.a);
                i = Convert.ToUInt32(aij.i, 10);
                j = Convert.ToUInt32(aij.j, 10);

                if (i > 3 || j > 3 || aij.a.Length > 32) throw new FormatException();
            }
            catch (FormatException)
            {
                ModelState.AddModelError("Error", "Некорректные параметры");
                return BadRequest(ModelState);
            }

            return await Task.Run(() => BitOperation.BigIntegerToBinStr(BitOperation.SwapBytes(a, i, j)));
        }
    }
}
