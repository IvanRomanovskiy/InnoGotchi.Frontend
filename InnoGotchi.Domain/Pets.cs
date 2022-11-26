using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoGotchi.Domain
{
    public class Pets
    {
        public ICollection<Pet> pets { get; set; } = new List<Pet>();
    }
}
