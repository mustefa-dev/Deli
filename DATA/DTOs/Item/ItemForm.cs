namespace Deli.DATA.DTOs
{

    public class ItemForm 
    {
        public string? Name { get; set; }
        public string? ArName { get; set; }
        public  string MainDetails { get; set; }
        public  string ArMainDetails { get; set; }
        public string? Description { get; set; }
        public string? ArDescription { get; set; }
        public string? RefNumber { get; set; }
        public Dictionary<string, string> AdditionalInfo{ get; set; }
        public string[] imaages { get; set; }
        public double? Price { get; set; }
        public int? Quantity { get; set; }
        public Guid? InventoryId { get; set; }
        public Guid? CategoryId { get; set; }
    }
}
