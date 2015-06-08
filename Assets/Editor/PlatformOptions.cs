using UnityEngine;
using UnityEditor;
using System.Collections;

public enum VRPlatforms {
	Desktop_NO_VR,
	Desktop_DK2,
	Mobile_NO_VR,
	Mobile_GearVR,
	Mobile_Cardboard
}

public class PlatformOptions : EditorWindow {
	private static VRPlatforms currentVRPlatform;

	private static string manifest_String = "Assets/Plugins/Android/AndroidManifest.xml";
	private static string manifest_GearVR_String = "Assets/Plugins/Android/Manifests/AndroidManifest_GearVR.xml";
	private static string manifest_Cardboard_String = "Assets/Plugins/Android/Manifests/AndroidManifest_Cardboard.xml";

	[MenuItem("Platform Options/Settings")]
	static void Init () {
		string platform = PlayerPrefs.GetString ("VRPlatform");
		if (platform == null) {
			currentVRPlatform = VRPlatforms.Desktop_NO_VR;
		}
		else {
			if (platform == VRPlatforms.Desktop_NO_VR.ToString()) {
				currentVRPlatform = VRPlatforms.Desktop_NO_VR;
			}
			else if (platform == VRPlatforms.Desktop_DK2.ToString()) {
				currentVRPlatform = VRPlatforms.Desktop_DK2;
			}
			else if (platform == VRPlatforms.Mobile_NO_VR.ToString()) {
				currentVRPlatform = VRPlatforms.Mobile_NO_VR;
			}
			else if (platform == VRPlatforms.Mobile_GearVR.ToString()) {
				currentVRPlatform = VRPlatforms.Mobile_GearVR;
			}
			else if (platform == VRPlatforms.Mobile_Cardboard.ToString()) {
				currentVRPlatform = VRPlatforms.Mobile_Cardboard;
			}
		}

		// Get existing open window or if none, make a new one:
		PlatformOptions window = (PlatformOptions)EditorWindow.GetWindow (typeof (PlatformOptions));
		window.Show();
	}

	void SwitchHeadsetInScenes() {
		foreach (EditorBuildSettingsScene levelName in EditorBuildSettings.scenes) {
			EditorApplication.OpenScene(levelName.path);
			foreach (GameObject headsetGameObject in GameObject.FindGameObjectsWithTag(Tags.vr_headset)) {
				DestroyImmediate (headsetGameObject);
			}

			GameObject vrHeadsetPrefab = null;
			GameObject vrHeadsetGameObject = null;
			if (currentVRPlatform == VRPlatforms.Mobile_GearVR || currentVRPlatform == VRPlatforms.Desktop_DK2) {
				vrHeadsetPrefab = (GameObject) Resources.Load("Prefabs/VR_Headset_Oculus", typeof(GameObject));
			}
			else if (currentVRPlatform == VRPlatforms.Mobile_Cardboard) {
				vrHeadsetPrefab = (GameObject) Resources.Load("Prefabs/VR_Headset_Cardboard", typeof(GameObject));
			}
			else if (currentVRPlatform == VRPlatforms.Desktop_NO_VR || currentVRPlatform == VRPlatforms.Mobile_NO_VR) {
				vrHeadsetPrefab = (GameObject) Resources.Load("Prefabs/Desktop_Headset", typeof(GameObject));
			}

			vrHeadsetGameObject = (GameObject) Instantiate (vrHeadsetPrefab, vrHeadsetPrefab.transform.position, vrHeadsetPrefab.transform.rotation);
			vrHeadsetGameObject.name = vrHeadsetGameObject.name.Split('(')[0];

			EditorApplication.SaveScene();
		}
	}

	void OnGUI () {
		GUILayout.Label ("VR Platform", EditorStyles.boldLabel);
		EditorGUILayout.Space ();
		GUILayout.Label ("Current platform: " + currentVRPlatform.ToString());
		currentVRPlatform = (VRPlatforms) EditorGUILayout.EnumPopup (currentVRPlatform);

		if(GUI.changed) {
			PlayerPrefs.SetString ("VRPlatform", currentVRPlatform.ToString());
			Debug.Log ("VR Target platform switched to " + currentVRPlatform.ToString());

			if (currentVRPlatform == VRPlatforms.Mobile_GearVR) {
				System.IO.File.Delete(manifest_String);
				System.IO.File.Copy(manifest_GearVR_String, manifest_String);
			}
			else if (currentVRPlatform == VRPlatforms.Mobile_Cardboard) {
				System.IO.File.Delete(manifest_String);
				System.IO.File.Copy(manifest_Cardboard_String, manifest_String);
			}

			SwitchHeadsetInScenes();
		}
	}
}
