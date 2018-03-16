using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingFeedback : MonoBehaviour {
    public float deadzone = 0.25f;
    public float magnification = 30f;
    public float speed = 1;
    //axes to use
    private string h;
    private string v;
    private string hAim;
    private string vAim;
    private string f1;
    private string f2;

    Vector2 crosshairPosition;

    // Use this for initialization
    void Start () {
        if (transform.parent.gameObject.tag == "Player") //check if the player that this is attached to is p1 or p2 and assign controls based on that
        {
            h = "Horizontal";
            v = "Vertical";
            hAim = "HorizontalAim";
            vAim = "VerticalAim";
            f1 = "Fire1";
            f2 = "Fire2";
        }
        if (transform.parent.gameObject.tag == "Player2")
        {
            h = "Horizontal2";
            v = "Vertical2";
            hAim = "HorizontalAim2";
            vAim = "VerticalAim2";
            f1 = "Fire12";
            f2 = "Fire22";
        }
    }
	
	// Update is called once per frame
	void Update () {

        //if (Input.GetAxis(hAim) != 0 || Input.GetAxis(vAim) != 0)
        //{
        //    Vector2 crosshairPosition = new Vector2(-Input.GetAxis(vAim), -Input.GetAxis(hAim));
        //    if (crosshairPosition.magnitude > deadzone)
        //    {
        //        GetComponent<SpriteRenderer>().enabled = true;
        //        transform.localPosition = crosshairPosition.normalized * magnification;
        //    }
        //}
        //else
        //{
        //    GetComponent<SpriteRenderer>().enabled = false;
        //}

        if (transform.root.gameObject.GetComponent<PlayerMovement>().getinput)
        {
            if (Input.GetAxis(hAim) != 0 || Input.GetAxis(vAim) != 0)
            {
                //new aiming code from here
                if (crosshairPosition == null)
                {
                    crosshairPosition = new Vector2(Input.GetAxis(hAim), Input.GetAxis(vAim)).normalized;
                }
                crosshairPosition = Vector2.Lerp(crosshairPosition, new Vector2( -Input.GetAxis(vAim),Input.GetAxis(hAim)).normalized, 10 * Time.deltaTime);
                //to here
                if (crosshairPosition.magnitude > deadzone)
                {
                    GetComponent<SpriteRenderer>().enabled = true;
                    float angle = Mathf.Atan2(crosshairPosition.x, crosshairPosition.y) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                    if ((angle > 68 && angle < 180) || (angle >= -180 && angle < -64))
                    {
                        GetComponent<SpriteRenderer>().sortingOrder = transform.root.gameObject.GetComponent<SpriteRenderer>().sortingOrder - 1;
                    }
                    else
                    {
                        GetComponent<SpriteRenderer>().sortingOrder = transform.root.gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
                    }
                    //transform.localPosition = crosshairPosition.normalized * magnification;
                }
            }
            else
            {
                GetComponent<SpriteRenderer>().enabled = false;
            }

        }
    }


}
