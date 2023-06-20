using System;
using UnityEngine;

// Token: 0x020002AA RID: 682
public class pusher : MonoBehaviour
{
	// Token: 0x06001044 RID: 4164 RVA: 0x00069F38 File Offset: 0x00068138
	private void Start()
	{
		this.untilNext += UnityEngine.Random.Range(0f, this.extraRandom) + this.baseWait;
		Debug.Log(this.forward.forward);
		if (Mathf.Abs(this.forward.forward.x) > 0.1f)
		{
			this.headRig.constraints = (RigidbodyConstraints)124;
			Debug.Log("x");
		}
		if (Mathf.Abs(this.forward.forward.y) > 0.1f)
		{
			this.headRig.constraints = (RigidbodyConstraints)122;
			Debug.Log("y");
		}
		if (Mathf.Abs(this.forward.forward.z) > 0.1f)
		{
			this.headRig.constraints = (RigidbodyConstraints)118;
			Debug.Log(this.forward.forward.z);
		}
		this.au = base.GetComponent<AudioSource>();
	}

	// Token: 0x06001045 RID: 4165 RVA: 0x0006A048 File Offset: 0x00068248
	private void Update()
	{
		if (this.isOn)
		{
			this.untilNext -= Time.deltaTime;
		}
		if (this.untilNext < 0f)
		{
			this.shake += Time.deltaTime * 0.2f;
			if (this.shakeCd > 0.01f)
			{
				this.pos = UnityEngine.Random.insideUnitSphere * this.shake;
				this.shakeCd = 0f;
			}
			this.headTrans.localPosition = Vector3.MoveTowards(this.headTrans.localPosition, this.pos, Time.deltaTime * 30f);
			this.shakeCd += Time.deltaTime;
			if (this.shake > 0.4f)
			{
				this.headTrans.localPosition = Vector3.zero;
				this.shake = 0f;
				this.untilNext = 1.5f + UnityEngine.Random.Range(0f, this.extraRandom);
				this.headRig.isKinematic = false;
				this.headRig.AddForce(this.forward.forward * this.force, ForceMode.VelocityChange);
				this.resting = false;
				if (this.au != null)
				{
					this.au.PlayOneShot(this.au.clip);
				}
			}
		}
		if (this.goingBack)
		{
			this.headRig.AddForce(-this.forward.forward * this.backSpeed * Time.unscaledDeltaTime, ForceMode.VelocityChange);
			this.backSpeed += Time.unscaledDeltaTime * 100f;
			if (this.backSpeed > 150f)
			{
				this.headTrans.localPosition = Vector3.MoveTowards(this.headTrans.localPosition, Vector3.zero, Time.unscaledDeltaTime * 100f);
			}
		}
	}

	// Token: 0x06001046 RID: 4166 RVA: 0x0006A23C File Offset: 0x0006843C
	private void FixedUpdate()
	{
		if (this.headTrans.localPosition.x < -this.farCap)
		{
			this.headRig.velocity *= 0.5f;
			this.goingBack = true;
		}
		if (this.headTrans.localPosition.x > -2f && this.goingBack)
		{
			this.goingBack = false;
			this.resting = true;
			this.backSpeed = 100f;
		}
		if (this.resting)
		{
			this.headRig.velocity *= 0.5f;
			this.headTrans.localPosition = Vector3.MoveTowards(this.headTrans.localPosition, Vector3.zero, 0.24000001f);
			if (this.headRig.velocity.magnitude < 1f)
			{
				this.headRig.isKinematic = true;
			}
		}
	}

	// Token: 0x06001047 RID: 4167 RVA: 0x0006A340 File Offset: 0x00068540
	private void sendInfo(float[] info)
	{
		this.baseWait = info[0];
		this.extraRandom = info[1];
		this.farCap = info[2];
		this.force = info[3] * 0.9f;
	}

	// Token: 0x06001048 RID: 4168 RVA: 0x0006A378 File Offset: 0x00068578
	private void Pull()
	{
		this.isOn = !this.isOn;
	}

	// Token: 0x04000D40 RID: 3392
	private float untilNext = 0.5f;

	// Token: 0x04000D41 RID: 3393
	private float shake;

	// Token: 0x04000D42 RID: 3394
	public Transform headTrans;

	// Token: 0x04000D43 RID: 3395
	public Transform forward;

	// Token: 0x04000D44 RID: 3396
	public Rigidbody headRig;

	// Token: 0x04000D45 RID: 3397
	private float shakeCd;

	// Token: 0x04000D46 RID: 3398
	private Vector3 pos;

	// Token: 0x04000D47 RID: 3399
	private bool goingBack;

	// Token: 0x04000D48 RID: 3400
	private bool resting = true;

	// Token: 0x04000D49 RID: 3401
	private float baseWait = 1f;

	// Token: 0x04000D4A RID: 3402
	private float extraRandom = 1f;

	// Token: 0x04000D4B RID: 3403
	private float farCap = 12f;

	// Token: 0x04000D4C RID: 3404
	private float force = 50f;

	// Token: 0x04000D4D RID: 3405
	private bool isOn = true;

	// Token: 0x04000D4E RID: 3406
	private float backSpeed = 100f;

	// Token: 0x04000D4F RID: 3407
	private AudioSource au;
}
