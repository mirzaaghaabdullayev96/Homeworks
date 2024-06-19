using ClassLibraryMyHelper;
using MyHelpDesk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using System.Threading.Tasks;
using Weaponry.Business.interfaces;
using Weaponry.Core.Models;
using Weaponry.Database;
using Weaponry.Business.SoundEffects;

namespace Weaponry.Business.implementation
{
    public class WeaponService : IWeaponService
    {
        public void ChangeFireMode(Weapon weapon)
        {
            Console.Clear();
            int shootMode = MyHelper.TakeChoice(1, 2, Choices.ShootingModesChangeChoices);
            if (shootMode == -1)
                return;
            if (weapon.ShootingMode == (ShootingModes)shootMode)
            {
                Console.Clear();
                Console.WriteLine("Your weapon already has this shooting mode");
                MyHelper.GoToMainMenu();
                return;
            }
            weapon.ShootingMode = (ShootingModes)shootMode;
            Console.Clear();
            Console.WriteLine($"You have successfully change your mode to {weapon.ShootingMode}");
            MyHelper.GoToMainMenu();

        }

        public void Create(out Weapon ak47)
        {
            int maxCapacity = MyHelper.TakeChoiceFirstTimeMagazine(10, 20, 30, 40, 75);
            int current = MyHelper.TakeChoice(1, () => Console.WriteLine("Enter current amount of bullets"), maxCapacity);
            int shootMode = MyHelper.TakeChoiceMode(1, 2, Choices.ShootingModesChoices);
            ak47 = new Weapon(maxCapacity, current, (ShootingModes)shootMode);
            Console.Clear();
            Console.WriteLine("Your weapon was successfully created!");
            Console.WriteLine(Drawings.drawingOfKalashnikov);

            string productJson = JsonSerializer.Serialize(ak47);
            //writing to TXT file
            Directory.CreateDirectory(Data.directoryPath);
            if (!File.Exists(Data.filePathDatabase))
            {
                using FileStream fs = File.Create(Data.filePathDatabase);
            }

            using StreamWriter writer = new(Data.filePathDatabase);
            writer.WriteLine(productJson);
            writer.WriteLine(Drawings.drawingOfKalashnikov);
            Thread.Sleep(1500);
            Console.WriteLine("Let's start our game!");
            Thread.Sleep(1500);
            Console.Clear();
        }

        public void Fire(Weapon weapon)
        {
            Console.Clear();
            if (weapon.ShootingMode == ShootingModes.Automatic)
            {
                if (MagazineCheck(weapon))
                {
                    Drawings.drawOfFiring();
                    SoundHandling.AutomaticShot();
                    Thread.Sleep(800);
                    weapon.CurrentMagazine = 0;
                }
            }

            if (weapon.ShootingMode == ShootingModes.Single)
            {
                if (MagazineCheck(weapon))
                {
                    Drawings.drawOfFiring();
                    SoundHandling.SingleShot();
                    Thread.Sleep(800);
                    weapon.CurrentMagazine -= 1;
                }
            }
        }

        public int GetRemainBulletCount(Weapon weapon)
        {
            return weapon.CurrentMagazine;
        }

        public void GetRemainBulletCountInfo(Weapon weapon)
        {
            Console.Clear();
            Console.WriteLine($"Your current state of magazine: {weapon.CurrentMagazine}/{weapon.MaxCapacityMagazine}");
            MyHelper.GoToMainMenu();
        }


        public void Reload(Weapon weapon)
        {
            Console.Clear();
            Drawings.drawOfReloading();
            weapon.CurrentMagazine += (weapon.MaxCapacityMagazine - GetRemainBulletCount(weapon));
            SoundHandling.Reload();
            Thread.Sleep(800);
        }

        public void Shoot(Weapon weapon)
        {
            Console.Clear();
            if (MagazineCheck(weapon))
            {
                Drawings.drawOfShooting();
                SoundHandling.SingleShot();
                Thread.Sleep(800);
                weapon.CurrentMagazine -= 1;
            }
        }

        public void GetInformation(Weapon weapon)
        {
            Console.Clear();
            Console.WriteLine($"Max capacity of magazine is {weapon.MaxCapacityMagazine} and right now there are {weapon.CurrentMagazine} bullets. Shooting mode is {weapon.ShootingMode}");
            Console.WriteLine();
            MyHelper.GoToMainMenu();
        }

        public bool MagazineCheck(Weapon weapon)
        {
            if (weapon.CurrentMagazine <= 0)
            {
                Console.WriteLine("There are not enough bullets for shooting. Your magazine is empty");
                MyHelper.GoToMainMenu();
                return false;
            }
            return true;

        }

        public void ChangeMagazineCapacity(Weapon weapon)
        {
            Console.Clear();
            int newCapacity = MyHelper.TakeChoice(10, 20, 30, 40, 75);
            if (newCapacity == -1)
                return;
            if (newCapacity == weapon.MaxCapacityMagazine)
            {
                Console.Clear();
                Console.WriteLine("You magazine has the same maximum capacity");
                MyHelper.GoToMainMenu();
            }
            if (newCapacity != weapon.MaxCapacityMagazine)
            {
                if (newCapacity > weapon.CurrentMagazine)
                {
                    weapon.MaxCapacityMagazine = newCapacity;
                    Console.Clear();
                    Console.WriteLine($"You have successfully changed your magazine. Maximum capacity of new magazine is {weapon.MaxCapacityMagazine}");
                    MyHelper.GoToMainMenu();
                    return;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"You need to first remove bullets from your magazine to have current amount of bullets less or equal to new magazine capacity!");
                    MyHelper.GoToMainMenu();
                    return;
                }

            }

        }

        public void AddOrRemoveBullets(Weapon weapon)
        {
            int choice = MyHelper.TakeChoice(1, 2, Choices.RemoveOrAddBullet);
            if (choice == -1)
                return;
            if (choice == 1)
            {
                Console.Clear();
                Console.WriteLine("How many bullets would you like to add?");
                int count = MyHelper.TakeChoiceForAdd(weapon.CurrentMagazine, weapon.MaxCapacityMagazine);
                if (count == -1)
                    return;
                weapon.CurrentMagazine += count;
                Console.WriteLine($"You have successfully added {count} bullets\n");
                MyHelper.GoToMainMenu();
                return;
            }

            if (choice == 2)
            {
                Console.Clear();
                Console.WriteLine("How many bullets would you like to remove?");
                int count = MyHelper.TakeChoiceForRemove(weapon.CurrentMagazine);
                if (count == -1)
                    return;
                weapon.CurrentMagazine -= count;
                Console.WriteLine($"You have successfully removed {count} bullets\n");
                MyHelper.GoToMainMenu();
                return;
            }

        }
    }
}
