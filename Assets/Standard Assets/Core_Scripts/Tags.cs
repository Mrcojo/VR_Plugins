/***************************************************************************\
Title:  		Tags
Author:       	Marco Colombo

Description:  	Provides easy access to all the tags used in the game.

Inheritance:	None
\***************************************************************************/

using UnityEngine;
using System.Collections;

public class Tags : MonoBehaviour {
	// VR Tags
	public const string vr_headset = "VR_Headset";
	public const string vr_headset_interaction = "VR_Headset_Interaction";
	public const string vr_headset_GUI = "VR_Headset_GUI";
	public const string vr_pointer = "VR_Pointer";

	// Generic Tags
	public const string headsetFadeSphere = "HeadsetFadeSphere";
	public const string interactiveButton = "InteractiveButton";

	// Game Object Tags
	public const string colliderSphere = "ColliderSphere";
	public const string managers = "Managers";
}