using System.Net;
using System.IO;
using UnityEngine;
using System.Diagnostics;
using System.IO.Compression;

public class Updater : MonoBehaviour
{
    string localVersionPath;
    string serverVersionUrl = "https://raw.githubusercontent.com/PrpleMoon/RPG/refs/heads/main/version.txt";
    void Start()
    {
        CheckForUpdates();
    }

    void CheckForUpdates()
    {
        string localVersionPath = Application.persistentDataPath + "/version.txt";
        WebClient webClient = new WebClient();
        string serverVersion = webClient.DownloadString(serverVersionUrl).Trim();
        string localVersion = File.Exists(localVersionPath) ? File.ReadAllText(localVersionPath).Trim() : "0.0.0";

        if (serverVersion != localVersion)
        {
            StartUpdateProcess(serverVersion);
        }
        else 
        {
            return;
        }

        void StartUpdateProcess(string newVersion)
        {
            string updateUrl = "https://github.com/PrpleMoon/RPG/raw/refs/heads/main/game.zip";
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
            Process.Start(Application.dataPath + "/RPG.exe");
            Application.Quit();
        }

    }
}