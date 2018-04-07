using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAnimLayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<SpriteRenderer>().sortingOrder = transform.root.GetComponent<SpriteRenderer>().sortingOrder + 1;

        //if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Teleport"))
        //{
        //    GetComponent<Animator>().SetBool("Teleport", false);
        //    GetComponent<SpriteRenderer>().enabled = false;
        //}
	}

    public void PlayAnim() {
        
        //GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Animator>().SetTrigger("teleport");
    }
    public void EndAnim() {
        //GetComponent<SpriteRenderer>().enabled = false;
    }
}
