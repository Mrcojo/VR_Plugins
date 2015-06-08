/***************************************************************************\
Title:  		Button_Load_Scene
Author:       	Marco Colombo

Description:  	A VR Button that allows to quit the game, in every game mdoe.
				Triggering the functionalty, will quit the game.
				
Inheritance:	VR_Interactive_Button
\***************************************************************************/

using UnityEngine;
using System.Collections;

public class Button_LoadScene : VR_Interactive_Button {
	// The scene to load
	public string sceneToLoad;

	// The text of the level button to display the extra information
	public TypogenicText extraInfoText;

	// Subscribers to events, to listen to on the activation of this game object
	protected override void OnEnable () {
		base.OnEnable ();
		Events.On ("Generic.GameStateChanged", OnGameStateChange);
	}
	
	// Unubscribers to events, to stop listening to on the deactivation of this game object
	protected override void OnDisable () {
		base.OnDisable ();
		Events.Off ("Generic.GameStateChanged", OnGameStateChange);
	}

	// Executes this logic when the game state change
	// <param name="parameters">The data passed to the method</param>
	void OnGameStateChange (object[] parameters) {

	}
	
	protected override void Start () {
		base.Start ();

		if (extraInfoText != null) {
			if (sceneToLoad == "5_Scene_GameModeDAMN") {
				int numberOfLives = PlayerPrefs.GetInt("NumberOfLives");
				if (numberOfLives > 0) {
					extraInfoText.Text = "Lives: " + numberOfLives;
				}
				else {
					extraInfoText.Text = "";
				}
			}
			else {
				float bestTime = PlayerPrefs.GetFloat(sceneToLoad + "_BestValue");
				if (bestTime > 0) {
					extraInfoText.Text = "Best time: " + bestTime.ToString("F2");
				}
				else {
					extraInfoText.Text = "";
				}
			}
		}
	}

	protected override void Update () {
		base.Update ();
	}
	// Executes this logic when the button is not "looked at" anymore
	protected override void Deactivate () {
		base.Reset ();
		denyInteraction = false;
	}

	// Loads the new scene
	protected override void Interaction () {
		base.Reset ();
		denyInteraction = true;
		Events.Fire ("Audio.PlaySound", "Button_Interaction");
		Events.Fire ("Generic.LoadNewScene", sceneToLoad);
	}
}
