using UnityEngine;
using System.Collections;

public enum VR_Developer_Modes : int {
	Edit,
	Play
}

public class VR_Developer_Manager : MonoBehaviour {
	public VR_Developer_Modes developerMode;

	void Start() {
		developerMode = VR_Developer_Modes.Play;
	}

	void Update() {
		if (Application.platform == RuntimePlatform.WindowsPlayer) {
			if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.RightControl)) {
				cycleModes();
			}
		}
		else if (Application.platform == RuntimePlatform.OSXPlayer) {
			if (Input.GetKeyUp(KeyCode.LeftCommand) || Input.GetKeyUp(KeyCode.RightCommand)) {
				cycleModes();
			}
		}
	}

	private void cycleModes () {
		if (developerMode < Enum.GetValues(typeof(VR_Developer_Modes).GetValues().Length) {
			developerMode++;
		} else {
			developerMode = 0;
		}
	}
}
