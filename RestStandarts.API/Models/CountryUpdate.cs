namespace RestStandarts.API.Models
{
    public class CountryUpdate
    {
        public Guid Id { get; set; }
        public string NumericCode { get; set; }
        public string AlphaCode2 { get; set; }
        public string AlphaCode3 { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
