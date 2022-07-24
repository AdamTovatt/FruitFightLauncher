using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitFightLauncher.Models
{
    public class ProgressReporter
    {
        public delegate void DownloadProgressChangedHandler(int percentage);
        public event DownloadProgressChangedHandler OnDownloadProgressChanged;

        public ProgressReporter() { }

        public void ReportProgress(int newPercentage)
        {
            OnDownloadProgressChanged?.Invoke(newPercentage);
        }
    }
}
