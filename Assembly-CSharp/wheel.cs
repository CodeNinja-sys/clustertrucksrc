using System;
using UnityEngine;

// Token: 0x020002C6 RID: 710
public class wheel : MonoBehaviour
{
	// Token: 0x060010E7 RID: 4327 RVA: 0x0006E388 File Offset: 0x0006C588
	private void Start()
	{
		this.myCar = base.transform.root.GetComponent<car>();
	}

	// Token: 0x060010E8 RID: 4328 RVA: 0x0006E3A0 File Offset: 0x0006C5A0
	private void Update()
	{
	}

	// Token: 0x04000E0A RID: 3594
	private car myCar;
}
