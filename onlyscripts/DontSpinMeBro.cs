using System;
using UnityEngine;

// Token: 0x0200001B RID: 27
public class DontSpinMeBro : MonoBehaviour
{
	// Token: 0x06000094 RID: 148 RVA: 0x00005CD0 File Offset: 0x00003ED0
	private void Start()
	{
		this.rot = base.transform.rotation;
	}

	// Token: 0x06000095 RID: 149 RVA: 0x00005CE4 File Offset: 0x00003EE4
	private void Update()
	{
		base.transform.rotation = this.rot;
	}

	// Token: 0x0400008C RID: 140
	private Quaternion rot;
}
