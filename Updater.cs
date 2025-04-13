using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.IO;
using System.Net;
using System;
using TMPro;

public class Updater : MonoBehaviour
{
    private string localVersionPath = Path.Combine(Application.dataPath, "../version.txt");
    private string remoteVersionUrl = "https://raw.githubusercontent.com/PrpleMoon/RPG/refs/heads/main/version.txt";
    public TMP_Text updateMessage;

    public void OnClick()
    {
        StartCoroutine(CheckVersion());
    }

    IEnumerator CheckVersion()
    {
        // Read local version
        float localVersion = 0f;
        if (File.Exists(localVersionPath))
        {
            string localVersionString = File.ReadAllText(localVersionPath);
            float.TryParse(localVersionString.Trim(), out localVersion);
        }
        else
        {
            updateMessage.text = "Local version file not found.";
            Debug.LogWarning("Local version file not found: " + localVersionPath);
        }

        // Download remote version
        using (UnityWebRequest www = UnityWebRequest.Get(remoteVersionUrl))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                updateMessage.text = "Failed to download remote version: " + www.error;
            }
            else
            {
                float remoteVersion = 0f;
                float.TryParse(www.downloadHandler.text.Trim(), out remoteVersion);

                updateMessage.text = $"Local version: {localVersion} | Remote version: {remoteVersion}";

                // Compare versions
                if (localVersion < remoteVersion)
                {
                    updateMessage.text = "Update available! Downloading...";
                    // Put your update logic here
                }
                else
                {
                    updateMessage.text = "Game is up to date.";
                }
            }
        }
    }
}
