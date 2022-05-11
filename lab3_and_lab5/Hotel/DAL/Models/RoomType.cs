using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class RoomType : BaseEntity
    {       
        [Required]
        public string Type { get; set; }
    }
}
