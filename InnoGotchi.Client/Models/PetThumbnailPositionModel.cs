using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoGotchi.Client.Models
{
    public class PetThumbnailPositionModel
    {
        public PetThumbnailModel PetThumbnail { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
    }
}
