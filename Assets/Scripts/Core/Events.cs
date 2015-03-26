using UnityEngine;
using System.Collections.Generic;

public static class Events  {	


	/*
	 * The delegate of the callback handler and the dictionary to store all the handlers subscribed
	 */
	public delegate void Handler(object[] parameters);
	private static Dictionary<int, List<Handler>> listeners = new Dictionary<int, List<Handler>>();


	/*
	 * On method
	 */
	
	public static void On(string eventName, Handler handler) {
		AddListener(eventName.GetHashCode(), eventName, handler);
	}
	
	private static void AddListener(int eventID, string eventGraphName, Handler handler) {
		if(listeners.ContainsKey(eventID)) {
			listeners[eventID].Add(handler);
		} else {
			List<Handler> handlers =  new  List<Handler>();
			handlers.Add(handler);
			listeners.Add(eventID, handlers);
		}
	}
	
	
	/*
	 * Off method
	 */
	
	public static void Off(string eventName, Handler handler)  {
		RemoveListener(eventName.GetHashCode(), eventName, handler);
	}
	
	public static void RemoveListener(int eventID, string eventGraphName, Handler handler) {
		if(listeners.ContainsKey(eventID)) {
			List<Handler> handlers =  listeners[eventID];
			handlers.Remove(handler);
			
			if(handlers.Count == 0) {
				listeners.Remove(eventID);
			}
		}
	}
	
	
	/*
	 * Fire method
	 */
	
	public static void Fire(string eventName) {
		Fire(eventName.GetHashCode(), eventName, null);
	}
	
	public static void Fire(string eventName, params object[] data) {
		Fire(eventName.GetHashCode(), eventName, data);
	}
	
	private static void Fire(int eventID, string eventName, params object[] data) {
		if(listeners.ContainsKey(eventID)) {
			List<Handler>  handlers = CloneArray(listeners[eventID]);
			int len = handlers.Count;
			for(int i = 0; i < len; i++) {
				handlers[i](data);
			}
		}
	}


	/*
	 * Private service methods
	 */
	
	private static List<Handler> CloneArray(List<Handler> list) {
		List<Handler> nl =  new List<Handler>();
		int len = list.Count;
		for(int i = 0; i < len; i++) {
			nl.Add(list[i]);
		}
		
		return nl;
	}


	/*
	 * Public service methods
	 */
	
	public static void ClearEvents() {
		listeners.Clear();
	}
}

