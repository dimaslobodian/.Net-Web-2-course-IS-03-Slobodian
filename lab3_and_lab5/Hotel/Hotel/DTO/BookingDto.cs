using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DTO
{
    public class BookingDto
    {
        public string RoomNumber { get; set; }
        public string CheckInYear { get; set; }
        public string CheckInMonth { get; set; }
        public string CheckInDay { get; set; }
        public string CheckOutYear { get; set; }
        public string CheckOutMonth { get; set; }
        public string CheckOutDay { get; set; }
    }
}
