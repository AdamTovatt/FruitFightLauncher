using FruitFightLauncher.Helpers;
using FruitFightLauncher.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FruitFightLauncher
{
    public partial class FruitFightLauncher : Form
    {
        public FruitFightLauncher()
        {
            InitializeComponent();
        }

        private async void FruitFightLauncherLoad(object sender, EventArgs e)
        {
            statusText.Text = "Checking for updates";

            List<Release> releases = await ApiHelper.GetReleasesAsync();
            Release latestRelease = releases.Where(x => x.Assets.Count > 0).OrderByDescending(x => x.Id).FirstOrDefault();

            statusText.Text = "Setting up local files";

            FileHelper fileHelper = new FileHelper();

            if (!fileHelper.Settings.GameInstalled || fileHelper.Settings.GameVersionId != latestRelease.Id)
            {
                statusText.Text = "Downloading latest release";
                string savedFile = await fileHelper.DownloadGame(latestRelease);

                statusText.Text = "Unpacking";
                await fileHelper.InstallGame(latestRelease, savedFile);       
            }

            statusText.Text = "Launching game";

            Process.Start(Path.Combine(fileHelper.GameInstallDirectory, "FFBuild", "FruitFight.exe"));

            Close();
        }
    }
}
