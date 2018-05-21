using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class Inventory : MonoBehaviour {

    private static Inventory _instance;
    public static Inventory Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.Find("Canvas/Inventory").GetComponent<Inventory>();
            return _instance;
        }
    }

    public List<InventoryItemGrid> itemGridList = new List<InventoryItemGrid>();
    
	void Awake () {

        _instance = this;

	}
	
    public void Show()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.4f);
        transform.localPosition = new Vector3(589, 0, 0);
        transform.DOLocalMove(Vector3.zero, 0.4f);
    }

    public void Hide()
    {
        transform.DOLocalMove(new Vector3(589, 0, 0), 0.4f);
    }
}
