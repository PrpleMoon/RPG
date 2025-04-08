using UnityEngine;
using TMPro;
using System.IO;

public class Version : MonoBehaviour
{
    void Start()
    {
        string localVersionPath = Application.persistentDataPath + "/version.txt";
        string versionContent = File.ReadAllText(localVersionPath);
        // Set the text to the content of the version.txt file
        gameObject.GetComponent<TextMeshProUGUI>().text = versionContent;

    }

}
