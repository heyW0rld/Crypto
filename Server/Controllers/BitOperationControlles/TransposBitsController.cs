//using System;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using System.Numerics;
//using Crypto.Lab1;
//using System.Collections.Generic;

//namespace Server.Controllers.BitOperationControlles
//{
//    [Route("api/[controller]")]
//    //public class TransposBitsController : Controller
//    //{
//    //    public class AT
//    //    {
//    //        public string a { get; set; }
//    //        public string t { get; set; }
//    //    }

//    //    [HttpPost()]
//    //    public async Task<ActionResult<string>> Post(AT at)
//    //    {
//    //        BigInteger a;
//    //        var t = new List<uint>();

//    //        try
//    //        {
//    //            if (at.a == null || at.t == null) throw new FormatException();

//    //            a = BitOperation.BinStrToBigInteger(at.a);

//    //            var splitStr = at.t.Split(" ");
//    //            if (splitStr.Length > at.a.Length) throw new FormatException();
//    //            foreach (var item in splitStr)
//    //            {
//    //                var tmp = Convert.ToUInt32(item);
//    //                if (tmp > 9) throw new FormatException();
//    //                t.Add(tmp);
//    //            }
                    
//    //        }
//    //        catch (FormatException)
//    //        {
//    //            ModelState.AddModelError("Error", "Некорректные параметры");
//    //            return BadRequest(ModelState);
//    //        }


//    //        return await Task.Run(() => BitOperation.BigIntegerToBinStr(BitOperation.TransposBits(a, t.ToArray())));
//    //    }
//    //}
//}
