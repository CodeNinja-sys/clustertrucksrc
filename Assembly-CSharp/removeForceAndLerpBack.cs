using System;
using UnityEngine;

// Token: 0x020002FA RID: 762
public class removeForceAndLerpBack : MonoBehaviour
{
	// Token: 0x060011F3 RID: 4595 RVA: 0x000730E4 File Offset: 0x000712E4
	private void Start()
	{
		this.startPos = base.transform.localPosition;
	}

	// Token: 0x060011F4 RID: 4596 RVA: 0x000730F8 File Offset: 0x000712F8
	private void Update()
	{
		base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, this.startPos, Time.deltaTime * this.speedBack);
		base.transform.Translate(this.force * Time.deltaTime, Space.World);
	}

	// Token: 0x060011F5 RID: 4597 RVA: 0x00073150 File Offset: 0x00071350
	private void FixedUpdate()
	{
		this.force *= this.friction;
	}

	// Token: 0x060011F6 RID: 4598 RVA: 0x0007316C File Offset: 0x0007136C
	public void addForce(float f)
	{
		this.force += base.transform.forward * -f;
	}

	// Token: 0x04000F0B RID: 3851
	private Vector3 force = Vector3.zero;

	// Token: 0x04000F0C RID: 3852
	private Vector3 startPos;

	// Token: 0x04000F0D RID: 3853
	public float friction = 0.8f;

	// Token: 0x04000F0E RID: 3854
	public float speedBack = 1f;
}
