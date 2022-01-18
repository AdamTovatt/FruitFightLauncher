using FruitFightLauncher.Helpers;
using FruitFightLauncher.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FruitFightLauncher
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(async () => { await Start(); }).Wait();
        }

        private static async Task Start()
        {
            List<Release> releases = await ApiHelper.GetReleasesAsync();
            Release latestRelease = releases.OrderByDescending(x => x.Id).FirstOrDefault();

            FileHelper fileHelper = new FileHelper();
        }
    }
}
