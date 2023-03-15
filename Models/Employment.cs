namespace PortfolioAPI.Models
{
    public class Employment 
    {
        public int Id { get; set; }
        public string? Employer { get; set; }
        public string? Position { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End {get; set; }
        public string? ImagePath {get; set; }
        public string? Description {get; set; }
    }
}