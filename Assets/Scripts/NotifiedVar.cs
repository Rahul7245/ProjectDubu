using System;
using System.Collections.Generic;

/*
 * We have created our own generic variable to make the system MVC and Observer pattern.
 * It is a subscription based model where modules are independent of each other.
 */

[Serializable]
public class NotifiedVar<T>
{
	private T val;
	[NonSerialized]
	private Action evt;

	/// <summary>
	/// Variable to avoid stackoverflow in case of cyclic dependency
	/// </summary>
	[NonSerialized]
	private object caller;

	public T Value
	{
		get { return val; }
		set { val = value; Invoke(); }
	}

	/// <summary>
	/// for object type references
	/// </summary>
	// public void Changed()
	// {
	// 	Invoke();
	// }

	private void Invoke()
	{
		if (caller == this) return;
		caller = this;
		evt?.Invoke();
		caller = null;
	}

	public NotifiedVar(T defaultVal)
	{
		val = defaultVal;
		evt = default;
	}

	public void Subscribe(Action onChange)
	{
		evt += onChange;
	}

	public void Unsubscribe(Action onChange)
	{
		evt -= onChange;
	}
}
	
	
	[Serializable]
	public class Notify
	{
		[NonSerialized]
		private Action evt;

		/// <summary>
		/// Variable to avoid stackoverflow in case of cyclic dependency
		/// </summary>
		[NonSerialized]
		private object caller;

		/// <summary>
		/// for object type references
		/// </summary>
	

		public void Invoke()
		{
			if (caller == this) return;
			caller = this;
			evt?.Invoke();
			caller = null;
		}

		public Notify()
		{
			evt = default;
		}

		public void Subscribe(Action onChange)
		{
			evt += onChange;
		}

		public void Unsubscribe(Action onChange)
		{
			evt -= onChange;
		}
	}