//https://www.raywenderlich.com/149239/htc-vive-tutorial-unity

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViveControllerInput : MonoBehaviour {


	// Use this for initialization
	void Start () {
	}

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update () {
        //gets position of finger on trackpad, writes to console
    
        if (Controller.GetAxis() != Vector2.zero)
        {
            Debug.Log(gameObject.name + Controller.GetAxis());
        }
        //when you squeeze the trigger
        if (Controller.GetHairTriggerDown())
        {
            Debug.Log(gameObject.name + " CANVAS PLS");

        }
        //when you release the trigger
        if (Controller.GetHairTriggerUp())
        {
            Debug.Log(gameObject.name + " Trigger Release!!!!!25");
        }
        //when you squeeze a grip button
        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log(gameObject.name + " Grip Press");
            GlobalVar.toggleInterface();
        }
        //when you release a grip button
        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log(gameObject.name + " Grip Release");
        }
        //TOUCHPAD MAYBE???
        if (Controller.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Debug.Log(gameObject.name + " Touchpad Press");
        }
    }
}
