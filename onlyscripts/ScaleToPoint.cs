using System;
using UnityEngine;

// Token: 0x02000255 RID: 597
public class ScaleToPoint : MonoBehaviour
{
	// Token: 0x06000E90 RID: 3728 RVA: 0x0005E2F0 File Offset: 0x0005C4F0
	private void Start()
	{
	}

	// Token: 0x06000E91 RID: 3729 RVA: 0x0005E2F4 File Offset: 0x0005C4F4
	private void LateUpdate()
	{
		if (this.x)
		{
			base.transform.localScale = new Vector3(Vector3.Distance(base.transform.position, this.otherPoint.position), base.transform.localScale.y, base.transform.localScale.z);
		}
		if (this.y)
		{
			base.transform.localScale = new Vector3(base.transform.localScale.x, Vector3.Distance(base.transform.position, this.otherPoint.position), base.transform.localScale.z);
		}
		if (this.z)
		{
			base.transform.localScale = new Vector3(base.transform.localScale.x, base.transform.localScale.y, Vector3.Distance(base.transform.position, this.otherPoint.position));
		}
	}

	// Token: 0x04000B17 RID: 2839
	public Transform otherPoint;

	// Token: 0x04000B18 RID: 2840
	public bool x;

	// Token: 0x04000B19 RID: 2841
	public bool y;

	// Token: 0x04000B1A RID: 2842
	public bool z;
}
