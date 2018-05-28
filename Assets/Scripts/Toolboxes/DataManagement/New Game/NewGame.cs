using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class NewGame : MonoBehaviour {
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(DeleteSaveData); //(delegate { ApplicationManager.GetInstance().GetLevelManager().LoadScene(scene); });
    }

    private void DeleteSaveData()
    {
        DeleteTempFiles();
        DeletePermFiles();
        ApplicationManager.GetInstance().GetLevelManager().LoadScene("Demoburgh");
    }

    private void DeleteTempFiles()
    {
        string tempDir = Application.persistentDataPath + "/temp";

        if (Directory.Exists(tempDir))
        {
            string[] files = Directory.GetFiles(tempDir);

            foreach (string s in files)
            {
                File.Delete(s);
            }
        }
        else
        {
            Debug.Log("tempDir not found");
        }
    }

    private void DeletePermFiles()
    {
        string permDir = Application.persistentDataPath + "/perm";

        if (Directory.Exists(permDir))
        {
            string[] files = Directory.GetFiles(permDir);

            foreach (string s in files)
            {
                File.Delete(s);
            }
        }
        else
        {
            Debug.Log("tempDir not found");
        }
    }
}
