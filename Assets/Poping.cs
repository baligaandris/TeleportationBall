using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Poping : MonoBehaviour {

    string value;
    float timepassed = 0f;
    // Use this for initialization
    void Start () {
        value = GetComponent<Text>().text;
	}
	
	// Update is called once per frame
	void Update () {
        
        if (value == GetComponent<Text>().text)
        {
            timepassed += Time.deltaTime;
            GetComponent<RectTransform>().localScale = new Vector3(1 - timepassed, 1 - timepassed, 1 - timepassed);
        }
        else {
            timepassed = 0f;
            value = GetComponent<Text>().text;
        }
        
	}
}
