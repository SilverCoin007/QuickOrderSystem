using System.ComponentModel.DataAnnotations;

namespace QuickOrderSystemWebAPI.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int TableNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime Created { get; set; }
        public string? Status { get; set; }
    }
}
