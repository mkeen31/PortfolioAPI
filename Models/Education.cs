namespace PortfolioAPI.Models
{
    public class Education 
    {
        public int Id { get; set; }
        public string? Degree { get; set; }
        public string? Major { get; set; }
        public string? Institution { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public string? ImagePath { get; set; }
        public string? Description { get; set; }
    }
}