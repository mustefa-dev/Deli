namespace Deli.DATA.DTOs
{

    public class SaleForm 
    {   public Guid ItemId { get; set; }
        public double SalePrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
