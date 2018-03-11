using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepAudioManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.root.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude>0)
        {
            if (!GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            GetComponent<AudioSource>().Stop();
        }
	}
}
