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

    public void alphaOn()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 1;
    }
	void Update () {
		if (Input.GetKeyDown (KeyCode.Z) || GlobalVar.interfaces==1) {
			gameObject.GetComponent <CanvasGroup> ().alpha = 1;
		}
		if (Input.GetKeyUp (KeyCode.Z) || GlobalVar.interfaces!=1) {
			gameObject.GetComponent <CanvasGroup> ().alpha = 0;
		}


	}
}
