using System;
using System.Collections.Generic;

public delegate void MsgDelegate();

public static class MessengerManager {

	public static Dictionary<string, Delegate> dic = new Dictionary<string, Delegate>();

	public static void AddListener(string name, MsgDelegate msg) {

		if(!dic.ContainsKey(name)) {
		
			dic.Add(name, null);
		
		}
		
		dic[name] = (MsgDelegate)dic[name] + msg;

	}

	public static void RemoveListener(string name, MsgDelegate msg) {
	
		if(dic.ContainsKey(name)) {
			
			dic[name] = (MsgDelegate)dic[name] - msg;

			if(dic[name] == null) {
			
				dic.Remove(name);
			
			}
			
		}
	
	}

	public static void Involve(string name) {
	
		Delegate _callback;

		if(dic.TryGetValue(name, out _callback)) {
		
			MsgDelegate callback = (MsgDelegate)_callback;

			if(callback != null)
				callback();
		
		}

	
	}

}

public delegate void MsgDelegate<T>(T info);

public static class MessengerManager<T> {
	
	public static Dictionary<string, Delegate> dic = new Dictionary<string, Delegate>();
	
	public static void AddListener(string name, MsgDelegate<T> msg) {
		
		if(!dic.ContainsKey(name)) {
			
			dic.Add(name, null);
			
		}
			
		dic[name] = (MsgDelegate<T>)dic[name] + msg;

	}
	
	public static void RemoveListener(string name, MsgDelegate<T> msg) {
		
		if(dic.ContainsKey(name)) {
			
			dic[name] = (MsgDelegate<T>)dic[name] - msg;

			if(dic[name] == null) {
				
				dic.Remove(name);
				
			}
			
		}
		
	}
	
	public static void Involve(string name, T info) {
		
		Delegate _callback;
		
		if(dic.TryGetValue(name, out _callback)) {
			
			MsgDelegate<T> callback = (MsgDelegate<T>)_callback;
			
			if(callback != null)
				callback(info);
			
		}
		
	}
	
}
