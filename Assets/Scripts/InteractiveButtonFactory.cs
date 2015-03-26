using UnityEngine;
using System.Collections;

public class InteractiveButtonFactory : MonoBehaviour {
	public GameObject interactiveButtons;
	public GameObject buttonPrefab;

	private GameObject pointer;
	private GameObject button;

	void OnEnable()
	{
		Events.On ("testEventVR", PlaceButton);
	}

	void OnDisable()
	{
		Events.Off ("testEventVR", PlaceButton);
	}

	void Start() {

	}

	void Update() {

	}

	void PlaceButton (object[] parameters) {
		RaycastHit hit = (RaycastHit) parameters [0];
		string paramString = (string) parameters [1];
		int paramNumber = (int) parameters [2];

		if (hit.collider.gameObject.tag == "InteractiveButton") {
			Destroy(hit.collider.gameObject);
		}
		else {
			button = Instantiate(buttonPrefab, hit.point, Quaternion.identity) as GameObject;
			button.name = "InteractiveButton";
		}
		Debug.Log (hit + " - " + paramString + " - " + paramNumber);
	}
}
