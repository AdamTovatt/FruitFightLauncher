using FruitFightLauncher.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FruitFightLauncher.Helpers
{
    public class FileHelper
    {
        public static string AssemblyPath
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return path;
            }
        }

        public static string AssemblyName
        {
            get
            {
                return Path.GetFileName(AssemblyPath);
            }
        }

        private const string sakurDirectoryName = "Sakur";
        private const string gameInstallDirectoryName = "FruitFightGame";
        private const string launcherInstallDirectoryName = "FruitFightLauncher";

        private string appdataPath;
        private string settingsPath;
        private InstallationSettings settings;

        public FileHelper()
        {
            appdataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            settingsPath = Path.Combine(appdataPath, sakurDirectoryName, launcherInstallDirectoryName, "settings.json");

            if (!GetLauncherInstalled())
            {
                InstallLauncher();
            }
        }

        public InstallationSettings LoadSettings()
        {
            string json = File.ReadAllText(settingsPath);
            return InstallationSettings.FromJson(json);
        }

        public void SaveSettings()
        {
            if (settings != null)
            {
                File.WriteAllText(settingsPath, settings.ToJson());
            }
        }

        private void InstallLauncher()
        {
            string launcherDirectory = Path.Combine(appdataPath, sakurDirectoryName, launcherInstallDirectoryName);

            if (!Directory.Exists(launcherDirectory))
            {
                Directory.CreateDirectory(launcherDirectory);
            }

            File.Copy(AssemblyPath, Path.Combine(launcherDirectory, AssemblyName));
        }

        private void AssureBaseDirectory()
        {
            string sakurDirectoryPath = Path.Combine(appdataPath, sakurDirectoryName);

            if (!Directory.Exists(sakurDirectoryPath))
            {
                Directory.CreateDirectory(sakurDirectoryPath);
            }
        }

        private bool GetGameInstalled()
        {
            string appdataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string sakurDirectoryPath = Path.Combine(appdataPath, sakurDirectoryName);
            string gameDirectoryPath = Path.Combine(sakurDirectoryPath, gameInstallDirectoryName);

            AssureBaseDirectory();

            return Directory.Exists(gameDirectoryPath) && Directory.GetFiles(gameDirectoryPath).Length > 0;
        }

        private bool GetLauncherInstalled()
        {
            string appdataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string sakurDirectoryPath = Path.Combine(appdataPath, sakurDirectoryName);
            string launcherDirectoryPath = Path.Combine(sakurDirectoryPath, launcherInstallDirectoryName);

            AssureBaseDirectory();

            return Directory.Exists(launcherDirectoryPath) && Directory.GetFiles(launcherDirectoryPath).Length > 0;
        }
    }
}
