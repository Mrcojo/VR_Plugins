using UnityEngine;
using System.Collections;

public class VR_Utility_PrefabFactory : MonoBehaviour {

	public GameObject prefabToSpawn;
	public string prefabName;

	private GameObject prefab;

	void OnEnable() {
		Events.On ("VR_Developer_Headset_Interaction.Interact", PlacePrefab);
	}

	void OnDisable() {
		Events.Off ("VR_Developer_Headset_Interaction.Interact", PlacePrefab);
	}

	void Start() {
		prefabName = "";
	}

	void Update() {

	}

	void PlacePrefab (object[] parameters) {
		RaycastHit hit = (RaycastHit) parameters [0];

		if (hit.collider.gameObject.tag == Tags.interactiveButton) {
			Destroy(hit.collider.gameObject);
		}
		else {
			prefab = Instantiate(prefabToSpawn, hit.point, Quaternion.identity) as GameObject;
			if (prefabName == "") {
				prefabName = "PrefabSpawned";
			}
			prefab.name = prefabName;
		}
	}
}
