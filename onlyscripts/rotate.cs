using System;
using UnityEngine;

// Token: 0x020002AE RID: 686
public class rotate : MonoBehaviour
{
	// Token: 0x06001056 RID: 4182 RVA: 0x0006A90C File Offset: 0x00068B0C
	private void Start()
	{
		this.rig = base.gameObject.GetComponent<Rigidbody>();
	}

	// Token: 0x06001057 RID: 4183 RVA: 0x0006A920 File Offset: 0x00068B20
	private void FixedUpdate()
	{
		if (this.relative)
		{
			this.rig.AddRelativeTorque(this.dir);
		}
		else
		{
			this.rig.AddTorque(this.dir);
		}
	}

	// Token: 0x04000D60 RID: 3424
	private Rigidbody rig;

	// Token: 0x04000D61 RID: 3425
	public Vector3 dir;

	// Token: 0x04000D62 RID: 3426
	public bool relative;
}
