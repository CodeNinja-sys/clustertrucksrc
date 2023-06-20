using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200024F RID: 591
public class ObjectEventHandler : MonoBehaviour
{
	// Token: 0x17000292 RID: 658
	// (get) Token: 0x06000E66 RID: 3686 RVA: 0x0005DB9C File Offset: 0x0005BD9C
	private bool HasTouchAllEvents
	{
		get
		{
			return this.AllFireableEvents.FindAll((ObjectEventHandler.FireableEvent index) => index.Type == EventToolLogic.EventTypes.OnTruckTouchAll).Count > 0;
		}
	}

	// Token: 0x06000E67 RID: 3687 RVA: 0x0005DBDC File Offset: 0x0005BDDC
	private void Awake()
	{
		foreach (Collider collider in base.GetComponentsInChildren<Collider>())
		{
			collider.gameObject.AddComponent<EventTriggerCheck>().Init(this);
		}
	}

	// Token: 0x06000E68 RID: 3688 RVA: 0x0005DC1C File Offset: 0x0005BE1C
	public void Initialize(EventInfo[] eventsTotrigger)
	{
		Debug.Log(string.Concat(new object[]
		{
			base.name,
			" Got: ",
			eventsTotrigger.Length,
			" Events!"
		}));
		for (int i = 0; i < eventsTotrigger.Length; i++)
		{
			Debug.Log(eventsTotrigger[i].EventType.ToString() + " " + eventsTotrigger[i].TriggerEvent.ToString(), base.gameObject);
			EventInfo eventInfo = eventsTotrigger[i];
			this.AllFireableEvents.Add(new ObjectEventHandler.FireableEvent(eventInfo.EntityBlock, eventInfo.TriggerEvent, eventInfo.EventType, eventInfo.Delay, eventInfo.OnlyOnce, eventInfo.Parameters));
		}
		base.StartCoroutine(this.FireEventsFor(EventToolLogic.EventTypes.Timed, null));
	}

	// Token: 0x06000E69 RID: 3689 RVA: 0x0005DCF4 File Offset: 0x0005BEF4
	private void Start()
	{
	}

	// Token: 0x06000E6A RID: 3690 RVA: 0x0005DCF8 File Offset: 0x0005BEF8
	private void Update()
	{
	}

	// Token: 0x06000E6B RID: 3691 RVA: 0x0005DCFC File Offset: 0x0005BEFC
	private void OnCollisionEnter(Collision other)
	{
		if (other.transform.tag == "Player")
		{
			Debug.Log("Hit Player!");
		}
	}

	// Token: 0x06000E6C RID: 3692 RVA: 0x0005DD30 File Offset: 0x0005BF30
	private void OnTriggerStay(Collider trig)
	{
		if (this.HasTouchAllEvents)
		{
		}
	}

	// Token: 0x06000E6D RID: 3693 RVA: 0x0005DD40 File Offset: 0x0005BF40
	public IEnumerator FireEventsFor(EventToolLogic.EventTypes eventType, GameObject that = null)
	{
		Debug.Log("Firing Events for: " + eventType.ToString());
		ObjectEventHandler.FireableEvent[] currentFireableList = this.AllFireableEvents.FindAll((ObjectEventHandler.FireableEvent item) => this.EvaluateEventType(item, eventType)).ToArray();
		for (int i = 0; i < currentFireableList.Length; i++)
		{
			ObjectEventHandler.FireableEvent eventToFire = (currentFireableList[i].Entity == null) ? new ObjectEventHandler.FireableEvent(currentFireableList[i], that) : currentFireableList[i];
			this.FireEvent(eventToFire);
			if (currentFireableList[i].OnlyOnce)
			{
				this.AllFireableEvents.Remove(currentFireableList[i]);
			}
		}
		yield return null;
		yield break;
	}

	// Token: 0x06000E6E RID: 3694 RVA: 0x0005DD78 File Offset: 0x0005BF78
	private bool EvaluateEventType(ObjectEventHandler.FireableEvent check, EventToolLogic.EventTypes eventType)
	{
		return (eventType.ToString().ToLower().Contains("touch") && check.Type == EventToolLogic.EventTypes.OnAnyTouch) || check.Type == eventType;
	}

	// Token: 0x06000E6F RID: 3695 RVA: 0x0005DDC4 File Offset: 0x0005BFC4
	public void FireEvent(ObjectEventHandler.FireableEvent eventToFire)
	{
		GameObject gameObject = (!(eventToFire.EntityGo == null)) ? eventToFire.EntityGo : base.gameObject;
		if (gameObject == null)
		{
			return;
		}
		Debug.Log("Firing Event! " + eventToFire.TriggerEvent.ToString() + ": " + gameObject.name);
		switch (eventToFire.TriggerEvent)
		{
		case EventToolLogic.TriggerEvents.Destroy:
			base.StartCoroutine(this.DestroyEvent(gameObject, eventToFire.Delay));
			break;
		case EventToolLogic.TriggerEvents.Stop:
			gameObject.BroadcastMessage("Stop");
			break;
		case EventToolLogic.TriggerEvents.Go:
			gameObject.BroadcastMessage("Go");
			break;
		case EventToolLogic.TriggerEvents.AddSpin:
			this.AddBehaviour(BehaviourToolLogic.BehaviourTypes.Spin, gameObject, eventToFire.Params);
			break;
		case EventToolLogic.TriggerEvents.AddRigidBody:
			this.AddBehaviour(BehaviourToolLogic.BehaviourTypes.RigidBody, gameObject, eventToFire.Params);
			break;
		case EventToolLogic.TriggerEvents.AddNoGravity:
			this.AddBehaviour(BehaviourToolLogic.BehaviourTypes.NoGravity, gameObject, eventToFire.Params);
			break;
		default:
			Debug.LogError("Invalid TriggerEvent: " + eventToFire.TriggerEvent);
			break;
		}
	}

	// Token: 0x06000E70 RID: 3696 RVA: 0x0005DEE8 File Offset: 0x0005C0E8
	private IEnumerator DestroyEvent(GameObject entity, float delay = 0f)
	{
		Debug.Log("DESTROY! Delay: " + delay, entity);
		yield return new WaitForSeconds(delay);
		UnityEngine.Object.Destroy(entity);
		yield break;
	}

	// Token: 0x06000E71 RID: 3697 RVA: 0x0005DF18 File Offset: 0x0005C118
	private void AddBehaviour(BehaviourToolLogic.BehaviourTypes Type, GameObject go, string[] _params)
	{
		switch (Type)
		{
		case BehaviourToolLogic.BehaviourTypes.Spin:
		{
			if (go.GetComponent<spin>())
			{
				return;
			}
			spin spin = go.AddComponent<spin>();
			spin.Initalize(_params);
			break;
		}
		case BehaviourToolLogic.BehaviourTypes.RigidBody:
			go.AddComponent<Rigidbody>();
			break;
		case BehaviourToolLogic.BehaviourTypes.NoGravity:
			go.GetComponent<Rigidbody>().useGravity = false;
			break;
		default:
			throw new Exception("Inavlid Behaviour Type: " + Type.ToString() + " Is not Setup!");
		}
	}

	// Token: 0x04000B09 RID: 2825
	private List<ObjectEventHandler.FireableEvent> AllFireableEvents = new List<ObjectEventHandler.FireableEvent>();

	// Token: 0x02000250 RID: 592
	public class FireableEvent
	{
		// Token: 0x06000E73 RID: 3699 RVA: 0x0005DFAC File Offset: 0x0005C1AC
		public FireableEvent(levelEditorManager.Block _Entity, EventToolLogic.TriggerEvents _EventToTrigger, EventToolLogic.EventTypes _Type, float _Delay = 0f, bool _onlyOnce = false, string[] _Parameters = null)
		{
			this._entity = ((_Entity.Index != -1) ? _Entity : null);
			this._triggerEvent = _EventToTrigger;
			this._type = _Type;
			this._delay = _Delay / 100f;
			this._params = _Parameters;
			this._onlyOnce = _onlyOnce;
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x0005E004 File Offset: 0x0005C204
		public FireableEvent(ObjectEventHandler.FireableEvent other, GameObject target)
		{
			this._entity = new levelEditorManager.Block
			{
				go = target
			};
			this._triggerEvent = other.TriggerEvent;
			this._type = other.Type;
			this._delay = other.Delay / 100f;
			this._params = other.Params;
			this._onlyOnce = other.OnlyOnce;
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000E75 RID: 3701 RVA: 0x0005E070 File Offset: 0x0005C270
		public EventToolLogic.EventTypes Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000E76 RID: 3702 RVA: 0x0005E078 File Offset: 0x0005C278
		public levelEditorManager.Block Entity
		{
			get
			{
				return this._entity;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000E77 RID: 3703 RVA: 0x0005E080 File Offset: 0x0005C280
		public GameObject EntityGo
		{
			get
			{
				return this._entity.go;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000E78 RID: 3704 RVA: 0x0005E090 File Offset: 0x0005C290
		public EventToolLogic.TriggerEvents TriggerEvent
		{
			get
			{
				return this._triggerEvent;
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000E79 RID: 3705 RVA: 0x0005E098 File Offset: 0x0005C298
		public float Delay
		{
			get
			{
				return this._delay;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000E7A RID: 3706 RVA: 0x0005E0A0 File Offset: 0x0005C2A0
		public string[] Params
		{
			get
			{
				return this._params;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000E7B RID: 3707 RVA: 0x0005E0A8 File Offset: 0x0005C2A8
		public bool OnlyOnce
		{
			get
			{
				return this._onlyOnce;
			}
		}

		// Token: 0x04000B0B RID: 2827
		private EventToolLogic.EventTypes _type;

		// Token: 0x04000B0C RID: 2828
		private levelEditorManager.Block _entity;

		// Token: 0x04000B0D RID: 2829
		private EventToolLogic.TriggerEvents _triggerEvent;

		// Token: 0x04000B0E RID: 2830
		private float _delay;

		// Token: 0x04000B0F RID: 2831
		private string[] _params;

		// Token: 0x04000B10 RID: 2832
		private bool _onlyOnce;
	}
}
