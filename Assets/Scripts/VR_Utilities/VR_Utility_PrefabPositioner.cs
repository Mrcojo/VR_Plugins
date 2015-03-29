using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VR_Utility_PrefabPositioner : MonoBehaviour {
	
	public GameObject prefabToSpawn;
	public string prefabName;
	
	private GameObject prefab;
	
	private Dictionary<int, string> objectStrings;
	
	void OnEnable() {
		Events.On ("VR_Developer_Headset_Interaction.Interact", PlacePrefab);
	}
	
	void OnDisable() {
		Events.Off ("VR_Developer_Headset_Interaction.Interact", PlacePrefab);
	}
	
	void Start() {
		prefabName = prefabToSpawn.name;

		objectStrings = new Dictionary<int, string> ();
	}
	
	void Update() {
		
	}
	
	void PlacePrefab (object[] parameters) {
		RaycastHit hit = (RaycastHit) parameters [0];
		
		if (hit.collider.gameObject.tag == Tags.interactiveButton) {
			RemovePrefabLocation(hit.collider.gameObject);
			Destroy(hit.collider.gameObject);
		}
		else {
			prefab = Instantiate(prefabToSpawn, hit.point, Quaternion.identity) as GameObject;
			prefab.name = prefabName;
			AddPrefabLocation(prefab);
		}
		
		UpdatePlayerPrefabs ();
	}
	
	void AddPrefabLocation (GameObject prefabToAdd) {
		string jsonObject = "{"+
			"\"name\": \"" + prefabToAdd.name + "\"" + 
				",\"position\": {" + 
				"\"x\": \"" + prefabToAdd.transform.position.x + "\"" +  
				",\"y\": \"" + prefabToAdd.transform.position.y + "\"" +  
				",\"z\": \"" + prefabToAdd.transform.position.z + "\"" + 
				"}" +
				"}";
		
		objectStrings.Add (prefabToAdd.GetInstanceID(), jsonObject);
	}
	
	void RemovePrefabLocation (GameObject prefabToRemove) {
		objectStrings.Remove(prefabToRemove.GetInstanceID());
	}
	
	void UpdatePlayerPrefabs () {
		string playerPrefabsJSON = "{";
		int i = 0;
		foreach(KeyValuePair<int, string> entry in objectStrings) {
			playerPrefabsJSON += "\"" + entry.Key + "\": " + entry.Value;
			if (i < objectStrings.Count - 1) {
				playerPrefabsJSON += ",";
				i++;
			}
		}
		
		playerPrefabsJSON += "}";

		PlayerPrefs.SetString ("VR_Utility_PrefabPositioner", playerPrefabsJSON);
	}
}
