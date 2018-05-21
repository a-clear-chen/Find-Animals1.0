using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse : MonoBehaviour {

    private float randomRot = 0;             //随机旋转角度
    private float frequence = 2;             //随机旋转频率
    private float click = 0;                 //计时器
    private float speed = 3;                 //移动速度
    private bool move = false;               //旋转是否完成

    private Animator anim;
    private int walkId = Animator.StringToHash("Walk");

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        click += Time.deltaTime;
        if(click>=frequence)
        {
            RandomMove();
            click = 0;
        }
        if(move && gameObject.GetComponent<Collider>().enabled==true)
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}

    private void RandomMove()
    {
        randomRot = Random.Range(1, 60);
        //randomTime = Random.Range(0.1f, 3f);
        transform.Rotate(new Vector3(0, randomRot, 0));
        anim.SetBool(walkId, true);
        move = true;
    }
    
}
