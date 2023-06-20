using System;
using UnityEngine;

// Token: 0x0200013C RID: 316
public class Move : MonoBehaviour
{
	// Token: 0x060006D0 RID: 1744 RVA: 0x0002DD60 File Offset: 0x0002BF60
	private void Start()
	{
	}

	// Token: 0x060006D1 RID: 1745 RVA: 0x0002DD64 File Offset: 0x0002BF64
	private void Update()
	{
		base.transform.Translate(this.dir * Time.deltaTime);
	}

	// Token: 0x04000507 RID: 1287
	public Vector3 dir;
}
