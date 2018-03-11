using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    private Vector3 defaultPos;

    // Use this for initialization
    void Start () {
        defaultPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (GetComponent<Rigidbody2D>().velocity.magnitude>0)
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

    public void ResetPosition()
    {
        transform.position = defaultPos;
    }
}
