using System;
using UnityEngine;

// Token: 0x02000259 RID: 601
public class SwingingObject : MonoBehaviour
{
	// Token: 0x06000EA1 RID: 3745 RVA: 0x0005E804 File Offset: 0x0005CA04
	private void Start()
	{
		this.baseWaitTime += UnityEngine.Random.Range(0f, this.extraRandomTime);
	}

	// Token: 0x06000EA2 RID: 3746 RVA: 0x0005E824 File Offset: 0x0005CA24
	private void Update()
	{
		if (this.baseWaitTime < 0f && this.isOn)
		{
			this.rig.useGravity = true;
		}
		if (this.baseWaitTime < 0f && !this.used)
		{
			this.rig.constraints = RigidbodyConstraints.None;
			this.rig.AddForce(Vector3.up * 5f, ForceMode.VelocityChange);
			this.used = true;
		}
		this.baseWaitTime -= Time.deltaTime;
	}

	// Token: 0x06000EA3 RID: 3747 RVA: 0x0005E8B4 File Offset: 0x0005CAB4
	private void FixedUpdate()
	{
		if (!this.isOn)
		{
			this.rig.angularVelocity *= 0.9f;
			this.rig.useGravity = false;
		}
	}

	// Token: 0x06000EA4 RID: 3748 RVA: 0x0005E8F4 File Offset: 0x0005CAF4
	private void sendInfo(float[] info)
	{
		this.baseWaitTime = info[0];
		this.extraRandomTime = info[1];
	}

	// Token: 0x06000EA5 RID: 3749 RVA: 0x0005E908 File Offset: 0x0005CB08
	private void Pull()
	{
		this.isOn = !this.isOn;
	}

	// Token: 0x04000B2E RID: 2862
	public float baseWaitTime;

	// Token: 0x04000B2F RID: 2863
	public float extraRandomTime;

	// Token: 0x04000B30 RID: 2864
	public Rigidbody rig;

	// Token: 0x04000B31 RID: 2865
	private bool isOn = true;

	// Token: 0x04000B32 RID: 2866
	private bool used;
}
