using FruitFightLauncher.Helpers;
using FruitFightLauncher.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            List<Release> releases = await ApiHelper.GetReleasesAsync();
            Release latestRelease = releases.OrderByDescending(x => x.Id).FirstOrDefault();
        }
    }
}
