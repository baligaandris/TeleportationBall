using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    Vector2 defaultPos;
    public AudioClip bounce;

    // Use this for initialization
    void Start()
    {

        defaultPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter (Collision coll)
    {

     AudioSource audio = GetComponent<AudioSource>();
     audio.PlayOneShot(bounce);

    }
    public void ResetPosition() {
        transform.position = defaultPos;
        GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity*0;
        GetComponent<Rigidbody2D>().angularVelocity = GetComponent<Rigidbody2D>().angularVelocity * 0;
    }
}
