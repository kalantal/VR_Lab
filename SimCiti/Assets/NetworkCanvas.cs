using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkCanvas : MonoBehaviour {

    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;


	// Use this for initialization
	void Start () {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
	}

	// Update is called once per frame
	void Update () {
/*
       device = SteamVR_Controller.Input((int)trackedObject.index);
        if (device.GetPressDown(triggerButton))
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 1;
        }
        */

        if (Input.GetKeyDown (KeyCode.X) || GlobalVar.interfaces==2) {
			gameObject.GetComponent <CanvasGroup> ().alpha = 1;
		}
		if (Input.GetKeyUp (KeyCode.X) || GlobalVar.interfaces!=2) {
			gameObject.GetComponent <CanvasGroup> ().alpha = 0;
		}

	}
}
