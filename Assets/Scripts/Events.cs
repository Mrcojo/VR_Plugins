using UnityEngine;
using System.Collections.Generic;
	
public static class Events  {	
	
	public delegate void CallbackHandler();
	public delegate void DataCallbackHandler(object[] parameters);

	private static Dictionary<int, List<CallbackHandler>> listners = new Dictionary<int, List<CallbackHandler>>();
	private static Dictionary<int, List<DataCallbackHandler>> dataListners = new Dictionary<int, List<DataCallbackHandler>>();
	
	/*
	 * On methods
	 */
	
	public static void on(string eventName, CallbackHandler handler) {
		addListener(eventName.GetHashCode(), handler, eventName);
	}

	private static void addListener(int eventID, CallbackHandler handler, string eventGraphName) {
		if(listners.ContainsKey(eventID)) {
			listners[eventID].Add(handler);
		} else {
			List<CallbackHandler> handlers =  new  List<CallbackHandler>();
			handlers.Add(handler);
			listners.Add(eventID, handlers);
		}
	}
	
	public static void on(string eventName, DataCallbackHandler handler) {
		addListener(eventName.GetHashCode(), handler, eventName);
	}

	private static void addListener(int eventID, DataCallbackHandler handler,  string eventGraphName) {
		if(dataListners.ContainsKey(eventID)) {
			dataListners[eventID].Add(handler);
		} else {
			List<DataCallbackHandler> handlers =  new  List<DataCallbackHandler>();
			handlers.Add(handler);
			dataListners.Add(eventID, handlers);
		}
	}
	
	
	/*
	 * Off methods
	 */
	
	public static void off(string eventName, CallbackHandler handler) {
		removeListener(eventName.GetHashCode(), handler, eventName);
	}

	public static void removeListener(int eventID, CallbackHandler handler, string eventGraphName) {
		if(listners.ContainsKey(eventID)) {
			List<CallbackHandler> handlers =  listners[eventID];
			handlers.Remove(handler);

			if(handlers.Count == 0) {
				listners.Remove(eventID);
			}
		}
	}
	
	public static void off(string eventName, DataCallbackHandler handler)  {
		removeListener(eventName.GetHashCode(), handler, eventName);
	}

	public static void removeListener(int eventID, DataCallbackHandler handler, string eventGraphName) {
		if(dataListners.ContainsKey(eventID)) {
			List<DataCallbackHandler> handlers =  dataListners[eventID];
			handlers.Remove(handler);

			if(handlers.Count == 0) {
				dataListners.Remove(eventID);
			}
		}
	}
	
	
	/*
	 * Fire methods
	 */
	
	public static void fire(string eventName) {
		fire(eventName.GetHashCode(), null, eventName);
	}
	
	public static void fire(string eventName, object[] data) {
		fire(eventName.GetHashCode(), data, eventName);
	}

	private static void fire(int eventID, object[] data, string eventName) {
		if(dataListners.ContainsKey(eventID)) {
			List<DataCallbackHandler>  handlers = cloneArray(dataListners[eventID]);
			int len = handlers.Count;
			for(int i = 0; i < len; i++) {
				handlers[i](data);
			}
		}
		
		if(listners.ContainsKey(eventID)) {
			List<CallbackHandler>  handlers = cloneArray(listners[eventID]);
			int len = handlers.Count;
			for(int i = 0; i < len; i++) {
				handlers[i]();
			}
		}	
	}


	/*
	 * Public methods
	 */

	public static void clearEvents() {
		listners.Clear();
		dataListners.Clear();
	}

	/*
	 * Private methods
	 */

	private static List<CallbackHandler> cloneArray(List<CallbackHandler> list) {
		List<CallbackHandler> nl =  new List<CallbackHandler>();
		int len = list.Count;
		for(int i = 0; i < len; i++) {
			nl.Add(list[i]);
		}
		
		return nl;
	}
	
	private static List<DataCallbackHandler> cloneArray(List<DataCallbackHandler> list) {
		List<DataCallbackHandler> nl =  new List<DataCallbackHandler>();
		int len = list.Count;
		for(int i = 0; i < len; i++) {
			nl.Add(list[i]);
		}
		
		return nl;
	}
}

