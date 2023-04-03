using System.ComponentModel.DataAnnotations;

namespace Sales.AtomicSeller.Entities
{
    public class AtomicService : BaseEntity
    {
        //public int Id { get; set; }
        public string? ServiceSKU { get; set; }

        [StringLength(128)]
        public string? ServiceName { get; set; }
        public string? ServiceDescription { get; set; }
        public string? ServiceType { get; set; }
        public int? NbShippingMax { get; set; }
        public string? CustomerType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }        
        public string? ImageFilePath { get; set; }
        public decimal UnitPriceExclTax { get; set; }
        public decimal? MonthUnitPriceExclTax { get; set; }
        public decimal? AnnualUnitPriceExclTax { get; set; }
        public bool Visible { get; set; }
    }
}
