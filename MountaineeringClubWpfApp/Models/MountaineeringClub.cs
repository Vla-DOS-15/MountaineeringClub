using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountaineeringClubWpfApp.Models
{
    public class MountaineeringClub
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int IdMountaineeringClub { get; set; }
        public int GroupId { get; set; }
        [ForeignKey("GroupId")]
        public Group Group { get; set; }
        public int MountainId { get; set; }
        [ForeignKey("MountainId")]
        public Mountain Mountain { get; set; }
        public DateTime? DateBeginningAscent { get; set; }
        public DateTime? DateCompletionAscent { get; set; }
        public bool IsCarriedOutAscent { get; set; }
    }
}
