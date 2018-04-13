using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallExplosion : MonoBehaviour {
    public int playersAround = 0;
    private float charge;
    public List<GameObject> nearbyObjects;
    public float pushStrength = 10f;
    public float pushPlayer = 15f;
    public float chargeTime = 2.5f;
    Color originalColor;
    // Use this for initialization
    void Start () {
        originalColor = GetComponent<SpriteRenderer>().color;
	}
	
	// Update is called once per frame
	void Update () {
        if (playersAround==2)
        {
            charge += Time.deltaTime;
            //GetComponent<SpriteRenderer>().color = Color.Lerp(GetComponent<SpriteRenderer>().color,Color.red,0.5f*Time.deltaTime);
            GetComponent<Animator>().SetBool("charging", true);
            if (charge>= chargeTime)
            {
                print("pusingAway");
                for (int i = 0; i < nearbyObjects.Count; i++)
                {
                    if (nearbyObjects[i].gameObject.tag == "Player" || nearbyObjects[i].gameObject.tag == "Player2")
                    {
                        if (nearbyObjects[i].gameObject.GetComponent<PlayerMovement>() != null)
                        {
                            nearbyObjects[i].gameObject.GetComponent<PlayerMovement>().outsideForce += (Vector2)(nearbyObjects[i].transform.position - transform.position).normalized*pushPlayer;
                        }
                        else
                        {
                            nearbyObjects[i].gameObject.transform.parent.GetComponent<PlayerMovement>().outsideForce += (Vector2)(nearbyObjects[i].transform.position - transform.position).normalized* pushPlayer;
                        }

                    }
                    else if (nearbyObjects[i].gameObject.transform.root.GetComponent<Rigidbody2D>() != null)
                    {
                        nearbyObjects[i].gameObject.transform.root.GetComponent<Rigidbody2D>().AddForce((nearbyObjects[i].transform.position - transform.position)*pushStrength);
                    }
                    GetComponent<Animator>().SetBool("charging", false);
                }
                charge = 0;
                //GetComponent<SpriteRenderer>().color = originalColor;
            }
        } else
        {
            charge = 0;
            //GetComponent<SpriteRenderer>().color = originalColor;
            GetComponent<Animator>().SetBool("charging", false);
        }
    }
}
