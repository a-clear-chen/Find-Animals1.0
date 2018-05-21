using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.Find("AudioSources").GetComponent<AudioManager>();
            return _instance;
        }
    }

    //音乐资源的名称
    public const string Sound_Prefix = "Sounds/";
    public const string Sound_Begin = "bgmbegin";
    public const string Sound_False = "bgmfalse";
    public const string Sound_Win = "bgmwin";
    public const string Sound_Normal = "bgmnormal";
    public const string Sound_Button = "button";
    public const string Sound_Click = "click";

    //动态添加的AudioSource组件
    private AudioSource bgAudioSource;
    private AudioSource normalAudioSource;


    // Use this for initialization
    void Awake () {
        bgAudioSource = gameObject.AddComponent<AudioSource>();
        normalAudioSource = gameObject.AddComponent<AudioSource>();

        //PlayNormalSound(Sound_Begin);
	}

    void Start()
    {
        _instance = this;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="soundName"></param>
    public void PlayBGSound(string soundName)
    {
        PlaySound(bgAudioSource, LoadSound(soundName), 0.3f, true);
    }

    public void StopBGSound()
    {
        StopSound(bgAudioSource);
    }
    /// <summary>
    /// 播放其它音乐
    /// </summary>
    /// <param name="soundName"></param>
    public void PlayNormalSound(string soundName)
    {
        PlaySound(normalAudioSource, LoadSound(soundName), 1f);
    }

    public void StopNormalSound()
    {
        StopSound(normalAudioSource);
    }


    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="audioSource"></param>
    /// <param name="audioClip"></param>
    /// <param name="volume"></param>
    /// <param name="loop"></param>
    private void PlaySound(AudioSource audioSource,AudioClip clip,float volume,bool loop=false)
    {
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.loop = loop;
        audioSource.Play();
    }

    private void StopSound(AudioSource audioSource)
    {
        audioSource.Stop();
    }

    /// <summary>
    /// 按路径加载音效
    /// </summary>
    /// <param name="soundsName"></param>
    /// <returns></returns>
    private AudioClip LoadSound(string soundsName)
    {
        return Resources.Load<AudioClip>(Sound_Prefix + soundsName);
    }

}
