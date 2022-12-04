using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KreditPlus_Web.Models
{
    public class Menu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MenuId { get; set; }
        [Required]
        public string? MenuName { get; set; }
        [Required]
        public decimal? Price { get; set; }
        public string? MenuTypeDesc { get; set; }
        public string? StatusMenuDesc { get; set; }
        public short? StatusMenuId { get; set; }
        public short? MenuTypeId { get; set; }
    }
}
