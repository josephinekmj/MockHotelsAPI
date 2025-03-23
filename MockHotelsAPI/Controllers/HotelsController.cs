using Microsoft.AspNetCore.Mvc;
using MockHotelsAPI.Models;
using MockHotelsAPI.Services;
using System.Collections.Generic;

namespace MockHotelsAPI.Controllers
{
    /// <summary>
    /// API-controller som returnerer en liste af mock-hoteller.
    /// Dette simulerer en rigtig hotelsøgning, som man kender det fra f.eks. Amadeus API.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class HotelsController : ControllerBase
    {
        /// <summary>
        /// Returnerer en liste over genererede hoteller.
        /// Kan udvides med query-parametre som city, dates, guests, mm.
        /// </summary>
        /// <returns>Et JSON-objekt med en "data" property, som indeholder en liste af hoteller</returns>
        [HttpGet]
        public ActionResult<object> Get()
        {
            // Generér 10 mock-hoteller
            List<Hotel> hotels = HotelGenerator.GenerateHotels(200);

            // Returner i Amadeus-lignende struktur
            var result = new
            {
                data = hotels
            };

            return Ok(result);
        }
    }
}
