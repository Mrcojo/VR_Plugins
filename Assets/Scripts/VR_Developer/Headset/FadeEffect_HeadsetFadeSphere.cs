/***************************************************************************\
Title:  		FadeEffect_HeadsetFadeSphere
Author:       	Marco Colombo

Description:  	Handles a fading in and out effect, on the material of
				the fade sphere of the headset.

Inheritance:	FadeEffect
\***************************************************************************/

using UnityEngine;
using System.Collections;

public class FadeEffect_HeadsetFadeSphere : FadeEffect {
	// Subscribers to events, to listen to on the activation of this game object
	protected override void OnEnable () {
		base.OnEnable();
		Events.On ("Generic.LoadNewScene", FadeOut);
	}

	// Unubscribers to events, to stop listening to on the deactivation of this game object
	protected override void OnDisable () {
		base.OnDisable();
		Events.Off ("Generic.LoadNewScene", FadeOut);
	}

	protected override void Awake () {
		base.Awake();
	}

	protected override void Start () {
		base.Start();

		Reset ();
	}

	void OnLevelWasLoaded(int level) {
		Reset ();
	}

	void Reset () {
		fadeEffect = FadeEffects.In;
		alphaValue = 1;
		fadeMaterial.color = new Color(fadeMaterial.color.r, fadeMaterial.color.g, fadeMaterial.color.b, alphaValue);
	}

	protected override void Update () {
		base.Update();
	}

	// Fades the material IN (from transparent to opaque)
	// <param name="parameters">The data passed to the method</param>
	public override  void FadeIn (object[] parameters) {
		base.FadeIn(parameters);
	}

	// Fades the material OUT (from opaque to transparent)
	// <param name="parameters">The data passed to the method</param>
	public override  void FadeOut (object[] parameters) {
		base.FadeOut(parameters);
	}
}
