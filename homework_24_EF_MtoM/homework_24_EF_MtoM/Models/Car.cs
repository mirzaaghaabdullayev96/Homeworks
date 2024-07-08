using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace homework_24_EF_MtoM.Models
{
    public class Car
    {
        public int id { get; set; }
        public int MaxSpeed { get; set; }
        public int FuelTankCapacity { get; set; }
        public int Power { get; set; }
        public int DoorCount { get; set; }
        public int ModelId { get; set; }
        public Model Model { get; set; }
        public List<CarColor> CarColors { get; set; }
        public override string ToString()
        {
            return $"Max speed - {MaxSpeed}, Power - {Power}";
        }
    }
}
