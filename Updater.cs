using System.Net;
using System.IO;
using UnityEngine;
using System.Diagnostics;
using System.IO.Compression;

public class Updater : MonoBehaviour
{
    string localVersionPath = Application.persistentDataPath + "/version.txt";
    string serverVersionUrl = "https://yourserver.com/version.txt";

    void CheckForUpdates()
    {
        WebClient webClient = new WebClient();
        string serverVersion = webClient.DownloadString(serverVersionUrl).Trim();
        string localVersion = File.Exists(localVersionPath) ? File.ReadAllText(localVersionPath).Trim() : "0.0.0";

        if (serverVersion != localVersion)
        {
            StartUpdateProcess(serverVersion);
        }

        void StartUpdateProcess(string newVersion)
        {
            string updateUrl = "https://yourserver.com/game_update.zip";
            string tempZipPath = Application.persistentDataPath + "/game_update.zip";

            WebClient webClient = new WebClient();
            webClient.DownloadFile(updateUrl, tempZipPath);

            // Extract files
            string extractPath = Application.dataPath; // Or another directory
            ZipFile.ExtractToDirectory(tempZipPath, extractPath, true);

            // Update local version file
            File.WriteAllText(localVersionPath, newVersion);

            // Relaunch game
            RelaunchGame();
        }


        void RelaunchGame()
        {
            string executablePath = Application.dataPath + "/YourGame.exe";
            Process.Start(executablePath);
            Application.Quit();
        }

    }
}