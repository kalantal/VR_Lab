using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimCiti_Controls : MonoBehaviour {

	private SteamVR_TrackedController _controller;
	private GameObject dataServer;
	private DataBox box;
	private void OnEnable()
	{
		_controller = GetComponent<SteamVR_TrackedController>();
		_controller.TriggerClicked += HandleTriggerClicked;
		_controller.PadClicked += HandlePadClicked;
	}

	private void OnDisable()
	{
		_controller.TriggerClicked -= HandleTriggerClicked;
		_controller.PadClicked -= HandlePadClicked;
	}

	#region Color Display
	private void HandleTriggerClicked(object sender, ClickedEventArgs e)
	{
		showColor();
	}

	private void showColor()
	{
		dataServer = GameObject.Find ("DataBox");
		box = dataServer.GetComponent<DataBox>();
		box.setColor ();

		foreach(GameObject dataRack in GameObject.FindGameObjectsWithTag("data box"))
		{
			box = dataRack.GetComponent<DataBox>();
			box.setColor ();
		}
	}
	#endregion

	#region Info Display
	private void HandlePadClicked(object sender, ClickedEventArgs e)
	{
		if (e.padY < 0)
			SelectPreviousPrimitive();
		else
			SelectNextPrimitive();
	}

	private void SelectNextPrimitive()
	{
		dataServer = GameObject.Find ("DataBox");
		dataServer.GetComponent<CanvasGroup>().alpha = 1;
	}

	private void SelectPreviousPrimitive()
	{
		
	}
	#endregion
}
