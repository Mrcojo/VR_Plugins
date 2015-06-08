/***************************************************************************\
Title:  		VR_Headset_Pointer
Author:       	Marco Colombo

Description:  	Implements the pointer properties.
				
Inheritance:	None
\***************************************************************************/

using UnityEngine;
using System.Collections;

public class VR_Headset_Pointer : MonoBehaviour {
	// The static reference to the singleton
	private static VR_Headset_Pointer instanceRef;

	// The pointer material which changes color
	[SerializeField]
	private Material pointerMaterial;

	// Subscribers to events, to listen to on the activation of this game object
	void OnEnable () {

	}

	// Unubscribers to events, to stop listening to on the deactivation of this game object
	void OnDisable () {
		
	}

	void Awake () {
		DontDestroyOnLoad (gameObject);
		
		if(instanceRef == null) {
			instanceRef = this;
			DontDestroyOnLoad(gameObject);
		}
		else {
			DestroyImmediate(gameObject);
		}
	}

	void Start() {

	}

	void Update() {

	}
}
