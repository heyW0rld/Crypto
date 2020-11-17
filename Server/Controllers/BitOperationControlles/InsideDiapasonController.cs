using Microsoft.AspNetCore.Mvc;
using Crypto.Lab1;
using System;
using System.Threading.Tasks;
using System.Numerics;

namespace Server.Controllers.BitOperationControllers
{
    [Route("api/[controller]")]
    public class InsideDiapasonController : Controller
    {

        [HttpPost()]
        public async Task<ActionResult<string>> Post(string X)
        {
            BigInteger x;

            try
            {
                if (X == null) throw new FormatException();
                x = BigInteger.Parse(X);
            }
            catch (FormatException)
            {
                ModelState.AddModelError("Error", "Некорректные параметры");
                return BadRequest(ModelState);
            }

            return await Task.Run(() => BitOperation.InsideDiapason(x).ToString());
        }
    }
}

