using UnityEngine;
using UnityEditor;
using System.Collections;

public class PlayerPrefs_Editor : EditorWindow {
	[MenuItem("PlayerPrefs/Clear All")]
	static void Init() {
		PlayerPrefs.DeleteAll();
	}
}
