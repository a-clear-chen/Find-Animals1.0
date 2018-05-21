using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Video;

public class VideoController : MonoBehaviour {

    //public static bool play = false;

    private GameObject rawImage;
    private VideoPlayer videoPlayer;
    public float playTime = 1000f;

	// Use this for initialization
	void Start () {
        rawImage = transform.Find("RawImage").gameObject;
        videoPlayer = rawImage.GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += EndReached;
        rawImage.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        /*
		if(play)
        {
            PlayVideo();
            endTime += Time.deltaTime;
            if(endTime>=playTime)
            {
                rawImage.SetActive(false);
                gameObject.GetComponent<VideoPlayer>().enabled = false;
                //Destroy(rawImage);
                play = false;
            }
        }
        */
	}

    public void PlayVideo()
    {
        rawImage.SetActive(true);
        videoPlayer.Play();
        Time.timeScale = 1;
    }

    public void EndReached(VideoPlayer videoPlayer)
    {
        playTime = (float)videoPlayer.time;
    }

}
