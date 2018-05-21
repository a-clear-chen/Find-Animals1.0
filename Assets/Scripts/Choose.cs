using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Choose : MonoBehaviour {

    public Toggle[] gos;        //ToggleGroup，管理所有的Toggle              
    private Button close;       //关闭按钮
    private Button contain;     //确定按钮
    private int toggleEnum = 0; //Toggle在gos的索引，与animalEnum进行比较，判断选择的动物是否正确
    private bool loadGame = false; //重载游戏
    

	// Use this for initialization
	void Start () {

        gos = GetComponentsInChildren<Toggle>();
        gos[0].onValueChanged.AddListener(OnValueHorse);
        gos[1].onValueChanged.AddListener(OnValueZebra);

        close = transform.Find("Close").GetComponent<Button>();
        close.onClick.AddListener(OnCloseClick);
        contain = transform.Find("Contain").GetComponent<Button>();
        contain.onClick.AddListener(OnContainClick);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnValueHorse(bool check)
    {
        if(check)
        {
            toggleEnum = 1;
        }
    }

    void OnValueZebra(bool check)
    {
        if (check)
        {
            toggleEnum = 2;
        }
    }
    /// <summary>
    /// 切换全部不勾选
    /// </summary>
    private void ToggleFalse()
    {
        foreach(Toggle value in gos)
        {
            value.isOn = false;
        }
    }

    void OnCloseClick()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(600, 0, 0), Time.time);
        AudioManager.Instance.PlayNormalSound(AudioManager.Sound_Button);
    }

    void OnContainClick()
    {
        Invoke("ToggleFalse", 2f);                                        //按确定键2秒后，切换全部不勾选
        AudioManager.Instance.PlayNormalSound(AudioManager.Sound_Button);
        gameObject.SetActive(false);
        UIManager.Instance.totalCount++;
        if(toggleEnum==PlayerController.animalEnum)
        {
            UIManager.Instance.ChooseTrueEnter();
            UIManager.Instance.winCount++;
        }
        else
        {
            UIManager.Instance.ChooseFalseEnter();
        }
        if(UIManager.Instance.winCount >= 2 && UIManager.Instance.totalCount >= 2)
        {
            UIManager.Instance.WinPanelEnter();
            loadGame = true;
        }
        if(UIManager.Instance.totalCount >= 2 && UIManager.Instance.winCount < 2)
        {
            UIManager.Instance.FailPanelEnter();
            loadGame = true;
        }
        if(loadGame)
        {
            loadGame = false;
            Invoke("Destroy", 1f);
        }
    }
    /// <summary>
    /// 重载游戏前销毁游戏物体
    /// </summary>
    private void Destroy()
    {
        UIManager.Instance.DestroyGame();
    }
}
