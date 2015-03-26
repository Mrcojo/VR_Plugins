using UnityEngine;
using System.Collections;

public class InteractiveButtonFactory : MonoBehaviour {
	public GameObject interactiveButtons;
	public GameObject buttonPrefab;

	private GameObject pointer;
	private GameObject button;

	void OnEnable()
	{
		Events.on ("testEventVR", PlaceButton);
	}

	void OnDisable()
	{
		Events.off ("testEventVR", PlaceButton);
	}

	void Start() {

	}

	void Update() {

	}

	void PlaceButton (object[] parameters) {
		RaycastHit hit = (RaycastHit) parameters [0];

		if (hit.collider.gameObject.tag == "InteractiveButton") {
			Destroy(hit.collider.gameObject);
		}
		else {
			button = Instantiate(buttonPrefab, hit.point, Quaternion.identity) as GameObject;
			button.name = "InteractiveButton";
		}
	}
}
