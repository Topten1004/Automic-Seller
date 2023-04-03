using Sales.AtomicSeller.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sales.AtomicSeller.Entities
{
    public class Lang : BaseEntity
    {
        public string Token { get; set; }
        public string? EnUs { get; set; }
        public string? FrFr { get; set; }
        public string? DeDe { get; set; }
        public string? ZhChs { get; set; }
        public string? EsEs { get; set; }
        public string? ItIt { get; set; }
        public string? NlNl { get; set; }
        public string? ZhTw { get; set; }
        public string? ElGr { get; set; }
        public string? JaJp { get; set; }
        public string? PtPt { get; set; }
        public string? RuRu { get; set; }
        public string? HiIn { get; set; }
        public string? PlPl { get; set; }
        public string? IdId { get; set; }
        public string? ArEg { get; set; }
        public string? HeIl { get; set; }
    }
}
