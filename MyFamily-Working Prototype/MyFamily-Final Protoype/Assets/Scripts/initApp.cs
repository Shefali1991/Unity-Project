using UnityEngine;
using System.IO;
using System.Collections;

using UnityEngine.SceneManagement;


public class initApp : MonoBehaviour
{
    private string Dir;
    private string ImgDir;
    private string MovDir;
    private string AudioDir;

    // Use this for initialization
    void Start()
    {
        Dir = "/sdcard/MyFamilyResources";
        ImgDir = "/sdcard/MyFamilyResources/Images";
        MovDir = "/sdcard/MyFamilyResources/Videos";
        AudioDir = "/sdcard/MyFamilyResources/Audios";
        if (!Directory.Exists(Dir))
        {
            Directory.CreateDirectory(Dir);
            if (!Directory.Exists(ImgDir))
            {
                Directory.CreateDirectory(ImgDir);
            }
            if (!Directory.Exists(MovDir))
            {
                Directory.CreateDirectory(MovDir);
            }
            if (!Directory.Exists(AudioDir))
            {
                Directory.CreateDirectory(AudioDir);
            }
        }
    }
}
