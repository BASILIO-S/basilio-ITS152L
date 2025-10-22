using System.ComponentModel.DataAnnotations;


namespace G5M2.Models
{
    public class Item
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = "";

        [Required, StringLength(50)]
        public string Code { get; set; } = "";

        [Required, StringLength(50)]
        public string Brand { get; set; } = "";

        [Range(0.01, 100000)]
        public decimal UnitPrice { get; set; }
    }


}
