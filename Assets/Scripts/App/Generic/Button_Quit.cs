/***************************************************************************\
Title:  		Button_Quit
Author:       	Marco Colombo

Description:  	A VR Button that allows to quit the game, in every game mdoe.
				Triggering the functionalty, will quit the game.
				
Inheritance:	VR_Interactive_Button
\***************************************************************************/

using UnityEngine;
using System.Collections;

public class Button_Quit : VR_Interactive_Button {
	
	protected override void Start () {
		base.Start ();
	}

	protected override void Update () {
		base.Update ();
	}

	// The functionality of this button.
	// Executes this logic when this button is being looked at
	// enough to execute its functionality
	protected override void Deactivate () {
		base.Reset ();
	}

	// Quits the level
	protected override void Interaction () {
		base.Reset ();
		denyInteraction = true;
		Events.Fire ("Audio.PlaySound", "Button_Interaction");
		Events.Fire ("Generic.QuitGame");
	}
}
