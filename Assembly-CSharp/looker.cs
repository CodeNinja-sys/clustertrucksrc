using System;
using UnityEngine;

// Token: 0x02000290 RID: 656
public class looker : MonoBehaviour
{
	// Token: 0x06000FBE RID: 4030 RVA: 0x00066370 File Offset: 0x00064570
	private void Awake()
	{
		this.myCar = base.transform.root.GetComponent<car>();
	}

	// Token: 0x06000FBF RID: 4031 RVA: 0x00066388 File Offset: 0x00064588
	private void Update()
	{
	}

	// Token: 0x06000FC0 RID: 4032 RVA: 0x0006638C File Offset: 0x0006458C
	private void OnTriggerEnter(Collider other)
	{
		if (!other.isTrigger)
		{
			this.myCar.RoadTarget(other.transform);
		}
	}

	// Token: 0x04000C92 RID: 3218
	private car myCar;
}
