using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class GameOver : MonoBehaviour {

    private Button beginAgainButton;
    private Button exitGameButton;

    private GameObject beginGamePanel;
    private Transform canvas;

    //private UIManager uiManager = new UIManager();

	// Use this for initialization
	void Start () {

        beginGamePanel = Resources.Load("UIPanels/BeginGamePanel") as GameObject;
        canvas = GameObject.Find("Canvas").transform;

        beginAgainButton = transform.Find("BeginAgain").GetComponent<Button>();
        beginAgainButton.onClick.AddListener(OnBeginAgainButtonClick);

        exitGameButton = transform.Find("ExitGame").GetComponent<Button>();
        exitGameButton.onClick.AddListener(OnExitGameClick);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnBeginAgainButtonClick()
    {
        AudioManager.Instance.PlayNormalSound(AudioManager.Sound_Button);
        gameObject.SetActive(false);
        GameObject go = Instantiate(beginGamePanel, new Vector3(0, 0, 0), Quaternion.identity);
        go.transform.SetParent(canvas,false);
        UIManager.Instance.InitGame();
    }

    private void OnExitGameClick()
    {
        AudioManager.Instance.PlayNormalSound(AudioManager.Sound_Button);
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }
}
