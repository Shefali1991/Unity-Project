using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;


public class LoadImage : MonoBehaviour
{

    private Texture2D[] textList;
    public AudioSource[] audioSrcs;
    private string[] audio_names;

    private int frameCount;

    // Use this for initialization
    void Start()
    {
        frameCount = gameObject.transform.childCount;

        FetchImages();
        StartCoroutine(LoadQuadTexture());
    }

    IEnumerator LoadQuadTexture()
    {
        Vector3 frame_size;
        double ScalingFactor;
        int index = 0;
        

        foreach (Transform frame in transform)
        {

            GameObject background = frame.transform.GetChild(0).gameObject;
            GameObject picture = frame.transform.GetChild(1).gameObject;
            
            frame_size = background.transform.localScale;
            double BG_quad_width = frame_size.x;
            double BG_quad_height = frame_size.y;

            Texture2D tex = textList[index];
            double Tex_width = tex.width;
            double Tex_height = tex.height;

            double Background_ratio = GetRatio(BG_quad_width, BG_quad_height);
            double Texture_ratio = GetRatio(Tex_width, Tex_height);

            if (Background_ratio < Texture_ratio)
            {
                // Fit texture to Background by width
                ScalingFactor = BG_quad_width / (double)Tex_width;
            }
            else
            {
                // Fit texture to Background by height
                ScalingFactor = BG_quad_height / (double)Tex_height;
            }

            picture.transform.localScale = new Vector3((float)Tex_width * (float)ScalingFactor, (float)Tex_height * (float)ScalingFactor, 1);
            picture.GetComponent<Renderer>().material.mainTexture = tex;

            audioSrcs[index] = picture.AddComponent<AudioSource>();
            //yield return null;

            WWW www = new WWW("file://sdcard/MyFamilyResources/Audios/" + audio_names[index]);

            yield return www; // code will wait till file is completely read

            audioSrcs[index].clip = www.GetAudioClip(false);
            
            //all Incrementations
            index++;
        }
    }
    

    void FetchImages()
    {

        //get file names from input text file.
        string[] lines = System.IO.File.ReadAllLines("sdcard/MyFamilyResources/ImageAudio.txt");

        string[] image_names = new string[lines.Length];
                 audio_names = new string[lines.Length];
        
        int i = 0;

        foreach (string line in lines)
        {
            // Use a tab to indent each line of the file.
            string[] data = line.Split(' ');
            image_names[i] = data[0];
            audio_names[i] = data[1];
            
            i++;
        }

        // Function to load images from Android File System
        string path1 = "sdcard/MyFamilyResources/Images/";
        string pathPreFix = @"file://";

        textList = new Texture2D[image_names.Length];// init the list of textures.
        audioSrcs = new AudioSource[audio_names.Length];

        int index = 0;
        while (index < frameCount)
        {
            WWW www = new WWW(pathPreFix + path1 + image_names[index]);
            //yield return www;
            ExifLib.JpegInfo jpi = ExifLib.ExifReader.ReadJpeg(www.bytes, (index + 1).ToString());

            string orientation = jpi.Orientation.ToString();
            if (orientation == "TopRight")
            {
                textList[index] = rotate90(www.texture);
            }
            else if (orientation == "BottomLeft")
            {
                textList[index] = rotateMinus90(www.texture);
            }
            else //if (orientation == "TopLeft")
            {
                textList[index] = www.texture;
            }

            index++;
        }
    }

    Texture2D rotate90(Texture2D originalTexture)
    {
        Color32[] original = originalTexture.GetPixels32();
        Color32[] rotated = new Color32[original.Length];
        int w = originalTexture.width;
        int h = originalTexture.height;

        int iRotated, iOriginal;


        for (int j = 0; j < h; ++j)
        {
            for (int i = 0; i < w; ++i)
            {
                iRotated = (i + 1) * h - j - 1;
                iOriginal = original.Length - 1 - (j * w + i);
                rotated[iRotated] = original[iOriginal];
            }
        }

        Texture2D rotatedTexture = new Texture2D(h, w);
        rotatedTexture.SetPixels32(rotated);
        rotatedTexture.Apply();
        return rotatedTexture;

    }

    Texture2D rotateMinus90(Texture2D originalTexture)
    {
        Color32[] original = originalTexture.GetPixels32();
        Color32[] rotated = new Color32[original.Length];
        int w = originalTexture.width;
        int h = originalTexture.height;

        int iRotated, iOriginal;


        for (int j = 0; j < h; ++j)
        {
            for (int i = 0; i < w; ++i)
            {
                iRotated = (i + 1) * h - j - 1;
                iOriginal = j * w + i;
                rotated[iRotated] = original[iOriginal];
            }
        }

        Texture2D rotatedTexture = new Texture2D(h, w);
        rotatedTexture.SetPixels32(rotated);
        rotatedTexture.Apply();
        return rotatedTexture;

    }

    double GetRatio(double width, double height)
    {
        double ratio = (double)width / (double)height;
        return ratio;
    }

    
}