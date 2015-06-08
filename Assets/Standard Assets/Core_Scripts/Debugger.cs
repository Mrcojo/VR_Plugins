using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public enum DebuggerLogTypes {
	Log,
	Error,
	Warning
}

public class Debugger : MonoBehaviour {
	static Debugger singleton = null;
	public static Debugger instance {
		get { return singleton; }
	}

	public   string logFilePath = "log.txt";
	public   bool echoToConsole = true;
	public   bool addTimeStamp = true;

	private  StreamWriter outputStream;

	void Awake() {
		if (singleton != null) {
			//UnityEngine.Debug.LogError("Multiple Debugger instances exist!");
			return;
		}
		singleton = this;

		//#if UNITY_ANDROID
		logFilePath = Application.persistentDataPath + "/" + logFilePath;
		//#endif
		
		#if !FINAL
			outputStream = new StreamWriter( logFilePath, true );
		#endif
	}

	void OnDestory() {
		#if !FINAL
		if ( outputStream != null ) {
			outputStream.Close();
			outputStream = null;
		}
		#endif
	}

	public static void Log (string logString) {
		logString = DebuggerLogTypes.Log.ToString() + ": " + logString;
		if (instance != null) {
			instance.WriteLog (logString);
		}

	}

	public static void LogError (string logString) {
		logString = DebuggerLogTypes.Error.ToString() + ": " + logString;
		if (instance != null) {
			instance.WriteLog (logString);
		}
	}

	public static void LogWarning (string logString) {
		logString = DebuggerLogTypes.Warning.ToString() + ": " + logString;
		if (instance != null) {
			instance.WriteLog (logString);
		}
	}

	private void WriteLog (string logString) {
		#if !FINAL
			if (Debugger.instance != null) {
				Debugger.instance.StartCoroutine(Debugger.instance.WriteLogString(logString));
			}
		#endif
	}

	IEnumerator WriteLogString(string logString) {
		if (addTimeStamp) {
			logString = "[" + System.DateTime.UtcNow.ToString("dd/MM/yyyy-HH:mm") + "] " + logString;
		}
		
		if (echoToConsole) {
			UnityEngine.Debug.Log (logString);
		}
		
		if (outputStream != null) {
			outputStream.WriteLine(logString);
			outputStream.Flush();
		}
		yield return new WaitForSeconds (0.001F);
	}
}
