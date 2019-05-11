using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostalService.Api.Managers;
using PostalService.Api.Models;
using System;

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
        /// <param name="weight"> Weight of the parcel in kg</param>
        /// <param name="height">Height of the parcel in cm</param>
        /// <param name="width">Width of the parcel in cm</param>
        /// <param name="depth">Depth of the parcel in cm</param>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(200, Type = typeof(ParcelResult))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public ActionResult GetParcelAndCost(int weight, int height, int width, int depth)
        {
            if (weight <= 0 || height <= 0 || width <= 0 || depth <= 0)
            {
                return BadRequest("Values must be more than 0");
            }

            var inputArgs = new InputArgs(weight, height, width, depth);
            try
            {
                var result = _parcelManager.FindParcel(inputArgs);
                if (result != null) return Ok(result);
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
