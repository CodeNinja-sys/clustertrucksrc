using System;
using UnityEngine;

// Token: 0x020002FF RID: 767
public class scrollingTruck : MonoBehaviour
{
	// Token: 0x06001214 RID: 4628 RVA: 0x00073728 File Offset: 0x00071928
	private void Start()
	{
	}

	// Token: 0x06001215 RID: 4629 RVA: 0x0007372C File Offset: 0x0007192C
	private void Update()
	{
		if (base.transform.position.x < 1080f)
		{
			base.transform.position = new Vector3(1284f, base.transform.position.y, base.transform.position.z);
		}
	}
}
