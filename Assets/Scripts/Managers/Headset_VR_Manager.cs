/***************************************************************************\
Title:  		Headset_VR_Manager
Author:       	Marco Colombo

Description:  	The VR headset manager.

Inheritance:	None
\***************************************************************************/

using UnityEngine;

public class Headset_VR_Manager : MonoBehaviour {
	// The static reference to the singleton
	private static Headset_VR_Manager instanceRef;

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
