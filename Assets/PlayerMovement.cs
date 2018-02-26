using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    Rigidbody2D rb;
    public float accelleration;
    public float maxVelocity;
    private Vector3 fireVector;
    public GameObject bullet;
    public GameObject bullet2;
    public float fireSpeed;
    private bool canFire1 = true;
    private bool canFire2 = true;
    private Vector3 defaultPos;

    private float chargeTeleportTime = 0;

    public float deadzone = 0.25f;

    //axes to use
    private string h;
    private string v;
    private string hAim;
    private string vAim;
    private string f1;
    private string f2;

    Color originalcolor;

    // Use this for initialization
    void Start() {
        defaultPos = transform.position;
        rb = GetComponent<Rigidbody2D>(); //save rigidbody for later use
        originalcolor = GetComponent<SpriteRenderer>().color;
        if (gameObject.tag == "Player") //check if the player that this is attached to is p1 or p2 and assign controls based on that
        {
            h = "Horizontal";
            v = "Vertical";
            hAim = "HorizontalAim";
            vAim = "VerticalAim";
            f1 = "Fire1";
            f2 = "Fire2";
        }
        if (gameObject.tag == "Player2")
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
    void Update() {
        //get input for movement
        float xAxis = -Input.GetAxis(v);
        float yAxis = Input.GetAxis(h);
        Accellerate(xAxis, yAxis); //call the movement function

        if (Input.GetAxis(f1) != 0) //get input for shockwave attack from right trigger
        {
            if (canFire1) // since the trigger doesn't have a "getbuttondown" because it is an axis this bool takes care of only fireing it once when the trigger is pulled
            {
                if (Input.GetAxis(hAim) != 0 || Input.GetAxis(vAim) != 0) //this is to make sure you don't fire a shockwave when you are not aiming anywhere
                {
                    fireVector = new Vector2(Input.GetAxis(hAim), -Input.GetAxis(vAim)).normalized; //get input for aiming
                    //print(fireVector);
                    GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity); //instantiate bullet/shockwave
                    Physics2D.IgnoreCollision(GetComponent<Collider2D>(), newBullet.GetComponent<Collider2D>()); // this is to make sure you are not pushed around by your own bullet/shockwave
                    newBullet.GetComponent<Rigidbody2D>().AddForce(fireVector * fireSpeed); //add force to the bullet in the direction of your aim
                    canFire1 = false; //this is the same bool, that makes sure you don't fire a 1000 bullets a second
                }
            }
        }
        else
        {
            canFire1 = true; //when the player releases the trigger they get back the ability to shoot
        }
        if (Input.GetAxis(f2) != 0) //same thing for the other projectile, you get the idea
        {
            chargeTeleportTime += Time.deltaTime;
            GetComponent<SpriteRenderer>().color = Color.Lerp(GetComponent<SpriteRenderer>().color, Color.black, 0.02f);
            if (chargeTeleportTime>= 1)
            {
                if (Input.GetAxis(hAim) != 0 || Input.GetAxis(vAim) != 0)
                {
                    fireVector = new Vector2(Input.GetAxis(hAim), -Input.GetAxis(vAim)).normalized;
                    GameObject newBullet = Instantiate(bullet2, transform.position, Quaternion.identity);
                    newBullet.GetComponent<BulletSwitchBehavior>().SetWhoFiredMe(gameObject);
                    Physics2D.IgnoreCollision(GetComponent<Collider2D>(), newBullet.GetComponent<Collider2D>());
                    newBullet.GetComponent<Rigidbody2D>().AddForce(fireVector * fireSpeed);
                    chargeTeleportTime = 0;
                    GetComponent<SpriteRenderer>().color = originalcolor;
                }
            }


        }
        else
        {
            chargeTeleportTime = 0;
            GetComponent<SpriteRenderer>().color = originalcolor;
        }

    }

    void Accellerate(float y, float x) //this is the movement of the player
    {
        Vector2 force = new Vector2(0, 0);
        if (new Vector2(x, y).magnitude > deadzone) // there is a deadzone on the controllers joysticks, if the input is in the dead zone, we ignore it
        {
            force = (transform.up * y * accelleration) + (transform.right * x * accelleration); //we have physics based movement, here we calculate the force to be applied to the player
        }

        rb.AddForce(force); // and then we apply that force to the player
        ClampVelocity(); // this function limits the velocity of the player to the max velocity we set
    }
    void ClampVelocity()
    {
        float x = Mathf.Clamp(rb.velocity.x, -maxVelocity, maxVelocity);
        float y = Mathf.Clamp(rb.velocity.y, -maxVelocity, maxVelocity);

        rb.velocity = new Vector2(x, y);
    }
    public void ResetPosition (){
        transform.position = defaultPos;
        GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity * 0;
    }
}
