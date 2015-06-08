/***************************************************************************\
Title:  		VR_Developer_Headset_Interaction
Author:       	Marco Colombo

Description:  	Implements the interaction system of the VR headset.
				A ray is casted and the information about the colliding
				object is sent by an event, to be handled by the scripts
				that need it.
				A pointer is visualised and updated on the colliding point,
				to give a feedback to the user, about where he/she is 
				looking at.
				
Inheritance:	None
\***************************************************************************/

using UnityEngine;
using System.Collections;

public class VR_Developer_Headset_Interaction : MonoBehaviour {
	// The prefab of the pointer (to assign in the editor)
	[SerializeField]
	private GameObject pointerPrefab;

	// The pointer to spawn and update in the game scene
	private GameObject pointer;

	// Defines if the pointer should be shown or not
	public bool showPointer = true;

	// The object on which the raycast of the headset is colliding with
	public GameObject raycastedObject;

	// Subscribers to events, to listen to on the activation of this game object
	void OnEnable () {
		OVRTouchpad.TouchHandler += HandleTouchCallback;
	}
	
	// Unubscribers to events, to stop listening to on the deactivation of this game object
	void OnDisable () {
		OVRTouchpad.TouchHandler -= HandleTouchCallback;
	}

	// Handles the touch events of the headset's touchpad
	void HandleTouchCallback (object sender, System.EventArgs e)
	{
		OVRTouchpad.TouchArgs touchArgs = (OVRTouchpad.TouchArgs)e;
		if(touchArgs.TouchType == OVRTouchpad.TouchEvent.SingleTap) {
			Events.Fire("Input.HeadsetInput", Inputs.Tap);
		}
		else if (touchArgs.TouchType == OVRTouchpad.TouchEvent.Up) {
			Events.Fire("Input.HeadsetInput", Inputs.SwipeUp);
		}
		else if (touchArgs.TouchType == OVRTouchpad.TouchEvent.Down) {
			Events.Fire("Input.HeadsetInput", Inputs.SwipeDown);
		}
		else if (touchArgs.TouchType == OVRTouchpad.TouchEvent.Left) {
			Events.Fire("Input.HeadsetInput", Inputs.SwipeRight);
		}
		else if (touchArgs.TouchType == OVRTouchpad.TouchEvent.Right) {
			Events.Fire("Input.HeadsetInput", Inputs.SwipeLeft);
		}
	}

	void HandleDesktopInput () {
		/*if(Input.GetMouseButtonDown(0)) {
			Events.Fire("Input.HeadsetInput", Inputs.Tap);
		}*/
		if (Input.GetMouseButtonDown(2)) {
			Events.Fire("Input.HeadsetInput", Inputs.Button);
		}
		else if (Input.GetMouseButtonDown(1)) {
			Events.Fire("Input.HeadsetInput", Inputs.Button);
		}
		else if (Input.GetKeyDown(KeyCode.UpArrow)) {
			Events.Fire("Input.HeadsetInput", Inputs.SwipeUp);
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow)) {
			Events.Fire("Input.HeadsetInput", Inputs.SwipeDown);
		}
		else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			Events.Fire("Input.HeadsetInput", Inputs.SwipeLeft);
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow)) {
			Events.Fire("Input.HeadsetInput", Inputs.SwipeRight);
		}
	}

	void CreatePointer () {
		DestroyPointer ();
		pointer = Instantiate (pointerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		pointer.name = "Pointer";
	}

	void DestroyPointer () {
		if (pointer != null) {
			GameObject.Destroy(pointer);
		}
	}

	void Awake () {
		OVRTouchpad.Create();
	}

	void Start() {
		Cursor.visible = false;
		if (pointerPrefab != null) {
			if (showPointer) {
				CreatePointer();
			}
		}
	}

	void OnLevelWasLoaded () {
		if (pointerPrefab != null && pointer == null && showPointer) {
			CreatePointer();
		}
	}

	void Update () {
		HandleDesktopInput();
	}

	void FixedUpdate() {
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.forward * 1000, out hit)) {
			Debug.DrawLine(transform.position, hit.point, Color.green);
			raycastedObject = hit.collider.gameObject;
			if (pointer != null && showPointer) {
				pointer.transform.position = hit.point;
			}

			Events.Fire ("VR_Developer_Headset_Interaction.RayCasted", raycastedObject);
		}
	}
}
