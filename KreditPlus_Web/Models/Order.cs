using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KreditPlus_Web.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string? OrderNumber { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? OrderDate { get; set; }
        public string? StatusOrder { get; set; }
        public string? Cashier { get; set; }
        public string? Waiters { get; set; }
        public short? TableNumber { get; set; }
        public string? ClosedByName { get; set; }
        public decimal? Total { get; set; }
        [NotMapped]
        public int? OrderStatus { get; set; }
        [NotMapped]
        public int? ClosedBy { get; set; }
    }

    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public string? OrderId { get; set; }
        public int? MenuId { get; set; }
        public int? Qty { get; set; }
        public decimal? Price { get; set; }
        [NotMapped]
        public string TableNumber { get; set; }
        [NotMapped]
        public string MenuName { get; set; }
    }
}
