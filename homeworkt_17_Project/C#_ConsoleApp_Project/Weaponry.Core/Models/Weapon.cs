using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Weaponry.Core.Models
{
    public enum ShootingModes
    {
        Automatic = 1,
        Single
    }
    public class Weapon
    {
        public int MaxCapacityMagazine { get; set; }
        [JsonIgnore]
        public int CurrentMagazine { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ShootingModes ShootingMode { get; set; }

        public Weapon(int max, int current, ShootingModes mode)
        {
            MaxCapacityMagazine = max;
            CurrentMagazine = current;
            ShootingMode = mode;
        }
    }
}
