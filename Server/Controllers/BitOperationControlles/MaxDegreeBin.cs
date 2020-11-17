using System;
using Microsoft.AspNetCore.Mvc;
using Crypto.Lab1;
using System.Threading.Tasks;
using System.Numerics;

namespace Server.Controllers.BitOperationControllers
{
    [Route("api/[controller]")]
    public class MaxDegreeBinController : Controller
    {
        [HttpPost()]
        public async Task<ActionResult<string>> Post(string A)
        {
            BigInteger a;

            try
            {
                if (A == null) throw new FormatException();

                a = BigInteger.Parse(A);
            }
            catch (FormatException)
            {
                ModelState.AddModelError("Error", "Некорректные параметры");
                return BadRequest(ModelState);
            }

            return await Task.Run(() => BitOperation.MaxDegreeBin(a).ToString());
        }
    }
}
