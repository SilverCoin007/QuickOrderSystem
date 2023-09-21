using System.ComponentModel.DataAnnotations;

namespace QuickOrderSystemClassLibrary
{
    public class QrCode
    {
        public int ID { get; set; }
        public int TableNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime Created { get; set; }
        public string? ImageData { get; set; }
        public string? Url { get; set; }
    }
}
