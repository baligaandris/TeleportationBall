using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDetector : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag =="Player2")
        {
        transform.root.GetComponent<BallExplosion>().playersAround += 1;
        
        }
        transform.root.GetComponent<BallExplosion>().nearbyObjects.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player2")
        {
        transform.root.GetComponent<BallExplosion>().playersAround -= 1;
        
        }
        transform.root.GetComponent<BallExplosion>().nearbyObjects.Remove(collision.gameObject);
    }
}
