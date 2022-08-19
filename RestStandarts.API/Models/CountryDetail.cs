namespace RestStandarts.API.Models
{
    class CountryDetail
    {
        /// <summary>
        /// Country Unique Number
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Country Global Alpha Numeric Code
        /// </summary>
        public string NumericCode { get; set; }
        /// <summary>
        /// Country Global Alpha Code 2 Standart
        /// </summary>
        public string AlphaCode2 { get; set; }
        /// <summary>
        /// Country Global Alpha Code 3 Standart
        /// </summary>
        public string AlphaCode3 { get; set; }
        /// <summary>
        /// Country Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Country Description
        /// </summary>
        public string Description { get; set; }
    }
}
