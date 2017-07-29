using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyafter10 : MonoBehaviour {
    float delayTime = 10; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Destroy(gameObject, delayTime);
	}
}
