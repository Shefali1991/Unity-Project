using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;


public class LoadVideo : MonoBehaviour {
    private VideoPlayer videoPlayer;
    private VideoSource videoSource;
    private AudioSource audioSource;

    private UnityEngine.Object[] clip;

    private string[] files;
    private string pathPreFix;
    private string vide_url;
    // Use this for initialization
    void Start()
    {
        Application.runInBackground = true;

        // Function to load files from Android File System
        string path = "sdcard/MyFamilyResources/Videos/";
        pathPreFix = @"file://";

        files = System.IO.Directory.GetFiles(path, "*.*");
        vide_url = files[0];

        StartCoroutine(LoadVideoFile());

    }

    IEnumerator LoadVideoFile()
    {
        videoPlayer = gameObject.AddComponent<VideoPlayer>();

        //Add AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();

        //Disable Play on Awake for both Video and Audio
        videoPlayer.playOnAwake = false;
        audioSource.playOnAwake = false;
        audioSource.Pause();

        //Vide clip from Url
        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = pathPreFix + vide_url; // Changes here for Gear VR and windows  -- add path prefix

        //Set Audio Output to AudioSource
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;

        //Assign the Audio from Video to AudioSource to be played
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioSource);

        //prepare Audio to prevent Buffering
        videoPlayer.Prepare();

        //Wait until video is prepared
        while (!videoPlayer.isPrepared)
        {
            yield return null;
        }

        //Assign the Texture from Video to quad to be displayed
        gameObject.GetComponent<Renderer>().material.mainTexture = videoPlayer.texture;
        yield return null;
        
    }
    
}
