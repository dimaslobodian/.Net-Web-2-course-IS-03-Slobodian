using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Booking : BaseEntity
    {
        [Required]
        [ForeignKey("Room")]
        public int RoomId{ get; set; }
        [Required]
        public bool IsBooked { get; set; } = false;
        [Required]
        public bool IsOccupied { get; set; } = false;
        public DateTime? DateOfCheckIn{ get; set; }
        public DateTime? DateOfCheckOut { get; set; }

        public virtual Room Room { get; set; }
    }
}
