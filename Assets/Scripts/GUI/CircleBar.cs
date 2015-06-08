/***************************************************************************\
Title:  		CircleBar
Author:       	Marco Colombo

Description:  	Handles a circular bar that fills, based on a value.

Inheritance:	None
\***************************************************************************/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CircleBar : MonoBehaviour 
{
	// The circle bar material
	private Material circleBarMaterial;

	// The sprite renderer component
	private SpriteRenderer spriteRenderer;

	// The color of the circle bar
	[SerializeField]
	public Color barColor;

	// The value of the circle bar (progress)
	public float value;

	void Start () {
		value = 0;

		spriteRenderer = GetComponent<SpriteRenderer> ();

		circleBarMaterial = new Material (Shader.Find("Custom/CircleBarShader"));
		circleBarMaterial.name = "CircleBarMaterial";

		barColor.a = 1;

		spriteRenderer.material = circleBarMaterial;
	}
	
	void Update () 
    {
		circleBarMaterial.SetFloat("_Angle", Mathf.Lerp(-3.14f, 3.14f, value));
		circleBarMaterial.SetColor("_Color", barColor);
	}
}
