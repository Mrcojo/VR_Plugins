/***************************************************************************\
Title:  		VR_Interactive_Button
Author:       	Marco Colombo

Description:  	Implements the logic of a button for VR.
				The button needs to be "looked at" for a specific amount
				of time, to trigger its funcitonality.
				If / when the button is not "looked at" anymore,
				it resets.
				
Inheritance:	None
\***************************************************************************/

using UnityEngine;
using System.Collections;

public class VR_Interactive_Button : MonoBehaviour {
	// Defines if this object is activated or not
	protected bool activated;

	// Defines if this object faces the headset or not
	[SerializeField]
	protected bool faceHeadset = true;

	// The circle bar of this button (to display the activation status)
	[SerializeField]
	protected CircleBar circleBar;

	// The text to display on the bomb
	[SerializeField]
	protected TypogenicText buttonText;

	// The collider that allows the raycast to detect this button
	[SerializeField]
	protected Collider interactionCollider;

	// The seconds needed to trigger the functionality of this button
	[SerializeField]
	protected float secondsBeforeInteraction;

	// Defines id the button is currently activating
	protected bool isActivating;

	// Defines id the button was previously activating
	protected bool wasActivating;

	// Defines if the button should avoid exectuing its functionality
	protected bool denyInteraction;

	// The seconds passed since the button started interacting
	protected float interactionSeconds;

	// The current value of the activation
	protected float activationValue;

	// Subscribers to events, to listen to on the activation of this game object
	protected virtual void OnEnable () {
		Events.On ("VR_Developer_Headset_Interaction.RayCasted", ActivateAttempt);
	}

	// Unubscribers to events, to stop listening to on the deactivation of this game object
	protected virtual void OnDisable () {
		Events.Off ("VR_Developer_Headset_Interaction.RayCasted", ActivateAttempt);
	}

	protected virtual void Awake () {
		activated = true;
	}

	protected virtual void Start () {
		activationValue = 0;
		isActivating = false;
		wasActivating = false;
		denyInteraction = false;

		FaceHeadset ();
	}

	protected virtual void Update () {
	}

	// Rotates the button to face the headset (the player)
	protected void FaceHeadset () {
		if (faceHeadset) {
			transform.rotation = Quaternion.LookRotation(transform.position - GameObject.FindGameObjectWithTag(Tags.vr_headset).transform.position);
		}
	}

	//Sets the color of the text
	protected void SetTextColor (Color color) {
		buttonText.ColorBottomLeft = buttonText.ColorBottomRight = 
		buttonText.ColorTopLeft = buttonText.ColorTopRight = color;
	}

	// Tries to activate the button, when the object is "looked at"
	// <param name="parameters">The data passed to the method</param>
	protected void ActivateAttempt (object[] parameters) {
		if (activated) {
			// The object that is looked at
			GameObject objectInteracted = parameters [0] as GameObject;
			
			// If the "looked at" object is this one, proceed to activate it, if possible
			//Debug.Log(objectInteracted.GetComponent<Collider> ()  + " VS " + interactionCollider.name);
			if (objectInteracted.GetComponent<Collider> () == interactionCollider) {
				wasActivating = true;
				if (!denyInteraction) {
					Activating ();
				}
				// ...else, proceed to deactivate it
			} else {
				//if (!denyInteraction) {
					Deactivate ();
				//}
				isActivating = false;
			}
		}
	}

	// Executes this logic when this button is being "looked at"
	// and it's allowed to activate its functionality
	protected virtual void Activating () {
		if (activated) {
			if (interactionSeconds >= secondsBeforeInteraction) {
				Interaction();
			}
			
			isActivating = true;
			interactionSeconds += Time.deltaTime;
			activationValue = Mathf.Clamp (interactionSeconds / secondsBeforeInteraction, 0, 1);
			circleBar.value = activationValue;
		}
	}

	// Executes this logic when the button is not "looked at" anymore
	protected virtual void Deactivate () {
		if (activated) {
			if (wasActivating) {
				wasActivating = false;
			}
		}
	}

	// Resets the state of this button
	protected virtual void Reset () {
		isActivating = false;
		wasActivating = false;
		interactionSeconds = 0;
		activationValue = 0;
		circleBar.value = activationValue;
	}

	// The functionality of this button.
	// Executes this logic when this button is being looked at
	// enough to execute its functionality
	protected virtual void Interaction () {}
}