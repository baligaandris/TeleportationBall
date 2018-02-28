using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStandardBehavior : MonoBehaviour {
    float bulletDistance = 8.0f;
    public float bulletRange;
    private Vector3 startingPos;
    public float pushStrength;
	// Use this for initialization
	void Start () {
        startingPos = transform.position; //save the starting position of the bullet to use in range calculation
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(startingPos,transform.position) > bulletRange) //every frame check if the bullet got out of it's max range, if it did, destroy it
        {
            Destroy(gameObject);
        }
        transform.localScale = new Vector3(1,1,1) * Vector3.Distance(startingPos, transform.position)/3;

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Hit");
        if (collision.gameObject.tag=="Player" || collision.gameObject.tag=="Player2")
        {
            if (collision.gameObject.GetComponent<PlayerMovement>() != null)
            {
                collision.gameObject.GetComponent<PlayerMovement>().SwitchInput(false);
            }
            else
            {
                collision.gameObject.transform.parent.GetComponent<PlayerMovement>().SwitchInput(false);
            }
            
        }
        if (collision.gameObject.GetComponent<Rigidbody2D>() != null) //check if the bullet has collided with something, and if it did, and that something has a rigidbody, push it with a force proportional to it's distance from the player that fired it
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(GetComponent<Rigidbody2D>().velocity.normalized * pushStrength );
        }
        //Destroy(gameObject); //if it collides with anything, it should destroy itself
    }
    public void SetMaxRagne(float maxrange) {
        bulletRange += maxrange;
    }

    public void SetPushStrenght(float extraStrength) {
        pushStrength *= extraStrength;
    }
}
