using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000202 RID: 514
public class Event_CellSet : MonoBehaviour
{
	// Token: 0x1700025B RID: 603
	// (get) Token: 0x06000C31 RID: 3121 RVA: 0x0004C648 File Offset: 0x0004A848
	public float Delay
	{
		get
		{
			return this._delay;
		}
	}

	// Token: 0x1700025C RID: 604
	// (get) Token: 0x06000C32 RID: 3122 RVA: 0x0004C650 File Offset: 0x0004A850
	public bool OnlyOnce
	{
		get
		{
			return this._onlyOnce;
		}
	}

	// Token: 0x06000C33 RID: 3123 RVA: 0x0004C658 File Offset: 0x0004A858
	private void Start()
	{
	}

	// Token: 0x06000C34 RID: 3124 RVA: 0x0004C65C File Offset: 0x0004A85C
	private void Update()
	{
	}

	// Token: 0x06000C35 RID: 3125 RVA: 0x0004C660 File Offset: 0x0004A860
	public void ChangeValues(string EventType, string EventToTrigger, KeyValuePair<int, string> Entity, string[] Parameters = null)
	{
		this._eventType = EventType;
		this._eventToTrigger = EventToTrigger;
		this._entity = Entity;
		this._parameters = Parameters;
		this.TEventType.text = this._eventType;
		this.TEventToTrigger.text = this._eventToTrigger;
		this.TEntity.text = this._entity.Value.Split(new char[]
		{
			'_'
		})[0];
		this.TParameters.text = "NULL";
		if (this._parameters != null)
		{
			this.TParameters.text = ((this._parameters.Length <= 0) ? "NULL" : this._parameters[0]);
		}
	}

	// Token: 0x06000C36 RID: 3126 RVA: 0x0004C71C File Offset: 0x0004A91C
	public void ChangeValues(float Delay, bool OnlyOnce)
	{
		this._delay = Delay;
		this._onlyOnce = OnlyOnce;
		this.TDelay.text = this._delay.ToString("F2");
		this.TOnlyOnce.text = this._onlyOnce.ToString();
	}

	// Token: 0x06000C37 RID: 3127 RVA: 0x0004C768 File Offset: 0x0004A968
	public KeyValuePair<int, string> GetProperty(int prop)
	{
		switch (prop)
		{
		case 1:
		{
			KeyValuePair<int, string> result = new KeyValuePair<int, string>((int)Enum.Parse(typeof(EventToolLogic.EventTypes), this._eventType), this._eventType);
			return result;
		}
		case 2:
		{
			KeyValuePair<int, string> result = new KeyValuePair<int, string>((int)Enum.Parse(typeof(EventToolLogic.TriggerEvents), this._eventToTrigger), this._eventToTrigger);
			return result;
		}
		case 3:
			return this._entity;
		default:
			throw new Exception("Cannot finf prop! " + prop);
		}
	}

	// Token: 0x06000C38 RID: 3128 RVA: 0x0004C800 File Offset: 0x0004AA00
	public string[] GetParameters()
	{
		return this._parameters;
	}

	// Token: 0x040008CD RID: 2253
	private KeyValuePair<int, string> _entity;

	// Token: 0x040008CE RID: 2254
	private string _eventType;

	// Token: 0x040008CF RID: 2255
	private string _eventToTrigger;

	// Token: 0x040008D0 RID: 2256
	private string[] _parameters;

	// Token: 0x040008D1 RID: 2257
	private float _delay;

	// Token: 0x040008D2 RID: 2258
	private bool _onlyOnce;

	// Token: 0x040008D3 RID: 2259
	public Text TEventType;

	// Token: 0x040008D4 RID: 2260
	public Text TEventToTrigger;

	// Token: 0x040008D5 RID: 2261
	public Text TEntity;

	// Token: 0x040008D6 RID: 2262
	public Text TParameters;

	// Token: 0x040008D7 RID: 2263
	public Text TDelay;

	// Token: 0x040008D8 RID: 2264
	public Text TOnlyOnce;
}
