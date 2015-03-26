using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VR_Interaction : MonoBehaviour {
	public GameObject pointerPrefab;
	public GameObject buttonPrefab;

	private GameObject pointer;
	private GameObject button;

	void Start() {
		pointer = Instantiate(pointerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		pointer.name = "Pointer";
	}

	void Update() {
		RaycastHit hit;
		Debug.DrawRay(transform.position, transform.forward, Color.red, 100F);
		if (Physics.Raycast(transform.position, transform.forward, out hit)) {
			pointer.transform.position = hit.point;
			if (Input.GetKeyUp(KeyCode.Space)) {
				object [] parameters = new object[4];
				parameters[0] = hit;
				Events.fire ("testEventVR", parameters);
				
				/*Debug.Log(hit.collider.gameObject.name);
				if (hit.collider.gameObject.tag == "InteractiveButton") {
					Destroy(hit.collider.gameObject);
				}
				else {
					button = Instantiate(buttonPrefab, hit.point, Quaternion.identity) as GameObject;
					button.name = "InteractiveButton";
				}*/
			}
		}
	}
}
