using System;
using UnityEngine;

// Token: 0x020002DC RID: 732
public class Translate : MonoBehaviour
{
	// Token: 0x0600113C RID: 4412 RVA: 0x0007043C File Offset: 0x0006E63C
	private void Start()
	{
	}

	// Token: 0x0600113D RID: 4413 RVA: 0x00070440 File Offset: 0x0006E640
	private void Update()
	{
		base.transform.Translate(this.movement * Time.deltaTime);
	}

	// Token: 0x04000E6B RID: 3691
	public Vector3 movement = Vector3.zero;
}
