namespace Sales.AtomicSeller.Config
{
    /// <summary>
    /// Company Invoice settings.
    /// Will be displayed on Invoice header.
    /// </summary>
    public class InvoiceConfig
    {
        /// <summary>
        /// Company Name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Company Adress.
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Company Address Complement.
        /// </summary>
        public string AddressComplement { get; set; }
        /// <summary>
        /// Company City.
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Company Country.
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// Company Email.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Company Website.
        /// </summary>
        public string Website { get; set; }
        /// <summary>
        /// Company Logo.
        /// </summary>
        public string Logo { get; set; }
        /// <summary>
        /// Company Tax Rate.
        /// </summary>
        public decimal TaxRate { get; set; }
    }
}
