using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse : MonoBehaviour {

    public float amplitude = 0.1f;
    public float pulseSpeed = 1f;
    private Vector3 startingScale;

	// Use this for initialization
	void Start () {
        startingScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale += new Vector3(pulseSpeed * Time.deltaTime, pulseSpeed * Time.deltaTime, pulseSpeed * Time.deltaTime);
        if (transform.localScale.magnitude>=(startingScale+new Vector3 (amplitude,amplitude,amplitude)).magnitude || transform.localScale.magnitude <= (startingScale - new Vector3(amplitude, amplitude, amplitude)).magnitude)
        {
            pulseSpeed = -pulseSpeed;
        }
	}
}
