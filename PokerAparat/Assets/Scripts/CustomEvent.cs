using UnityEngine;
using System.Collections.Generic;
using System;

public class CustomEvent<T1, T2, T3>
{
	private const string _listenerExists = "Prevented adding duplicate listener.";

	private const string _listenerDoesNotExist = "Prevented removing non-existant listener.";

	private const string _emptyEventInvoke = "No events subscribed to invoke event.";

	private event Action<T1, T2, T3> _onEvent;

	private HashSet<Action<T1, T2, T3>> _listeners = new HashSet<Action<T1, T2, T3>>();

	public int Count
	{
		get
		{
			return _listeners.Count;
		}
	}

	public void AddListener(Action<T1, T2, T3> listener)
	{
		if (!_listeners.Contains(listener))
		{
			_onEvent += listener;
			_listeners.Add(listener);
		}
	}

	public void RemoveListener(Action<T1, T2, T3> listener)
	{
		if (_listeners.Contains(listener))
		{
			_onEvent -= listener;
			_listeners.Remove(listener);
		}
	}

	public void Invoke(T1 parameter1, T2 parameter2, T3 parameter3)
	{
		if (_onEvent != null)
		{
			_onEvent(parameter1, parameter2, parameter3);
		}
#if DEEP_DEBUG
        else
            Debug.LogError(_emptyEventInvoke);
#endif
	}

	public void Clear()
	{
		_listeners.Clear();
		_onEvent = null;
	}
}

public class CustomEvent<T1, T2>
{
	private const string _listenerExists = "Prevented adding duplicate listener.";

	private const string _listenerDoesNotExist = "Prevented removing non-existant listener.";

	private const string _emptyEventInvoke = "No events subscribed to invoke event.";

	private event Action<T1, T2> _onEvent;

	private HashSet<Action<T1, T2>> _listeners = new HashSet<Action<T1, T2>>();

	public int Count
	{
		get
		{
			return _listeners.Count;
		}
	}

	public void AddListener(Action<T1, T2> listener)
	{
		if (!_listeners.Contains(listener))
		{
			_onEvent += listener;
			_listeners.Add(listener);
		}
	}

	public void RemoveListener(Action<T1, T2> listener)
	{
		if (_listeners.Contains(listener))
		{
			_onEvent -= listener;
			_listeners.Remove(listener);
		}
	}

	public void Invoke(T1 parameter1, T2 parameter2)
	{
		if (_onEvent != null)
		{
			_onEvent(parameter1, parameter2);
		}
#if DEEP_DEBUG
        else
            Debug.LogError(_emptyEventInvoke);
#endif
	}

	public void Clear()
	{
		_listeners.Clear();
		_onEvent = null;
	}
}

public class CustomEvent<T>
{
	private const string _listenerExists = "Prevented adding duplicate listener.";

	private const string _listenerDoesNotExist = "Prevented removing non-existant listener.";

	private const string _emptyEventInvoke = "No events subscribed to invoke event.";

	private event Action<T> _onEvent;

	private HashSet<Action<T>> _listeners = new HashSet<Action<T>>();

	public int Count
	{
		get
		{
			return _listeners.Count;
		}
	}

	public void AddListener(Action<T> listener)
	{
		if (!_listeners.Contains(listener))
		{
			_onEvent += listener;
			_listeners.Add(listener);
		}
	}

	public void RemoveListener(Action<T> listener)
	{
		if (_listeners.Contains(listener))
		{
			_onEvent -= listener;
			_listeners.Remove(listener);
		}
	}

	public void Invoke(T parameter)
	{
		if (_onEvent != null)
		{
			_onEvent(parameter);
		}
#if DEEP_DEBUG
        else
            Debug.LogError(_emptyEventInvoke);
#endif
	}

	public void Clear()
	{
		_listeners.Clear();
		_onEvent = null;
	}

	public bool Exists(Action<T> listener)
	{
		return _listeners.Contains(listener);
	}
}

public class CustomEvent
{
	private const string _listenerExists = "Prevented adding duplicate listener.";

	private const string _listenerDoesNotExist = "Prevented removing non-existant listener.";

	private const string _emptyEventInvoke = "No events subscribed to invoke event.";

	private event Action _onEvent;

	private HashSet<Action> _listeners = new HashSet<Action>();

	public int Count
	{
		get
		{
			return _listeners.Count;
		}
	}

	public void AddListener(Action listener)
	{
		if (!_listeners.Contains(listener))
		{
			_onEvent += listener;
			_listeners.Add(listener);
		}
	}

	public void RemoveListener(Action listener)
	{
		if (_listeners.Contains(listener))
		{
			_onEvent -= listener;
			_listeners.Remove(listener);
		}
	}

	public void Invoke()
	{
		if (_onEvent != null)
		{
			_onEvent();
		}
#if DEEP_DEBUG
        else
            Debug.LogError(_emptyEventInvoke);
#endif
	}

	public void Clear()
	{
		_listeners.Clear();
		_onEvent = null;
	}
}