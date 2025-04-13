using UnityEngine;
using TMPro;
using System.IO;

public class Version : MonoBehaviour
{
    void Start()
    {
        string localVersionPath = Path.Combine(Application.dataPath, "../version.txt");
        string versionContent = File.ReadAllText(localVersionPath);
        gameObject.GetComponent<TextMeshProUGUI>().text = versionContent;
    }

}
