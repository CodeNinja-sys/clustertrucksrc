using System;
using UnityEngine;

// Token: 0x020002B6 RID: 694
public class smasher : MonoBehaviour
{
	// Token: 0x0600107C RID: 4220 RVA: 0x0006B440 File Offset: 0x00069640
	private void Start()
	{
		this.counter = this.waitStart + UnityEngine.Random.Range(0f, this.random);
		this.rig.SetMaxAngularVelocity(100f);
		this.rig.angularVelocity *= 0f;
		this.rig.velocity *= 0f;
		this.rig.useGravity = true;
		this.rig.transform.localRotation = Quaternion.identity;
	}

	// Token: 0x0600107D RID: 4221 RVA: 0x0006B4D4 File Offset: 0x000696D4
	private void Update()
	{
		this.counter -= Time.deltaTime;
		if (this.counter < 1.5f && !this.done)
		{
			this.shake += Time.deltaTime * 0.1f;
			if (this.shakeCd > 0.01f)
			{
				this.pos = UnityEngine.Random.insideUnitSphere * this.shake;
				this.shakeCd = 0f;
			}
			this.model.localPosition = Vector3.MoveTowards(this.model.localPosition, this.pos, Time.deltaTime * 30f);
			this.shakeCd += Time.deltaTime;
			if (this.counter < 0f)
			{
				this.model.localPosition = Vector3.zero;
				this.shake = 0f;
				this.counter = 3f;
				this.rig.AddTorque(this.rig.transform.up * -this.force, ForceMode.VelocityChange);
				this.done = true;
			}
		}
	}

	// Token: 0x0600107E RID: 4222 RVA: 0x0006B5FC File Offset: 0x000697FC
	private void FixedUpdate()
	{
		if (this.counter < 2f && this.done)
		{
			this.rig.angularVelocity *= 0.5f;
			this.rig.transform.localRotation = Quaternion.Lerp(this.rig.transform.localRotation, Quaternion.identity, 0.05f);
			if (this.counter < 0f)
			{
				this.counter = this.waitBetween + UnityEngine.Random.Range(0f, this.random);
				this.done = false;
			}
		}
		if (this.counter > 0f && !this.done)
		{
			this.rig.angularVelocity *= 0.5f;
			this.rig.transform.localRotation = Quaternion.identity;
		}
	}

	// Token: 0x0600107F RID: 4223 RVA: 0x0006B6F0 File Offset: 0x000698F0
	private void sendInfo(float[] info)
	{
		this.waitStart = info[0];
		this.waitBetween = info[1];
		this.random = info[2];
		this.force = info[3];
	}

	// Token: 0x04000D8A RID: 3466
	public Rigidbody rig;

	// Token: 0x04000D8B RID: 3467
	public Transform model;

	// Token: 0x04000D8C RID: 3468
	public float waitStart = 1f;

	// Token: 0x04000D8D RID: 3469
	public float waitBetween = 1f;

	// Token: 0x04000D8E RID: 3470
	public float random = 0.5f;

	// Token: 0x04000D8F RID: 3471
	public float force = 50f;

	// Token: 0x04000D90 RID: 3472
	private float counter;

	// Token: 0x04000D91 RID: 3473
	private bool done;

	// Token: 0x04000D92 RID: 3474
	private float shake;

	// Token: 0x04000D93 RID: 3475
	private float shakeCd;

	// Token: 0x04000D94 RID: 3476
	private Vector3 pos;
}
