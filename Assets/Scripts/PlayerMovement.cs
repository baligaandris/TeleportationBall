﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure;

public class PlayerMovement : MonoBehaviour {
    Rigidbody2D rb;
    public float accelleration;
    public float maxVelocity;
    private Vector3 fireVector;
    public GameObject bullet;
    public GameObject bullet2;
    public GameObject wandEnd;
    public float fireSpeed;
    private bool canFire1 = true;
    public bool canFire2 = false;
    private Vector3 defaultPos;
    private float noInputCountdown;
    public float noInputCountdownMax = 0.5f;

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

    private Animator anim;
    public bool getinput = true;

    private bool chargingPush = false;
    private float pushCharge = 0;
    public float maxCharge;

    //teleportation variables
    public int teleportsLeft;
    public int maxTeleports = 3;
    private float teleportCharging;
    public float teleportChargingMax = 2f;
    public Text teleportCounterUI;
    public float teleportWait = 1f;
    private RaycastHit2D hit;


    //audio
    AudioSource audio;
    public AudioClip shockwave;
    public AudioClip teleport;
    public AudioClip shockwavecharge;
    public AudioClip teleportcharge;

    public Vector2 outsideForce = Vector2.zero;



    //Xinput

    private PlayerIndex playerIndex;
    private GamePadState padState;

    // Use this for initialization
    void Start() {
        audio = GetComponent<AudioSource>();
        teleportsLeft = maxTeleports;
        teleportCounterUI.GetComponent<Text>().text = teleportsLeft.ToString();
        defaultPos = transform.position;
        rb = GetComponent<Rigidbody2D>(); //save rigidbody for later use
        originalcolor = GetComponent<SpriteRenderer>().color;
        anim = GetComponent<Animator>();
        if (gameObject.tag == "Player") //check if the player that this is attached to is p1 or p2 and assign controls based on that
        {
            h = "Horizontal";
            v = "Vertical";
            hAim = "HorizontalAim";
            vAim = "VerticalAim";
            f1 = "Fire1";
            f2 = "Fire2";
            playerIndex = PlayerIndex.One;

        }
        if (gameObject.tag == "Player2")
        {
            h = "Horizontal2";
            v = "Vertical2";
            hAim = "HorizontalAim2";
            vAim = "VerticalAim2";
            f1 = "Fire12";
            f2 = "Fire22";
            playerIndex = PlayerIndex.Two;
        }

    }

    // Update is called once per frame
    void Update() {
        padState = GamePad.GetState(playerIndex);


        float yAxis = padState.ThumbSticks.Left.X;
        float xAxis = padState.ThumbSticks.Left.Y;

        Accellerate(xAxis, yAxis); //call the movement function
        //get input for movement
        if (GetComponent<LineRenderer>().enabled)
        {
            GetComponent<LineRenderer>().SetPosition(0, wandEnd.transform.position);
        }
        if (teleportsLeft<maxTeleports)
        {
            teleportCharging += Time.deltaTime;
            if (teleportCharging>=teleportChargingMax)
            {
                teleportCharging = 0;
                teleportsLeft++;
                teleportCounterUI.GetComponent<Text>().text = teleportsLeft.ToString();
            }
        }

        if (getinput)
        {

            if (rb.velocity.magnitude>0)
            {
                anim.SetBool("Moving", true);
            }
            else
            {
                anim.SetBool("Moving", false);
            }

            if (padState.Triggers.Right!=0) //get input for shockwave attack from right trigger
            {
                if (canFire1) // since the trigger doesn't have a "getbuttondown" because it is an axis this bool takes care of only fireing it once when the trigger is pulled
                {
                    if (padState.ThumbSticks.Right.X != 0 || padState.ThumbSticks.Right.Y != 0) //this is to make sure you don't fire a shockwave when you are not aiming anywhere
                    {
                        chargingPush = true;
                        pushCharge += Time.deltaTime;
                        GamePad.SetVibration(playerIndex, pushCharge* 0.5f, 0);
                        if (!audio.isPlaying)
                        {
                            audio.clip = shockwavecharge;
                            audio.loop = false;
                            audio.Play();
                        }
                        
                    }
                }
            }
            else
            {
                if (chargingPush)
                {
                    audio.Stop();
                    GamePad.SetVibration(playerIndex, 0, 0);
                    fireVector = new Vector2(padState.ThumbSticks.Right.X, padState.ThumbSticks.Right.Y).normalized; //get input for aiming
                    if (pushCharge>maxCharge)
                    {
                        pushCharge = maxCharge;
                    }
                    GameObject newBullet = Instantiate(bullet, wandEnd.transform.position, Quaternion.Euler(new Vector3(0, 0, (Mathf.Atan2(padState.ThumbSticks.Right.Y, padState.ThumbSticks.Right.X) * Mathf.Rad2Deg)/* - 90*/))); //instantiate bullet/shockwave
                    Physics2D.IgnoreCollision(GetComponent<Collider2D>(), newBullet.GetComponent<Collider2D>()); // this is to make sure you are not pushed around by your own bullet/shockwave
                    newBullet.GetComponent<Rigidbody2D>().AddForce(fireVector * fireSpeed); //add force to the bullet in the direction of your aim
                    newBullet.GetComponent<BulletStandardBehavior>().SetMaxRagne(pushCharge);
                    newBullet.GetComponent<BulletStandardBehavior>().SetPushStrenght(pushCharge);
                    //canFire1 = false; //this is the same bool, that makes sure you don't fire a 1000 bullets a second
                    chargingPush = false;
                    pushCharge = 0;
                    audio.clip = shockwave;
                    audio.loop = false;
                    audio.Play();
                }
                //canFire1 = true; //when the player releases the trigger they get back the ability to shoot
            }
            if (padState.Triggers.Left != 0) //same thing for the other projectile, you get the idea
            {
                //chargeTeleportTime += Time.deltaTime;
                //GetComponent<SpriteRenderer>().color = Color.Lerp(GetComponent<SpriteRenderer>().color, Color.black, 0.02f);
                //if (chargeTeleportTime >= 0)

                    if (padState.ThumbSticks.Right.X != 0 || padState.ThumbSticks.Right.Y != 0)
                    {
                        if (teleportsLeft>0)
                        {
                            canFire2 = true;
                        GamePad.SetVibration(playerIndex, 0, 0.25f);

                        //fireVector = new Vector2(Input.GetAxis(hAim), -Input.GetAxis(vAim)).normalized;
                        //GameObject newBullet = Instantiate(bullet2, wandEnd.transform.position, Quaternion.identity);
                        //newBullet.GetComponent<BulletSwitchBehavior>().SetWhoFiredMe(gameObject);
                        //Physics2D.IgnoreCollision(GetComponent<Collider2D>(), newBullet.GetComponent<Collider2D>());
                        //Physics2D.IgnoreCollision(GetComponentInChildren<Collider2D>(), newBullet.GetComponent<Collider2D>());
                        //newBullet.GetComponent<Rigidbody2D>().AddForce(fireVector * fireSpeed);
                        //chargeTeleportTime = 0;
                        //GetComponent<SpriteRenderer>().color = originalcolor;

                        //fireVector = new Vector2(Input.GetAxis(hAim), -Input.GetAxis(vAim)).normalized;
                        if (fireVector==null)
                        {
                            fireVector = new Vector2(padState.ThumbSticks.Right.X, padState.ThumbSticks.Right.Y).normalized;
                        }
                        fireVector = Vector2.Lerp(fireVector, new Vector2(padState.ThumbSticks.Right.X, padState.ThumbSticks.Right.Y).normalized, 10*Time.deltaTime);
                            
                            hit = Physics2D.Raycast(wandEnd.transform.position, fireVector);


                            GetComponent<LineRenderer>().enabled = true;
                            GetComponent<LineRenderer>().SetPosition(0, wandEnd.transform.position);
                            GetComponent<LineRenderer>().SetPosition(1, hit.point);

                        if (!audio.isPlaying)
                        {
                            audio.clip = teleportcharge;
                            audio.loop = true;
                            audio.Play();
                        }
                    }
                        
                    }



            }
            else
            {
                
                if (canFire2)
                {
                    GamePad.SetVibration(playerIndex, 0, 0);
                    audio.Stop();
                    teleportsLeft--;
                    teleportCounterUI.GetComponent<Text>().text = teleportsLeft.ToString();
                    
                    if (hit.collider != null)
                    {
                        if (hit.collider.gameObject.tag != "Immovable" && hit.collider.gameObject.tag != "Goal") //when colliding with something that is not immovable switch its position with the player's, and the destroy self.
                        {
                            GetComponentInChildren<TeleportAnimLayer>().PlayAnim();
                            hit.collider.gameObject.transform.root.gameObject.GetComponentInChildren<TeleportAnimLayer>().PlayAnim();
                            StartCoroutine(WaitAndTeleport(hit));

                        }
                        else
                        {
                            StartCoroutine(WaitAndTurnOffLineRenderer());
                        }
                    }
                    else
                    {
                        StartCoroutine(WaitAndTurnOffLineRenderer());
                    }
                    canFire2 = false;
                }

            }
        }
        else
        {
            noInputCountdown -= Time.deltaTime;
            if (noInputCountdown<=0)
            {
                SwitchInput(true);
            }
        }
        
        if (padState.ThumbSticks.Right.X ==0 && padState.ThumbSticks.Right.Y == 0)
        {
            anim.SetBool("Aiming", false);
            anim.SetFloat("x", padState.ThumbSticks.Left.X);
            anim.SetFloat("y", padState.ThumbSticks.Left.Y);
        }
        else
        {
            //rotate according to aiming
            anim.SetBool("Aiming", true);
            anim.SetFloat("x", padState.ThumbSticks.Right.X);
            anim.SetFloat("y", padState.ThumbSticks.Right.Y);
        }

    }

    void Accellerate(float x, float y) //this is the movement of the player
    {
        rb.velocity = (new Vector2(y,x).normalized*maxVelocity)+outsideForce;
        outsideForce = Vector2.ClampMagnitude(outsideForce,outsideForce.magnitude-1);
        //Vector2 force = new Vector2(0, 0);
        //if (new Vector2(x, y).magnitude > deadzone) // there is a deadzone on the controllers joysticks, if the input is in the dead zone, we ignore it
        //{
        //    force = (transform.up * y * accelleration) + (transform.right * x * accelleration); //we have physics based movement, here we calculate the force to be applied to the player
        //}

        //rb.AddForce(force); // and then we apply that force to the player
        //ClampVelocity(); // this function limits the velocity of the player to the max velocity we set
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

    public void SwitchInput(bool switchto) {
        getinput = switchto;
        noInputCountdown = noInputCountdownMax;
    }

    private IEnumerator WaitAndTeleport(RaycastHit2D hit) {
        audio.PlayOneShot(teleport);
        yield return new WaitForSeconds(teleportWait);
        Vector3 tempPos = transform.position;
        transform.position = hit.collider.gameObject.transform.root.position;
        hit.collider.gameObject.transform.root.position = tempPos;
        GetComponent<LineRenderer>().enabled = false;
        GetComponentInChildren<TeleportAnimLayer>().EndAnim();
        hit.collider.gameObject.transform.root.gameObject.GetComponentInChildren<TeleportAnimLayer>().EndAnim();

    }
    private IEnumerator WaitAndTurnOffLineRenderer()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<LineRenderer>().enabled = false;
    }

    public void StopTeleportation() {
        StopAllCoroutines();
    }
    public int GetTeleportsLeft() {
        return teleportsLeft;
    }
}
