using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSwitchBehavior : MonoBehaviour {
    Vector3 tempPos; //use this to temporarily store a transform.position value while switching
    GameObject whoFiredMe; //use this to store the player that fired the spell

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag !="Immovable") //when colliding with something that is not immovable switch its position with the player's, and the destroy self.
        {
            print(collision.gameObject.name);
            tempPos = whoFiredMe.transform.position;
            whoFiredMe.transform.position = collision.gameObject.transform.root.position;
            collision.gameObject.transform.root.position = tempPos;
            Destroy(gameObject);
        }
    }

    public void SetWhoFiredMe(GameObject player) { //the player calls this so save itself as the one who fired the projectile
        whoFiredMe = player;
    }


}
