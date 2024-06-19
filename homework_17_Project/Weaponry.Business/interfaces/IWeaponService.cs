using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weaponry.Core.Models;

namespace Weaponry.Business.interfaces
{
    public interface IWeaponService
    {
        void Create(out Weapon weapon);
        void Shoot(Weapon weapon);
        void Reload(Weapon weapon);
        void Fire(Weapon weapon);
        int GetRemainBulletCount(Weapon weapon);
        void GetRemainBulletCountInfo(Weapon weapon);
        void ChangeFireMode(Weapon weapon);
        void GetInformation(Weapon weapon);
        void ChangeMagazineCapacity(Weapon weapon);
        void AddOrRemoveBullets(Weapon weapon);

    }
}
