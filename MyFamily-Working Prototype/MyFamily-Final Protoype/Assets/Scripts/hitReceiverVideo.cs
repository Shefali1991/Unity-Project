using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Video;

public class hitReceiverVideo : MonoBehaviour {

    private VideoPlayer videoPlayer;
    private AudioSource audioSource;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayVideo()
    {
        videoPlayer = gameObject.GetComponent<VideoPlayer>();
        audioSource = gameObject.GetComponent<AudioSource>();
        //Play Video
        videoPlayer.Play();
        audioSource.Play();
    }

    public void StopVideo()
    {
        videoPlayer = gameObject.GetComponent<VideoPlayer>();
        audioSource = gameObject.GetComponent<AudioSource>();
        videoPlayer.Pause();
        audioSource.Pause();
    }
}
