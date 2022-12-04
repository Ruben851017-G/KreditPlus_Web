using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KreditPlus_Web.Models
{
    public class Transactions
    {
        public int OrderId { get; set; }
        public string? OrderNumber { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? OrderDate { get; set; }
        public int UserWaitersOrder { get; set; }
        public string UserWaitersNameOrder { get; set; }
        public decimal? Total { get; set; }
        [NotMapped]
        public DateTime? FromDate { get; set; }
        [NotMapped]
        public DateTime? ToDate { get; set; }
    }
}
