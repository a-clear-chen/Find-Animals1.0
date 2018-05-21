using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Animator anim;
    private float speed = 3f;              //主角移动速度
    private Vector3 targetPos;             //目标位置
    private Vector3 offset;                //当前位置和目标位置偏移
    private bool finish = false;            //点击鼠标后主角转向是否完成

    //动物
    private Transform cat;
    private Transform zebra;

    public static int animalEnum = 0;     //动物的枚举


    private int runId = Animator.StringToHash("Run");

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        cat = GameObject.Find("Cat(Clone)").transform;
        zebra = GameObject.Find("Zebra(Clone)").transform;
        //rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        HitAnimals();
        DistanceToAnimals();
    }
    
    void FixedUpdate()
    {
        Move();
    }
    /// <summary>
    /// 按键移动和鼠标移动
    /// </summary>
    private void Move()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            //rigidbody.MovePosition(rigidbody.position + Vector3.forward * speed * Time.deltaTime);
            anim.SetBool(runId, true);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
            //rigidbody.MovePosition(rigidbody.position + Vector3.back * speed * Time.deltaTime);
            anim.SetBool(runId, true);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0, -5, 0));
            anim.SetBool(runId, true);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0, 5, 0));
            anim.SetBool(runId, true);
        }
        if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            anim.SetBool(runId, false);
        }
        #region 没使用finish时不会移动
        /*
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.name == "Terrain")
                {
                    targetPos = hit.point;
                    //print(targetPos);
                    transform.LookAt(targetPos);
                    offSet = targetPos - transform.position;
                    anim.SetBool(runId, true);
                    //offSet.normalized是一个向量，即一个方向
                    transform.position += offSet.normalized * 200 * Time.deltaTime;
                    if (Vector3.Distance(targetPos, transform.position) < 1f)
                    {
                        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime );
                        print(transform.position);
                    }
                    //anim.SetBool(runId, false);
                }
            }
        }
        */
        #endregion
        //要让角色先看向目标位置，用一个bool值控制后，再移动，没有bool值是无法移动的
        if (Input.GetMouseButtonDown(1))                   
        {
            anim.SetBool(runId, true);
            AudioManager.Instance.PlayNormalSound(AudioManager.Sound_Click);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.name == "Terrain")
                {
                    targetPos = hit.point;
                    transform.LookAt(targetPos);
                    finish = true;
                }
            }
        }
        if (finish)
        {
            offset = targetPos - transform.position;
            anim.SetBool(runId, true);
            transform.position += offset.normalized * speed * Time.deltaTime;
            if (Vector3.Distance(targetPos, transform.position) < 1f)
            {
                transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime);
                anim.SetBool(runId, false);
                finish = false;
            }
        }
    }
    /// <summary>
    /// 当点击动物时
    /// </summary>
    private void HitAnimals()
    {
        if(Input.GetMouseButtonDown(0))
        {
            AudioManager.Instance.PlayNormalSound(AudioManager.Sound_Click);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if(Physics.Raycast(ray,out hit))
            {
                if(hit.collider.gameObject.name== "Cat(Clone)" && Vector3.Distance(transform.position, hit.collider.transform.position)<20f)
                {
                    UIManager.Instance.ChooseEnter();
                    animalEnum = (int)AnimalEnum.NormalHorse;
                    hit.collider.gameObject.SetActive(false);
                }
            }
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.name == "Zebra(Clone)" && Vector3.Distance(transform.position, hit.collider.transform.position) < 20f)
                {
                    UIManager.Instance.ChooseEnter();
                    animalEnum = (int)AnimalEnum.Zebra;
                    hit.collider.gameObject.SetActive(false);
                }
            }
        } 
    }

    private void DistanceToAnimals()
    {
        if(Vector3.Distance(transform.position,cat.position)<30f && cat.gameObject.activeInHierarchy)
        {
            UIManager.Instance.NormalHorseEnter();
            UIManager.Instance.play = true;
        }
        else
        {
            UIManager.Instance.NormalHorseExit();
        }
    }


}
