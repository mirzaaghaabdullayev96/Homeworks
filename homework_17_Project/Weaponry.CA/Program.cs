using Weaponry.Core.Models;
using Weaponry.Business.implementation;
using Weaponry.Business.interfaces;
using MyHelpDesk;

namespace Weaponry.CA
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("====Hello! First you need to create your weapon!=====\n");
            //Creating your ak47!
            IWeaponService weaponService = new WeaponService();
            weaponService.Create(out Weapon ak47);

            while (true)
            {
                Choices.MainChoices();
                int choice = MyHelper.TakeChoice(0, 7);
                switch (choice)
                {
                    case 0:
                        weaponService.GetInformation(ak47);
                        break;
                    case 1:
                        weaponService.Shoot(ak47);
                        break;
                    case 2:
                        weaponService.Fire(ak47);
                        break;
                    case 3:
                        weaponService.GetRemainBulletCountInfo(ak47);
                        break;
                    case 4:
                        weaponService.Reload(ak47);
                        break;
                    case 5:
                        weaponService.ChangeFireMode(ak47);
                        break;
                    case 6:
                        Console.WriteLine("Program is closing ....");
                        Thread.Sleep(2000);
                        return;
                    case 7:
                        Choices.EditChoices();
                        int editChoice = MyHelper.TakeChoiceForEdit(1, 2);
                        switch (editChoice)
                        {
                            case -1:
                                break;
                            case 1:
                                weaponService.ChangeMagazineCapacity(ak47);
                                break;
                            case 2:
                                weaponService.AddOrRemoveBullets(ak47);
                                break;
                        }
                        break;
                }
            }
        }
    }
}
