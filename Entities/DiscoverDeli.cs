namespace Deli.Entities
{
    public class DiscoverDeli : BaseEntity<Guid>
    {
        public string? Title { get; set; } = "";
        public string? Description { get; set; }= "";
        public string? Image { get; set; }= "";
        public string? RedirectButton { get; set; }= "";
        public string? MiniTitle1 { get; set; }= "";
        public string? MiniDescription1 { get; set; }= "";
        public string? MiniTitle2 { get; set; }= "";
        public string? MiniDescription2 { get; set; }= "";
        public string? MiniTitle3 { get; set; }= "";
        public string? MiniDescription3 { get; set; }= "";
    }
}
