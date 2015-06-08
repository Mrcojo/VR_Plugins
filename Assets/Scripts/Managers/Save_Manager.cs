/***************************************************************************\
Title:  		Save_Manager
Author:       	Marco Colombo

Description:  	A manager for saving prefs on the cache of the game.
				
Inheritance:	None
\***************************************************************************/

using UnityEngine;
using System.Collections;

public class Save_Manager : MonoBehaviour {
	// Subscribers to events, to listen to on the activation of this game object
	void OnEnable () {

	}
	
	// Unubscribers to events, to stop listening to on the deactivation of this game object
	void OnDisable () {

	}

	// Saves a boolean in the cache
	// <param name="key">The key of the bool to save</param>
	// <param name="value">The value of the bool to save</param>
	public static void SaveBool (string key, bool value) {
		int valueToSave;
		if (value) {
			valueToSave = 1;
		}
		else {
			valueToSave = 0;
		}
		PlayerPrefs.SetInt(key, valueToSave);
	}

	// Saves a int in the cache
	// <param name="key">The key of the int to save</param>
	// <param name="value">The value of the int to save</param>
	public static void SaveInt (string key, int value) {
		PlayerPrefs.SetInt(key, value);
	}

	// Saves a float in the cache
	// <param name="key">The key of the float to save</param>
	// <param name="value">The value of the float to save</param>
	public static void SaveFloat (string key, float value) {
		PlayerPrefs.SetFloat(key, value);
	}

	// Saves a string in the cache
	// <param name="key">The key of the string to save</param>
	// <param name="value">The value of the string to save</param>
	public static void SaveString (string key, string value) {
		PlayerPrefs.SetString(key, value);
	}

	// Retrieves a boolean from the cache
	// <param name="key">The key of the bool to retrieve</param>
	// <param name="value">The value of the bool to retrieve</param>
	public static bool GetBool (string key) {
		int valueToRetrieve = PlayerPrefs.GetInt(key);
		if (valueToRetrieve == 0) {
			return false;
		}
		else {
			return true;
		}
	}

	// Retrieves a int from the cache
	// <param name="key">The key of the int to retrieve</param>
	// <param name="value">The value of the int to retrieve</param>
	public static int GetInt (string key) {
		return PlayerPrefs.GetInt(key);
	}

	// Retrieves a float from the cache
	// <param name="key">The key of the float to retrieve</param>
	// <param name="value">The value of the float to retrieve</param>
	public static float GetFloat (string key) {
		return PlayerPrefs.GetFloat(key);
	}

	// Retrieves a string from the cache
	// <param name="key">The key of the string to retrieve</param>
	// <param name="value">The value of the string to retrieve</param>
	public static string GetString (string key) {
		return PlayerPrefs.GetString(key);
	}

	// Saves all the data on the hard drive of the device
	public static void SaveData () {
		PlayerPrefs.Save ();
	}

	void Awake () {

	}

	void Start () {

	}

	void Update () {
	
	}

	void OnApplicationQuit() {
		PlayerPrefs.Save();
	}
}
