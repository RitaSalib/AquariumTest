using System.Collections.Generic;

namespace AquariumTest.Models
{
    public class Tank
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }

        public virtual ICollection<Fish> Fishes { get; set; }

        public Tank()
        {
            this.Fishes = new List<Fish>();
        }
    }
}