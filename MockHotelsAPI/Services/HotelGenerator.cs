using Bogus;
using MockHotelsAPI.Models;

namespace MockHotelsAPI.Services
{
    /// <summary>
    /// Genererer mock-hoteller med realistiske data til test og udvikling.
    /// </summary>
    public static class HotelGenerator
    {
        /// <summary>
        /// Genererer en liste af fiktive hoteller fra hele verden.
        /// </summary>
        /// <param name="count">Antal hoteller der skal genereres</param>
        /// <returns>Liste med hoteller</returns>
        public static List<Hotel> GenerateHotels(int count = 10)
        {
            var faker = new Faker();

            var cities = new List<(string CityCode, string CountryCode, double Latitude, double Longitude)>
            {
                ("CPH", "DK", 55.6761, 12.5683),
                ("NYC", "US", 40.7128, -74.0060),
                ("LON", "GB", 51.5074, -0.1278),
                ("PAR", "FR", 48.8566, 2.3522),
                ("BKK", "TH", 13.7563, 100.5018),
                ("TYO", "JP", 35.6895, 139.6917),
                ("RIO", "BR", -22.9068, -43.1729),
                ("SYD", "AU", -33.8688, 151.2093),
                ("CPT", "ZA", -33.9249, 18.4241),
                ("CAI", "EG", 30.0444, 31.2357)
            };

            var currencies = new List<string>
            {
                "USD", "DKK", "EUR", "GBP", "JPY", "THB", "BRL", "AUD", "ZAR", "EGP"
            };

            var hotels = new List<Hotel>();

            for (int i = 0; i < count; i++)
            {
                var city = faker.PickRandom(cities);
                var currency = faker.PickRandom(currencies);

                var hotel = new Hotel
                {
                    HotelId = faker.Random.AlphaNumeric(8).ToUpper(),
                    Name = faker.Company.CompanyName() + " Hotel",
                    CityCode = city.CityCode,
                    CountryCode = city.CountryCode,
                    Stars = faker.Random.Int(1, 5),
                    Rating = Math.Round(faker.Random.Double(6.5, 9.8), 1),
                    Available = faker.Random.Bool(0.85f),
                    GeoCode = new GeoCode
                    {
                        Latitude = city.Latitude + faker.Random.Double(-0.05, 0.05),
                        Longitude = city.Longitude + faker.Random.Double(-0.05, 0.05)
                    },
                    Price = new Price
                    {
                        Currency = currency,
                        Total = faker.Random.Decimal(50, 350)
                    },
                    Images = new List<string>
                    {
                        $"https://picsum.photos/seed/{faker.Random.Guid()}/600/400",
                        $"https://picsum.photos/seed/{faker.Random.Guid()}/600/400?blur",
                        $"https://picsum.photos/seed/{faker.Random.Guid()}/600/400?grayscale"
                    }
                };

                hotels.Add(hotel);
            }

            return hotels;
        }
    }
}
