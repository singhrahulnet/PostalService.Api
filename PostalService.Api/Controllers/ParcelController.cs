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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (weight <= 0 || height <= 0 || width <= 0 || depth <= 0)
            {
                return BadRequest("Values must be more than 0");
            }

            var inputArgs = new InputArgs { Weight = weight, Height = height, Width = width, Depth = depth };
            try
            {
                var response = _parcelManager.FindParcel(inputArgs);
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
