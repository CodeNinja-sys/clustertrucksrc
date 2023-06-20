using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000217 RID: 535
public class RoadHandle : MonoBehaviour
{
	// Token: 0x17000263 RID: 611
	// (get) Token: 0x06000C7F RID: 3199 RVA: 0x0004D5DC File Offset: 0x0004B7DC
	public bool PermanentConnection
	{
		get
		{
			return this.currentConnection != null && !this.isActivated;
		}
	}

	// Token: 0x17000264 RID: 612
	// (get) Token: 0x06000C80 RID: 3200 RVA: 0x0004D5F8 File Offset: 0x0004B7F8
	public roadHandleConnection currentConnection
	{
		get
		{
			if (this._currentConnection == null && !this.isActivated)
			{
				this.Activate();
			}
			return this._currentConnection;
		}
	}

	// Token: 0x17000265 RID: 613
	// (get) Token: 0x06000C81 RID: 3201 RVA: 0x0004D628 File Offset: 0x0004B828
	private bool isActivated
	{
		get
		{
			return base.GetComponent<Renderer>().enabled && base.GetComponent<Collider>().enabled;
		}
	}

	// Token: 0x17000266 RID: 614
	// (get) Token: 0x06000C82 RID: 3202 RVA: 0x0004D654 File Offset: 0x0004B854
	public GameObject connectionGameObject
	{
		get
		{
			if (this._connectionObject == null)
			{
				this.Activate();
			}
			else if (!this._connectionObject.activeInHierarchy)
			{
				this.Activate();
			}
			return this._connectionObject;
		}
	}

	// Token: 0x06000C83 RID: 3203 RVA: 0x0004D69C File Offset: 0x0004B89C
	private void Awake()
	{
		this._name = base.transform.root.name;
		foreach (RoadHandle roadHandle in base.transform.parent.GetComponentsInChildren<RoadHandle>())
		{
			if (roadHandle != base.GetComponent<RoadHandle>())
			{
				this.otherRoadhandles.Add(roadHandle);
			}
		}
	}

	// Token: 0x06000C84 RID: 3204 RVA: 0x0004D708 File Offset: 0x0004B908
	public void Initialize()
	{
		Debug.Log("INITIALIZE: ", base.transform);
		this.tryMakeConnection();
	}

	// Token: 0x06000C85 RID: 3205 RVA: 0x0004D720 File Offset: 0x0004B920
	private void Update()
	{
		this._PermanentConnection = this.PermanentConnection;
		if (!this._connectionObject && !this.isActivated)
		{
			this.Activate();
		}
		else if (!this.isActivated && this._connectionObject)
		{
			if (!this._connectionObject.activeInHierarchy)
			{
				this.Activate();
				return;
			}
			if (Vector3.Distance(this._connectionObject.transform.position, base.transform.position) > 5f)
			{
				this.Activate();
			}
		}
		else if (this._connectionObject && this._connectionObject.activeInHierarchy)
		{
			this.Deactivate();
		}
	}

	// Token: 0x06000C86 RID: 3206 RVA: 0x0004D7EC File Offset: 0x0004B9EC
	public RoadHandle.blockFamily getBlockFamily()
	{
		if (this._name.ToLower().Contains(RoadHandle.blockFamily.castle.ToString().ToLower() + "road"))
		{
			return RoadHandle.blockFamily.castle;
		}
		return RoadHandle.blockFamily.blab;
	}

	// Token: 0x06000C87 RID: 3207 RVA: 0x0004D82C File Offset: 0x0004BA2C
	public void setReferenceObject(GameObject g)
	{
		Debug.Log("Connection Made for: ", base.gameObject);
		this._connectionObject = g;
	}

	// Token: 0x06000C88 RID: 3208 RVA: 0x0004D848 File Offset: 0x0004BA48
	public void Deactivate()
	{
		Debug.Log("Deactivating!");
		base.GetComponent<Renderer>().enabled = false;
		base.GetComponent<Collider>().enabled = false;
	}

	// Token: 0x06000C89 RID: 3209 RVA: 0x0004D878 File Offset: 0x0004BA78
	private void Activate()
	{
		Debug.Log("Activating!");
		base.GetComponent<Renderer>().enabled = true;
		base.GetComponent<Collider>().enabled = true;
	}

	// Token: 0x06000C8A RID: 3210 RVA: 0x0004D8A8 File Offset: 0x0004BAA8
	public Transform Search()
	{
		Collider[] array = Physics.OverlapSphere(base.transform.position, 20f);
		float num = float.PositiveInfinity;
		Transform transform = null;
		foreach (Collider collider in array)
		{
			if (collider.GetComponent<RoadHandle>() && collider.transform != base.transform && Vector3.Distance(collider.transform.position, base.transform.position) < num)
			{
				transform = collider.transform;
				num = Vector3.Distance(collider.transform.position, base.transform.position);
			}
		}
		if (transform)
		{
			Debug.Log("Found Another Roadhandle Partner: ", transform);
			if (this._currentConnection != null)
			{
				Debug.Log("Current Connection", this._currentConnection.getT2());
			}
			if (this._currentConnection == null)
			{
				this.connection = transform;
				this._currentConnection = new roadHandleConnection(base.transform, transform);
			}
			else if (this._currentConnection.getT2() != transform)
			{
				this.connection = transform;
				this._currentConnection = new roadHandleConnection(base.transform, transform);
			}
		}
		else
		{
			this.connection = null;
			this._currentConnection = null;
		}
		return transform;
	}

	// Token: 0x06000C8B RID: 3211 RVA: 0x0004DA04 File Offset: 0x0004BC04
	public void makeConnection(Transform connector)
	{
		this._connectionObject = connector.gameObject;
		this.Deactivate();
		Debug.Log("Connection Made For: ", base.transform);
		Debug.Log("With: ", connector);
		this._currentConnection = new roadHandleConnection(base.transform, connector);
		this.connection = connector;
		foreach (RoadHandle roadHandle in this.otherRoadhandles)
		{
			roadHandle.tryMakeConnection();
		}
	}

	// Token: 0x06000C8C RID: 3212 RVA: 0x0004DAB0 File Offset: 0x0004BCB0
	public void makeConnectionSecond(Transform connector)
	{
		this._connectionObject = connector.gameObject;
		this.Deactivate();
		Debug.Log("Connection Made For: ", base.transform);
		Debug.Log("With: ", connector);
		this._currentConnection = new roadHandleConnection(base.transform, connector);
		this.connection = connector;
	}

	// Token: 0x06000C8D RID: 3213 RVA: 0x0004DB04 File Offset: 0x0004BD04
	public void tryMakeConnection()
	{
		Debug.Log("Trying TO make Connection");
		Collider[] array = Physics.OverlapSphere(base.transform.position, 0.5f);
		float num = float.PositiveInfinity;
		RoadHandle roadHandle = null;
		foreach (Collider collider in array)
		{
			if (collider.GetComponent<RoadHandle>() && collider.transform != base.transform && Vector3.Distance(collider.transform.position, base.transform.position) < num)
			{
				roadHandle = collider.GetComponent<RoadHandle>();
				num = Vector3.Distance(collider.transform.position, base.transform.position);
			}
		}
		if (roadHandle)
		{
			Debug.Log("Trying:: ", roadHandle);
			this.makeConnectionSecond(roadHandle.transform);
			roadHandle.makeConnectionSecond(base.transform);
		}
	}

	// Token: 0x04000904 RID: 2308
	public List<RoadHandle> otherRoadhandles = new List<RoadHandle>();

	// Token: 0x04000905 RID: 2309
	[SerializeField]
	public bool _PermanentConnection;

	// Token: 0x04000906 RID: 2310
	private roadHandleConnection _currentConnection;

	// Token: 0x04000907 RID: 2311
	public Transform connection;

	// Token: 0x04000908 RID: 2312
	public bool test = true;

	// Token: 0x04000909 RID: 2313
	private string _name = string.Empty;

	// Token: 0x0400090A RID: 2314
	private GameObject _connectionObject;

	// Token: 0x02000218 RID: 536
	public enum blockFamily
	{
		// Token: 0x0400090C RID: 2316
		castle,
		// Token: 0x0400090D RID: 2317
		bla,
		// Token: 0x0400090E RID: 2318
		blab
	}
}
