using UnityEngine;
using UnityEditor;
using System.Collections;

public class VR_Utility_PrefabPositioner_Editor : EditorWindow {
	JSONObject jsonObj;

	[MenuItem("VR_Utilities/Prefab Positioner")]
	static void Init() {
		string playerPrefEntry = PlayerPrefs.GetString ("VR_Utility_PrefabPositioner");

		JSONObject fullJSONString = new JSONObject(playerPrefEntry);

		if (playerPrefEntry != null || playerPrefEntry != "") {
			for (int i = 0; i < fullJSONString.list.Count; ++i) {
				JSONObject jsonObject = fullJSONString[i];
				string prefabName = jsonObject.GetField("name").str;
				Vector3 positionToSpawn = new Vector3(
					float.Parse(jsonObject.GetField("position").GetField("x").str),
					float.Parse(jsonObject.GetField("position").GetField("y").str),
					float.Parse(jsonObject.GetField("position").GetField("z").str)
					);
				
				GameObject prefab = Instantiate(Resources.Load("Prefabs/" + prefabName), positionToSpawn, Quaternion.identity) as GameObject;
				prefab.name = prefabName;
			}
			
			PlayerPrefs.DeleteKey ("VR_Utility_PrefabPositioner");
		}
	}
}
