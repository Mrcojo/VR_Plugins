using UnityEngine;
using System.Collections;

public enum VR_Developer_Modes {
	Play,
	Edit
}

public class VR_Developer_Manager : MonoBehaviour {

	public VR_Developer_Modes developerMode;

	private int modeIndex = 0;

	void OnEnable() {
	}
	
	void OnDisable() {

	}

	void Start() {
		developerMode = VR_Developer_Modes.Play;
		Events.Fire ("VR_Developer_Manager.DeveloperModeChanged");
	}

	void Update() {
		if (Application.platform == RuntimePlatform.WindowsEditor) {
			if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.RightControl)) {
				cycleModes();
			}
		}
		else if (Application.platform == RuntimePlatform.OSXEditor) {
			if (Input.GetKeyUp(KeyCode.LeftCommand) || Input.GetKeyUp(KeyCode.RightCommand)) {
				cycleModes();
			}
		}
	}

	private void cycleModes () {
		modeIndex++;
		if (modeIndex >= System.Enum.GetValues(typeof(VR_Developer_Modes)).Length) {
			modeIndex = 0;
		}
		developerMode = (VR_Developer_Modes) System.Enum.GetValues(typeof(VR_Developer_Modes)).GetValue(modeIndex);
		Events.Fire ("VR_Developer_Manager.DeveloperModeChanged");
	}
}
