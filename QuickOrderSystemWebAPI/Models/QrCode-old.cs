using System.ComponentModel.DataAnnotations;

namespace QuickOrderSystemWebAPI.Models
{
    public class QrCode
    {
        public int ID { get; set; }
        public int TableNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime Created { get; set; }
        public byte[] ImageData { get; set; }
    }
}
