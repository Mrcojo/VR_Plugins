using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VR_Developer_Headset_Hud : MonoBehaviour {
	
	public Text developerModeText;

	private VR_Developer_Manager vrDeveloperManager;

	void OnEnable() {
		Events.On ("VR_Developer_Manager.DeveloperModeChanged", UpdateText);
	}
	
	void OnDisable() {
		Events.Off ("VR_Developer_Manager.DeveloperModeChanged", UpdateText);
	}

	void Start() {
		vrDeveloperManager = (VR_Developer_Manager) GameObject.FindGameObjectWithTag(Tags.developmentManager).GetComponent(typeof(VR_Developer_Manager));
	}

	void Update() {


	}

	void UpdateText (object[] p) {
		developerModeText.text = vrDeveloperManager.developerMode.ToString();
		if (vrDeveloperManager.developerMode == VR_Developer_Modes.Edit) {
			developerModeText.color = Color.yellow;
		}
		else if (vrDeveloperManager.developerMode == VR_Developer_Modes.Play) {
			developerModeText.color = Color.cyan;
		}
		else {
			developerModeText.color = Color.white;
		}
	}
}
