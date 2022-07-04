using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountaineeringClubWpfApp.Models
{
    public class Group
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int IdGroup { get; set; }
        public Mountaineer Mountaineer { get; set; }
        public string NameGroup { get; set; }
        public virtual ICollection<MountaineeringClub> MountaineeringClubs { get; set; }

    }
}
