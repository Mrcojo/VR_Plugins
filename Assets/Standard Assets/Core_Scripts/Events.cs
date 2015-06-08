/***************************************************************************\
Title:  		Events
Author:       	Marco Colombo

Description:  	Implements a generic Event system (flash-like) that allows
				any script to easily fire events (passing any data) and
				to subscribe / un-subsubscribe to them, calling
				callback methods if / when needed.

Inheritance:	None
\***************************************************************************/

using UnityEngine;
using System.Collections.Generic;

public static class Events  {	
	// The delegate of the callback handler
	public delegate void Handler(object[] parameters);

	// The dictionary to store all the handlers subscribed
	private static Dictionary<int, List<Handler>> listeners = new Dictionary<int, List<Handler>>();

	private static List<Handler> CloneArray(List<Handler> list) {
		List<Handler> nl =  new List<Handler>();
		int len = list.Count;
		for(int i = 0; i < len; i++) {
			nl.Add(list[i]);
		}
		
		return nl;
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

	private static void RemoveListener(int eventID, string eventGraphName, Handler handler) {
		if(listeners.ContainsKey(eventID)) {
			List<Handler> handlers =  listeners[eventID];
			handlers.Remove(handler);
			
			if(handlers.Count == 0) {
				listeners.Remove(eventID);
			}
		}
	}

	private static void Publish(int eventID, string eventName, params object[] data) {
		if(listeners.ContainsKey(eventID)) {
			List<Handler>  handlers = CloneArray(listeners[eventID]);
			int len = handlers.Count;
			for(int i = 0; i < len; i++) {
				handlers[i](data);
			}
		}
	}

	// Clears all the listeners
	public static void ClearEvents() {
		listeners.Clear();
	}

	// Subscribes (aka registers) a specific event, and calls a callback when listening to it. 
	// <param name="eventName">The name of the event to subscribe to</param>
	// <param name="handler">The callback method</param>
	public static void On(string eventName, Handler handler) {
		AddListener(eventName.GetHashCode(), eventName, handler);
	}

	// Unsubscribes (aka deregisters) a specific event. 
	// <param name="eventName">The name of the event to unsubscribe from</param>
	// <param name="handler">The callback method</param>
	public static void Off(string eventName, Handler handler)  {
		RemoveListener(eventName.GetHashCode(), eventName, handler);
	}

	// Fires (aka publishes) a specific event. 
	// <param name="eventName">The name of the event to fire to</param>
	public static void Fire(string eventName) {
		Publish(eventName.GetHashCode(), eventName, null);
	}

	// Fires (aka publishes) a specific event. 
	// <param name="eventName">The name of the event to fire</param>
	// <param name="data">The data to pass with the event</param>
	public static void Fire(string eventName, params object[] data) {
		Publish(eventName.GetHashCode(), eventName, data);
	}
}

