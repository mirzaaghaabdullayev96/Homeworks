using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Weaponry.Business.SoundEffects
{
    public static class SoundHandling
    {
        public static void SingleShot()
        {
            string audioFilePath = "TunePocket-Single-Shot-AK-47-1-Preview.wav";
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SoundEffects", audioFilePath);
            SoundPlayer player = new SoundPlayer(fullPath);
            player.PlaySync();
        }

        public static void AutomaticShot()
        {
            string audioFilePath = "ak-47-firing-8760.wav";
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SoundEffects", audioFilePath);
            SoundPlayer player = new SoundPlayer(fullPath);
            player.PlaySync();
        }

        public static void Reload()
        {
            string audioFilePath = "reload-ak-47-made-with-Voicemod.wav";
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SoundEffects", audioFilePath);
            SoundPlayer player = new SoundPlayer(fullPath);
            player.PlaySync();
        }

    }
}
