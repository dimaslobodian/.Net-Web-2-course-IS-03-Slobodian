using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Room : BaseEntity
    {
        [Required]
        public string Number { get; set; }

        [Required]
        [ForeignKey("RoomType")]
        public int TypeId { get; set; }

        [Required]
        public decimal PricePerDay { get; set; }


        public virtual RoomType RoomType { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;
            if (obj.GetType() != typeof(Room))
                return false;
            return (this.Id == ((Room)obj).Id);
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
