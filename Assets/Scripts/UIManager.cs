using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.Video;

public class UIManager : MonoBehaviour {

    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.Find("Canvas").GetComponent<UIManager>();
            return _instance;
        }
    }

    //面板
    private GameObject normalHorse;
    private GameObject choose;
    private GameObject chooseTrue;
    private GameObject chooseFalse;
    private GameObject winPanel;
    private GameObject failPanel;
    private Transform canvas;

    private GameObject rawImage;
    private VideoController videoController;
    private float endTime = -0.5f;
    public bool play = false;
    
    //游戏物体
    private GameObject player;
    private GameObject cat;
    private GameObject zebra;
    private GameObject cameraOne;

    //各面板的初始坐标
    private Vector3 normalHorseOriginalPos;
    private Vector3 chooseOriginalPos;
    private Vector3 chooseTrueOriginalPos;
    private Vector3 chooseFalseOriginalPos;
    private Vector3 winPanelOriginalPos;
    private Vector3 failPanelOriginalPos;
    
    private Button quitButton;                 //退出游戏按钮     

    public int totalCount = 0;                 //回答数
    public int winCount = 0;                   //回答正确数

    void Awake()
    {
        _instance = this;
        canvas = GameObject.Find("Canvas").transform;
        InitGame();
    }

    // Use this for initialization
    void Start () {



        rawImage = transform.Find("RawImage").gameObject;
        rawImage.SetActive(false);

        quitButton = GameObject.Find("QuitButton").GetComponent<Button>();
        quitButton.onClick.AddListener(OnQuitButtonClick);


        normalHorseOriginalPos = new Vector3(-335.5f, 0, 0);
        normalHorse = Instantiate(Resources.Load("UIPanels/NormalHorse") as GameObject, normalHorseOriginalPos, Quaternion.identity);
        //normalHorse.SetActive(false);
        normalHorse.transform.SetParent(canvas, false);

        chooseOriginalPos = Vector3.zero;
        choose = Instantiate(Resources.Load("UIPanels/Choose") as GameObject, chooseTrueOriginalPos, Quaternion.identity);
        choose.SetActive(false);
        choose.transform.SetParent(canvas, false);

        chooseTrueOriginalPos = Vector3.zero;
        chooseTrue = Instantiate(Resources.Load("UIPanels/ChooseTrue") as GameObject, chooseTrueOriginalPos, Quaternion.identity);
        chooseTrue.SetActive(false);
        chooseTrue.transform.SetParent(canvas, false);

        chooseFalseOriginalPos = Vector3.zero;
        chooseFalse = Instantiate(Resources.Load("UIPanels/ChooseFalse") as GameObject, chooseFalseOriginalPos, Quaternion.identity);
        chooseFalse.SetActive(false);
        chooseFalse.transform.SetParent(canvas, false);
        

        winPanelOriginalPos = Vector3.zero;
        winPanel = Instantiate(Resources.Load("UIPanels/WinPanel") as GameObject, winPanelOriginalPos, Quaternion.identity);
        winPanel.SetActive(false);
        winPanel.transform.SetParent(canvas, false);

        failPanelOriginalPos = Vector3.zero;
        failPanel = Instantiate(Resources.Load("UIPanels/FailPanel") as GameObject, failPanelOriginalPos, Quaternion.identity);
        failPanel.SetActive(false);
        failPanel.transform.SetParent(canvas, false);
        

    }
	
    void Update()
    {
        if(play && videoController.enabled==true)
        {
            videoController.PlayVideo();
            normalHorse.SetActive(false);
            AudioManager.Instance.StopBGSound();
            endTime += Time.deltaTime;
            if (endTime >= videoController.playTime)
            {
                normalHorse.SetActive(true);
                AudioManager.Instance.PlayBGSound(AudioManager.Sound_Normal);
                videoController.enabled = false;
                rawImage.SetActive(false);
                endTime = -0.5f;
                //Destroy(rawImage);
                play = false;
            }
        }
    }

    /// <summary>
    /// 初始化游戏
    /// </summary>
    public void InitGame()
    {
        
        cat = Instantiate(Resources.Load("Prefabs/Cat") as GameObject, new Vector3(353.27f, 1.867536f, 167.1722f), Quaternion.identity);
        zebra = Instantiate(Resources.Load("Prefabs/Zebra") as GameObject, new Vector3(295.9258f, 1.290684f, 231.3642f), Quaternion.identity);
        player = Instantiate(Resources.Load("Prefabs/Player") as GameObject, new Vector3(270.285f, 1.095648f, 187.3844f), Quaternion.identity);

        cameraOne = GameObject.Find("Main Camera");
        cameraOne.transform.position = new Vector3(270.5293f, 3.140375f, 183.5741f);
        cameraOne.AddComponent<FollowTarget>();

        canvas.gameObject.AddComponent<VideoController>();
        videoController = GetComponent<VideoController>();

        totalCount = 0;
        winCount = 0;

    }
    /// <summary>
    /// 销毁游戏物体
    /// </summary>
    public void DestroyGame()
    {
        cameraOne.GetComponent<FollowTarget>().enabled = false;
        Destroy(player);
        Destroy(cat);
        Destroy(zebra);
    }

    

    public void NormalHorseEnter()
    {
        //normalHorse.SetActive(true);
        normalHorse.transform.localPosition = new Vector3(-520, 48, 0);
        StartCoroutine(MoveObject_Time(normalHorse, normalHorse.transform.localPosition, new Vector3(-324,48,0), 1.5f));
        //normalHorse.transform.localPosition = Vector3.Lerp(normalHorse.transform.localPosition, normalHorseOriginalPos, Time.time);
    }
    public void NormalHorseExit()
    {
        if(normalHorse!=null)
        {
            //normalHorse.SetActive(false);
            //StartCoroutine(MoveObject_Time(normalHorse, normalHorse.transform.localPosition, new Vector3(-520, 48, 0), 2));
            normalHorse.transform.localPosition = Vector3.Lerp(normalHorse.transform.localPosition, new Vector3(-520, 48, 0), Time.time);
        }
    }

    public void ChooseEnter()
    {
        choose.SetActive(true);
        choose.transform.localPosition = new Vector3(600, 0, 0);
        StartCoroutine(MoveObject_Time(choose, choose.transform.localPosition, Vector3.zero, 1.5f));
    }

    public void ChooseTrueEnter()
    {
        chooseTrue.SetActive(true);
        chooseTrue.transform.localPosition = new Vector3(0, 340, 0);
        StartCoroutine(MoveObject_Time(chooseTrue, chooseTrue.transform.localPosition, Vector3.zero, 1));
        //chooseTrue.transform.localPosition = Vector3.Lerp(chooseTrue.transform.localPosition, chooseTrueOriginalPos, Time.time);
        Invoke("ChooseTrueExit", 3f);
    }
    public void ChooseTrueExit()
    {
        StartCoroutine(MoveObject_Time(chooseTrue, chooseTrue.transform.localPosition,new Vector3(0, 340, 0), 1));
        //chooseTrue.transform.localPosition = Vector3.Lerp(chooseTrue.transform.localPosition, new Vector3(0, 340, 0), Time.time);
    }

    public void ChooseFalseEnter()
    {
        chooseFalse.SetActive(true);
        chooseFalse.transform.localPosition = new Vector3(0, 340, 0);
        StartCoroutine(MoveObject_Time(chooseFalse, chooseFalse.transform.localPosition, Vector3.zero, 1));
        //chooseFalse.transform.localPosition = Vector3.Lerp(chooseFalse.transform.localPosition, chooseFalseOriginalPos, Time.time);
        Invoke("ChooseFalseExit", 1.2f);
    }
    public void ChooseFalseExit()
    {
        StartCoroutine(MoveObject_Time(chooseFalse, chooseFalse.transform.localPosition, new Vector3(0, 340, 0), 2));
        //chooseFalse.transform.localPosition = Vector3.Lerp(chooseFalse.transform.localPosition, new Vector3(0, 340, 0), Time.time);
    }

    public void WinPanelEnter()
    {
        AudioManager.Instance.PlayNormalSound(AudioManager.Sound_Win);
        winPanel.SetActive(true);
        winPanel.transform.localPosition = new Vector3(0, 400, 0);
        StartCoroutine(MoveObject_Time(winPanel, winPanel.transform.localPosition, Vector3.zero, 2));
        //winPanel.transform.localPosition = Vector3.Lerp(winPanel.transform.localPosition, winPanelOriginalPos, Time.time);
    }
    public void WinPanelExit()
    {
        StartCoroutine(MoveObject_Time(winPanel, winPanel.transform.localPosition, new Vector3(0, 400, 0), 2));
        //winPanel.transform.localPosition = Vector3.Lerp(winPanel.transform.localPosition, new Vector3(0, 400, 0), Time.time);
    }

    public void FailPanelEnter()
    {
        AudioManager.Instance.PlayNormalSound(AudioManager.Sound_False);
        failPanel.SetActive(true);
        failPanel.transform.localPosition = new Vector3(0, 400, 0);
        StartCoroutine(MoveObject_Time(failPanel, failPanel.transform.localPosition, Vector3.zero, 2));
        //failPanel.transform.localPosition = Vector3.Lerp(failPanel.transform.localPosition, failPanelOriginalPos, Time.time);
    }
    public void FailPanelExit()
    {
        StartCoroutine(MoveObject_Time(failPanel, failPanel.transform.localPosition, new Vector3(0, 400, 0), 2));
        //failPanel.transform.localPosition = Vector3.Lerp(failPanel.transform.localPosition, new Vector3(0, 400, 0), Time.time);
    }

    private void OnQuitButtonClick()
    {
        AudioManager.Instance.PlayNormalSound(AudioManager.Sound_Button);
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }
    /// <summary>
    /// 一定时间移动到目标位置
    /// </summary>
    /// <param name="startPos"></param>
    /// <param name="endPos"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    private IEnumerator MoveObject_Time(GameObject go,Vector3 startPos,Vector3 endPos,float time)
    {
        float dur = 0.0f;
        while(dur<time)
        {
            dur += Time.deltaTime;
            go.transform.localPosition = Vector3.Lerp(startPos, endPos, dur / time);
            yield return null;
        }
    }
}
