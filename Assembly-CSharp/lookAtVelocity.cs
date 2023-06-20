using System;
using UnityEngine;

// Token: 0x0200028F RID: 655
public class lookAtVelocity : MonoBehaviour
{
	// Token: 0x06000FBB RID: 4027 RVA: 0x00066338 File Offset: 0x00064538
	private void Start()
	{
		this.rig = base.GetComponent<Rigidbody>();
	}

	// Token: 0x06000FBC RID: 4028 RVA: 0x00066348 File Offset: 0x00064548
	private void Update()
	{
		base.transform.rotation = Quaternion.LookRotation(this.rig.velocity);
	}

	// Token: 0x04000C91 RID: 3217
	private Rigidbody rig;
}
