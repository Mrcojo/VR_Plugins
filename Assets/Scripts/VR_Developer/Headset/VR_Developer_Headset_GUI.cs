/***************************************************************************\
Title:  		VR_Developer_Headset_GUI
Author:       	Marco Colombo - MrCojo Entertainment

Description:  	Implements a GUI to be attached to the headset.
				This allows the player to visualise messages, scores,
				timers and so on.
				
Inheritance:	None
\***************************************************************************/

using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class VR_Developer_Headset_GUI : MonoBehaviour {
	// The text object of the main message
	public TypogenicText mainMessage;
	
	// Subscribers to events, to listen to on the activation of this game object
	void OnEnable () {
		Events.On ("HeadsetGUI.SetText", SetTextMessage);
		Events.On ("HeadsetGUI.ShowMessage", ShowMessage);
		Events.On ("HeadsetGUI.ClearHeadsetGUI", ClearHeadsetGUI);
	}
	
	// Unubscribers to events, to stop listening to on the deactivation of this game object
	void OnDisable () {
		Events.Off ("HeadsetGUI.SetText", SetTextMessage);
		Events.Off ("HeadsetGUI.ShowMessage", ShowMessage);
		Events.Off ("HeadsetGUI.ClearHeadsetGUI", ClearHeadsetGUI);
	}

	// Sets the text of a specific message type
	// <param name="parameters">The data passed to the method</param>
	void SetTextMessage (object[] parameters) {
		mainMessage.Text = (string) parameters[0];
	}
	
	void ShowMessage (object[] parameters) {
		TweenParms parms = new TweenParms();
		mainMessage.gameObject.SetActive(true);
		parms.Prop("localScale", Vector3.one); // Scale tween
		parms.Ease(EaseType.EaseOutQuad); // Easing type
		//parms.OnComplete();
		parms.Delay(0); // Initial delay
		HOTween.To(mainMessage.gameObject.transform, 0.37F, parms );
	}
	
	void HideMessage (TypogenicText messageText) {
		mainMessage.transform.localScale = Vector3.zero;
	}
	
	// Clears all the text in the GUI
	// <param name="parameters">The data passed to the method</param>
	void ClearHeadsetGUI (object[] paramters = null) {
		mainMessage.Text = "";
		HideMessage(mainMessage);
	}
	
	void OnLevelWasLoaded(int level) {
		ClearHeadsetGUI ();
	}
	
	void Start() {
		ClearHeadsetGUI ();
	}
	
	void Update() {
		
	}
}