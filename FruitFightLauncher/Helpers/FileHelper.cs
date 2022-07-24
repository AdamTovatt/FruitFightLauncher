using FruitFightLauncher.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
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

        public string GameInstallDirectory { get { return Path.Combine(appdataPath, sakurDirectoryName, gameInstallDirectoryName); } }

        private const string sakurDirectoryName = "Sakur";
        private const string gameInstallDirectoryName = "FruitFightGame";
        private const string launcherInstallDirectoryName = "FruitFightLauncher";

        private string appdataPath;
        private string settingsPath;
        public InstallationSettings Settings { get; set; }

        public FileHelper()
        {
            appdataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            settingsPath = Path.Combine(appdataPath, sakurDirectoryName, launcherInstallDirectoryName, "settings.json");

            if (!GetLauncherInstalled())
            {
                InstallLauncher();
            }

            Settings = LoadSettings();

            if (Settings == null)
            {
                Settings = new InstallationSettings();
                SaveSettings();
            }
        }

        public InstallationSettings LoadSettings()
        {
            if (File.Exists(settingsPath))
            {
                string json = File.ReadAllText(settingsPath);
                return InstallationSettings.FromJson(json);
            }
            else
            {
                return null;
            }
        }

        public void SaveSettings()
        {
            if (Settings != null)
            {
                File.WriteAllText(settingsPath, Settings.ToJson());
            }
        }

        public async Task<string> DownloadGame(Release release, ProgressReporter progressReporter)
        {
            AssureBaseDirectory();

            string gameDirectory = Path.Combine(appdataPath, sakurDirectoryName, gameInstallDirectoryName);

            if (!Directory.Exists(gameDirectory))
            {
                Directory.CreateDirectory(gameDirectory);
            }

            return await ApiHelper.DownloadFileAsync(release.Assets.First().BrowserDownloadUrl, Path.Combine(GameInstallDirectory, "ffbuild.zip"), progressReporter);
        }

        public async Task InstallGame(Release release, string savedFile)
        {
            string installationDirectory = Path.Combine(GameInstallDirectory, "FFBuild");

            if (Directory.Exists(installationDirectory)) //clean up
                Directory.Delete(installationDirectory, true);

            await Task.Run(() => { ZipFile.ExtractToDirectory(savedFile, GameInstallDirectory); });

            Settings.GameInstalled = true;
            Settings.GameVersionId = release.Id;
            SaveSettings();

            File.Delete(savedFile);
        }

        private void InstallLauncher()
        {
            AssureBaseDirectory();

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
