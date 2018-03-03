using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIfollow : MonoBehaviour {

    public GameObject playerToFollow;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(playerToFollow.transform.position)+new Vector3(0,100,0);

    }
}
