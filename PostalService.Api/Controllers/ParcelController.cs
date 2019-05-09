using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PostalService.Api.Extensions;
using PostalService.Api.Managers;
using PostalService.Api.Models;

namespace PostalService.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ParcelController : ControllerBase
    {
        private readonly IParcelManager _parcelManager;
        public ParcelController(IParcelManager parcelManager)
        {
            _parcelManager = parcelManager ?? throw new ArgumentNullException(nameof(parcelManager));
        }

        /// <summary>
        /// Retrieves cost of delivery based on weight and size of a parcel
        /// </summary>
        /// <param name="dimension">Dimensions of a parcel</param>
        /// <returns></returns>
        [HttpPost()]
        [ProducesResponseType(200, Type = typeof(ParcelResult))]
        [ProducesResponseType(404)]
        public ActionResult GetParcelAndCost(Dimension dimension)
        {
            try
            {                
                var response = _parcelManager.FindParcel(dimension);
                if (response != null) return Ok(response);
            }
            catch (Exception)
            {
                // Yell    Log    Catch  Throw
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return NotFound();
        }
    }
}
