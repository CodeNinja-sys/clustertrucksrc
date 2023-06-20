using System;
using UnityEngine;

// Token: 0x020002AD RID: 685
public class roadHandleConnection
{
	// Token: 0x06001050 RID: 4176 RVA: 0x0006A864 File Offset: 0x00068A64
	public roadHandleConnection(Transform t1, Transform t2)
	{
		this._t1 = t1;
		this._t2 = t2;
	}

	// Token: 0x170002AE RID: 686
	// (get) Token: 0x06001051 RID: 4177 RVA: 0x0006A87C File Offset: 0x00068A7C
	public float distanceBetween
	{
		get
		{
			return this.calculateDistance();
		}
	}

	// Token: 0x06001052 RID: 4178 RVA: 0x0006A884 File Offset: 0x00068A84
	public Transform getT1()
	{
		if (!this._t1)
		{
			Debug.LogError("Transform1 is null! Returning Null");
		}
		return this._t1;
	}

	// Token: 0x06001053 RID: 4179 RVA: 0x0006A8B4 File Offset: 0x00068AB4
	public Transform getT2()
	{
		if (!this._t2)
		{
			Debug.LogError("Transform2 is null! Returning Null");
		}
		return this._t2;
	}

	// Token: 0x06001054 RID: 4180 RVA: 0x0006A8E4 File Offset: 0x00068AE4
	private float calculateDistance()
	{
		return Vector3.Distance(this._t1.position, this._t2.position);
	}

	// Token: 0x04000D5E RID: 3422
	private Transform _t1;

	// Token: 0x04000D5F RID: 3423
	private Transform _t2;
}
