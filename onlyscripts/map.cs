using System;
using UnityEngine;

// Token: 0x02000292 RID: 658
public class map : MonoBehaviour
{
	// Token: 0x06000FC6 RID: 4038 RVA: 0x000664E4 File Offset: 0x000646E4
	private void Awake()
	{
		info.explosive = this.explosive;
		info.truckWidth = this.truckWidth;
	}

	// Token: 0x06000FC7 RID: 4039 RVA: 0x000664FC File Offset: 0x000646FC
	private void Update()
	{
	}

	// Token: 0x04000C96 RID: 3222
	public float explosive = 1f;

	// Token: 0x04000C97 RID: 3223
	public float truckWidth = 1f;
}
