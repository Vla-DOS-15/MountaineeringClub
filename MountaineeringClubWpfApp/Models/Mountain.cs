using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountaineeringClubWpfApp.Models
{
    public class Mountain
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int IdMountain { get; set; }
        public string NameMountain { get; set; }
        public string HeightMountain { get; set; }
        public string Country  { get; set; }
        public string Region { get; set; }
        public virtual ICollection<MountaineeringClub> MountaineeringClubs { get; set; }

    }
}
