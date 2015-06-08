/***************************************************************************\
Title:  		Scenes_Manager
Author:       	Marco Colombo

Description:  	A manager for the scenes loaded and to load.
				
Inheritance:	None
\***************************************************************************/

using UnityEngine;
using System.Collections;

public class Scenes_Manager : MonoBehaviour {
	// The last scene played
	public string lastScenePlayed;

	// The current scene played
	public string currentScene;

	// The next scene to load
	public string nextSceneToLoad;

	// Subscribers to events, to listen to on the activation of this game object
	void OnEnable () {
		Events.On ("Generic.LoadNewScene", SetNextSceneToLoad);
		Events.On ("Generic.QuitGame", QuitGame);
		Events.On ("FadeEffect.FadeOutOver", LoadSceneAttempt);
	}
	
	// Unubscribers to events, to stop listening to on the deactivation of this game object
	void OnDisable () {
		Events.Off ("Generic.LoadNewScene", SetNextSceneToLoad);
		Events.Off ("Generic.QuitGame", QuitGame);
		Events.Off ("FadeEffect.FadeOutOver", LoadSceneAttempt);
	}

	// Sets the next scene to load
	// <param name="parameters">The data passed to the method</param>
	void SetNextSceneToLoad (object[] parameters) {
		nextSceneToLoad = parameters [0] as string;
	}

	// Attempts to load the next scene
	// <param name="parameters">The data passed to the method</param>
	void LoadSceneAttempt (object[] parameters) {
		if (nextSceneToLoad != "") {
			lastScenePlayed = currentScene;
			currentScene = nextSceneToLoad;
			//audioManager.DeleteAllAudioObjects();
			Application.LoadLevel(nextSceneToLoad);

			nextSceneToLoad = "";
		}
	}

	void Start () {
		lastScenePlayed = Application.loadedLevelName;
		currentScene = Application.loadedLevelName;
		nextSceneToLoad = "";
	}

	void QuitGame (object[] parameters) {
		Save_Manager.SaveData();

#if !UNITY_IOS
		System.Diagnostics.ProcessThreadCollection threads = System.Diagnostics.Process.GetCurrentProcess().Threads;
		
		foreach(System.Diagnostics.ProcessThread thread in threads){
			thread.Dispose();
		}
		
		System.Diagnostics.Process.GetCurrentProcess().Kill();
#else
		Application.Quit ();
#endif

	}
}
