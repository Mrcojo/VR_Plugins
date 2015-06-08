/***************************************************************************\
Title:  		FadeEffect
Author:       	Marco Colombo

Description:  	Handles a fading in and out effect, on a material.

Inheritance:	None
\***************************************************************************/

using UnityEngine;
using System.Collections;

public class FadeEffect : MonoBehaviour {
	public bool actOnOwnMaterial;
	
	// The material of the fading effect
	[SerializeField]
	protected Material fadeMaterial;
	
	// The speed of the fading effect
	public float fadeSpeed = 1;
	
	// The type of the fading effect
	protected FadeEffects fadeEffect;
	
	// The alpha value of the material
	protected float alphaValue;
	
	// Subscribers to events, to listen to on the activation of this game object
	protected virtual void OnEnable () {
		
	}
	
	// Unubscribers to events, to stop listening to on the deactivation of this game object
	protected virtual void OnDisable () {
		
	}
	
	protected virtual void Awake () {
		if (actOnOwnMaterial) {
			fadeMaterial = GetComponent<Renderer>().material;
		}
	}
	
	protected virtual void Start () {
		
	}
	
	protected virtual void Update () {
		if (fadeEffect == FadeEffects.In) {
			alphaValue -= Time.deltaTime * fadeSpeed;
		}
		else if (fadeEffect == FadeEffects.Out) {
			alphaValue += Time.deltaTime * fadeSpeed;
		}
		
		if (fadeEffect != FadeEffects.None) {
			if (alphaValue < 0 || alphaValue > 1) {
				if (fadeEffect == FadeEffects.In) {
					alphaValue = 0;
					fadeEffect = FadeEffects.None;
					Events.Fire ("FadeEffect.FadeInOver", gameObject);
				} else if (fadeEffect == FadeEffects.Out) {
					alphaValue = 1;
					fadeEffect = FadeEffects.None;
					Events.Fire ("FadeEffect.FadeOutOver", gameObject);
				}
			} 
			
			fadeMaterial.color = new Color(fadeMaterial.color.r, fadeMaterial.color.g, fadeMaterial.color.b, alphaValue);
		}
	}
	
	// Returns the current fading effect
	// <returns name="fadeEffect">The type of effect</param>
	protected virtual FadeEffects GetFadeEffect() {
		return fadeEffect;
	}
	
	// Fades the material IN (from transparent to opaque)
	// <param name="parameters">The data passed to the method</param>
	public virtual void FadeIn (object[] parameters = null) {
		fadeEffect = FadeEffects.In;
	}
	
	// Fades the material OUT (from opaque to transparent)
	// <param name="parameters">The data passed to the method</param>
	public virtual void FadeOut (object[] parameters = null) {
		fadeEffect = FadeEffects.Out;
	}
	
	public void FadeInObject () {
		FadeOut ();
	}
	
	public void FadeOutObject () {
		FadeIn ();
	}
}
