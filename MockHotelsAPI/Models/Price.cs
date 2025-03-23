namespace MockHotelsAPI.Models
{
    /// <summary>
    /// Indeholder pris og valuta for et hotelophold.
    /// </summary>
    public class Price
    {
        public string Currency { get; set; } // F.eks. "DKK"
        public decimal Total { get; set; }   // F.eks. 1295.50
    }
}
