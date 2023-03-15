namespace PortfolioAPI.Models
{
    public class Experience 
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Institution { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public string? ImagePath { get; set; }
        public string? Description { get; set; }
    }
}