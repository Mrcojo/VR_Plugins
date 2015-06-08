/***************************************************************************\
Title:  		Connection_Manager
Author:       	Marco Colombo

Description:  	A manager to handle the internet connection.
				
Inheritance:	None
\***************************************************************************/

using UnityEngine;
using System.Collections;
using System.Net;
using System.IO;

public enum ConnectionStates {
	Available,
	Unavailable
}

public class Connection_Manager : MonoBehaviour {
	// The static reference to the singleton
	private static Connection_Manager instanceRef;

	// The current connection state
	public ConnectionStates currentConnectionState;

	// The previous connection state
	private ConnectionStates previousConnectionState;

	public const bool allowCarrierDataNetwork = false;
	private const float waitingTime = 2.0f;
	
	private Ping ping;
	private float pingStartTime;

	void CheckForConnectionChange() {
		if (previousConnectionState != currentConnectionState) {
			previousConnectionState = currentConnectionState;
			Events.Fire ("ConnectionManager.ConnectionChanged", currentConnectionState);
		}
	}

	void Awake () {
		currentConnectionState = ConnectionStates.Unavailable;
		if(instanceRef == null) {
			instanceRef = this;
			DontDestroyOnLoad(gameObject);
		}
		else {
			DestroyImmediate(gameObject);
		}
	}

	void Start () {
		DontDestroyOnLoad (gameObject);

		TestConnection ();
		//InvokeRepeating("PingService", 0, waitingTime);
	}

	/*void PingService() {
		StartCoroutine(PingGoogle());
	}
	
	IEnumerator PingGoogle() {
		WWW www = new WWW("www.google.com");
		
		yield return www;
		
		if(www.error == "" || www.error == null) {
			currentConnectionState = ConnectionStates.Available;
		}
		else {
			currentConnectionState = ConnectionStates.Unavailable;
		}
		Debugger.Log (currentConnectionState.ToString());
	}*/

	public void Update() {
		CheckForConnectionChange();
	}

	public void TestConnection () {
		string str = GetHtmlFromUri("http://google.com");
		previousConnectionState = currentConnectionState;
		//Debugger.Log (currentConnectionState.ToString());
		if (!str.Contains ("schema.org/WebPage")) {
			currentConnectionState = ConnectionStates.Unavailable;
		} else {
			currentConnectionState = ConnectionStates.Available;
		}
	}

	public string GetHtmlFromUri(string resource)
	{
		string html = string.Empty;
		HttpWebRequest req = (HttpWebRequest)WebRequest.Create(resource);
		try
		{
			using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
			{
				bool isSuccess = (int)resp.StatusCode < 299 && (int)resp.StatusCode >= 200;
				if (isSuccess)
				{
					using (StreamReader reader = new StreamReader(resp.GetResponseStream()))
					{
						//We are limiting the array to 80 so we don't have
						//to parse the entire html document feel free to 
						//adjust (probably stay under 300)
						char[] cs = new char[80];
						reader.Read(cs, 0, cs.Length);
						foreach(char ch in cs)
						{
							html +=ch;
						}
					}
				}
			}
		}
		catch
		{
			return "";
		}
		return html;
	}
}