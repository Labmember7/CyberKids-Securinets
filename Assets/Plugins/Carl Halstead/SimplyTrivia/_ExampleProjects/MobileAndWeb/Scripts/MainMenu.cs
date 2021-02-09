using System.Runtime.InteropServices;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenuCanvas;
    [SerializeField] GameObject mainGameCanvas;

    public void OpenPage()
    {
        if(Application.platform == RuntimePlatform.WebGLPlayer)
        {
            if(Application.isEditor == false)
            {
                openPage("https://www.assetstore.unity3d.com/#!/content/102628?aid=1101l3ozs");
                return;
            }
        }

        Application.OpenURL("https://www.assetstore.unity3d.com/#!/content/102628?aid=1101l3ozs");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    [DllImport("__Internal")]
    private static extern void openPage(string url);
}
