using System;
using UnityEngine;

// Token: 0x020001F1 RID: 497
public class Gravity : MonoBehaviour
{
	// Token: 0x06000BBE RID: 3006 RVA: 0x00048FA8 File Offset: 0x000471A8
	private void Start()
	{
		this.rig = base.GetComponent<Rigidbody>();
	}

	// Token: 0x06000BBF RID: 3007 RVA: 0x00048FB8 File Offset: 0x000471B8
	private void FixedUpdate()
	{
		if (this.useGravity)
		{
			this.rig.AddForce(this.gravity, ForceMode.Acceleration);
		}
	}

	// Token: 0x06000BC0 RID: 3008 RVA: 0x00048FD8 File Offset: 0x000471D8
	public void SetDirection(Vector3 dir)
	{
		this.gravity = Vector3.Normalize(dir) * 9.82f;
	}

	// Token: 0x0400085B RID: 2139
	public Vector3 gravity;

	// Token: 0x0400085C RID: 2140
	private Rigidbody rig;

	// Token: 0x0400085D RID: 2141
	public bool useGravity = true;
}
