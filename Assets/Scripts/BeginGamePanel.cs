using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class BeginGamePanel : MonoBehaviour {

    private Button startButton;            //开始游戏按钮
    private Button helpButton;             //游戏说明按钮
    private GameObject helpPanel;          //游戏说明页面
    private Button containButton;          //游戏说明页面的确定按钮


	// Use this for initialization
	void Start () {

        startButton = transform.Find("StartButton").GetComponent<Button>();
        startButton.onClick.AddListener(OnStartButtonClick);

        helpButton = transform.Find("HelpButton").GetComponent<Button>();
        helpButton.onClick.AddListener(OnHelpButtonClick);

        helpPanel = transform.Find("HelpPanel").gameObject;
        helpPanel.SetActive(false);
        containButton = transform.Find("HelpPanel/ContainButton").GetComponent<Button>();
        containButton.onClick.AddListener(OnContainButtonClick);

        AudioManager.Instance.PlayBGSound(AudioManager.Sound_Begin);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnStartButtonClick()
    {
        gameObject.SetActive(false);
        AudioManager.Instance.PlayNormalSound(AudioManager.Sound_Button);
        AudioManager.Instance.PlayBGSound(AudioManager.Sound_Normal);
        Destroy(this.gameObject);
    }

    private void OnHelpButtonClick()
    {
        AudioManager.Instance.PlayNormalSound(AudioManager.Sound_Button);
        startButton.gameObject.SetActive(false);
        helpButton.gameObject.SetActive(false);
        helpPanel.SetActive(true);
    }

    private void OnContainButtonClick()
    {
        AudioManager.Instance.PlayNormalSound(AudioManager.Sound_Button);
        helpPanel.SetActive(false);
        startButton.gameObject.SetActive(true);
        helpButton.gameObject.SetActive(true);
    }
}
