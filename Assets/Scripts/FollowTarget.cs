using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {

    private Transform player;
    private Vector3 offSet;
    private float smoothSpeed = 3f;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player(Clone)").transform;
        offSet = transform.position - player.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(player!=null)
        {
            Vector3 targetPosition = player.position + player.TransformDirection(offSet);
            //transform.position = targetPosition;
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
            transform.LookAt(player.position + new Vector3(0, 2, 0));
        }
	}
}
