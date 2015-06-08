/***************************************************************************\
Title:  		FaceHeadset
Author:       	Marco Colombo

Description:  	Faces the headset on initialization.
				
Inheritance:	None
\***************************************************************************/

using UnityEngine;
using System.Collections;

public class FaceHeadset : MonoBehaviour {
	// Subscribers to events, to listen to on the activation of this game object
	void OnEnable () {

	}
	
	// Unubscribers to events, to stop listening to on the deactivation of this game object
	void OnDisable () {

	}

	void Start () {
		transform.rotation = Quaternion.LookRotation(transform.position - GameObject.FindGameObjectWithTag(Tags.vr_headset).transform.position);
	}

	void Update () {

	}
}
