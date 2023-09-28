using System.ComponentModel.DataAnnotations;

namespace QuickOrderSystemClassLibrary
{
    public class Order
    {
        public int Id { get; set; }
        public int TableNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime Created { get; set; }
        public string? Status { get; set; }
    }
}
