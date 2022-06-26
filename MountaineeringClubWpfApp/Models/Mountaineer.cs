using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountaineeringClubWpfApp.Models
{
    public class Mountaineer
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int IdMountaineer { get; set; }
        public string FullNameMountaineer { get; set; }
        public int GroupId { get; set; }
        [ForeignKey("GroupId")]
        public Group Group { get; set; }
        //public virtual ICollection<Group> Groups { get; set; }

    }
}
