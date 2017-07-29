using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasEnable : MonoBehaviour {

	/*
	 * this will allow user to display or hide the GUI 
	 * on keyboard input. Currently set to 'z'
	 * 1 = display
	 * 0 = hide
	*/
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Z)) {
			gameObject.GetComponent <CanvasGroup> ().alpha = 1;
		}
		if (Input.GetKeyUp (KeyCode.Z)) {
			gameObject.GetComponent <CanvasGroup> ().alpha = 0;
		}


	}
}
