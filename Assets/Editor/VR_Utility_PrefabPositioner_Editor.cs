using UnityEngine;
using UnityEditor;
using System.Collections;

public class VR_Utility_PrefabPositioner_Editor : EditorWindow {
	JSONObject jsonObj;

	[MenuItem("VR_Utilities/Prefab Positioner")]
	static void Init() {
		string playerPrefEntry = PlayerPrefs.GetString ("VR_Utility_PrefabPositioner");

		if (PlayerPrefs.HasKey ("VR_Utility_PrefabPositioner")) {
			JSONObject fullJSONString = new JSONObject (playerPrefEntry);

			for (int i = 0; i < fullJSONString.list.Count; ++i) {
				JSONObject jsonObject = fullJSONString [i];
				string prefabName = jsonObject.GetField ("name").str;
				Vector3 positionToSpawn = new Vector3 (
					float.Parse (jsonObject.GetField ("position").GetField ("x").str),
					float.Parse (jsonObject.GetField ("position").GetField ("y").str),
					float.Parse (jsonObject.GetField ("position").GetField ("z").str)
				);
				
				GameObject prefab = Instantiate (Resources.Load ("Prefabs/" + prefabName), positionToSpawn, Quaternion.identity) as GameObject;
				prefab.name = prefabName;
			}
			
			PlayerPrefs.DeleteKey ("VR_Utility_PrefabPositioner");

			EditorUtility.DisplayDialog("VR_Utility_PrefabPositioner Result", "The prefabs are now spawned in the scene.\n\nUse the VR_Plugin_PrefabPositioner from your headset, to save new data.", "Ok");
		} else {
			EditorUtility.DisplayDialog("VR_Utility_PrefabPositioner Result", "There is no new data added to the scene", "Ok");
		}
	}
}
