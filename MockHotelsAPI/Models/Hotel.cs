using System.Collections.Generic;

namespace MockHotelsAPI.Models
{
    /// <summary>
    /// Repræsenterer et hotel og dets primære egenskaber, som bruges til visning og søgning.
    /// </summary>
    public class Hotel
    {
        /// <summary>
        /// Unikt hotel-ID, som bruges til at identificere hotellet.
        /// </summary>
        public string HotelId { get; set; }

        /// <summary>
        /// Navnet på hotellet.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Koordinater (latitude, longitude) for hotellets placering.
        /// </summary>
        public GeoCode GeoCode { get; set; }

        /// <summary>
        /// Bykode (IATA 3-tegns kode, f.eks. CPH for København).
        /// </summary>
        public string CityCode { get; set; }

        /// <summary>
        /// ISO landekode, f.eks. DK for Danmark.
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// Antal stjerner (1 til 5).
        /// </summary>
        public int Stars { get; set; }

        /// <summary>
        /// Bruger-rating (skala fra 1.0 til 10.0).
        /// </summary>
        public double Rating { get; set; }

        /// <summary>
        /// Prisoplysninger for hotelopholdet.
        /// </summary>
        public Price Price { get; set; }

        /// <summary>
        /// Tilgængelighed (true hvis hotellet har ledige værelser).
        /// </summary>
        public bool Available { get; set; }

        /// <summary>
        /// Liste over billed-URLs som repræsenterer hotellet.
        /// </summary>
        public List<string> Images { get; set; }
    }
}
