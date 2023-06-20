using System;
using UnityEngine;

// Token: 0x02000298 RID: 664
public class moveMentAbility : AbilityBaseClass
{
	// Token: 0x06000FE0 RID: 4064 RVA: 0x00066D70 File Offset: 0x00064F70
	public override string getToolTip()
	{
		return this._toolTip;
	}

	// Token: 0x06000FE1 RID: 4065 RVA: 0x00066D78 File Offset: 0x00064F78
	public override string getToolTipController()
	{
		return this._toolTipController;
	}

	// Token: 0x06000FE2 RID: 4066 RVA: 0x00066D80 File Offset: 0x00064F80
	private void Start()
	{
		this.rig = UnityEngine.Object.FindObjectOfType<player>().GetComponent<Rigidbody>();
		this.auSource = base.GetComponent<AudioSource>();
	}

	// Token: 0x06000FE3 RID: 4067 RVA: 0x00066DA0 File Offset: 0x00064FA0
	private void OnEnable()
	{
	}

	// Token: 0x06000FE4 RID: 4068 RVA: 0x00066DA4 File Offset: 0x00064FA4
	private void Update()
	{
		this.timeSinceUse += Time.deltaTime;
	}

	// Token: 0x06000FE5 RID: 4069 RVA: 0x00066DB8 File Offset: 0x00064FB8
	private void FixedUpdate()
	{
		if (this.timeSinceUse < 0.5f && this.movementType == moveMentAbility.type.dash)
		{
			this.rig.velocity *= 0.96f + (1f - Time.timeScale) / 20f;
			Camera.main.GetComponent<cameraEffects>().SetShake(0.2f, Vector3.zero);
		}
		if (this.isGoing)
		{
			if (this.loopingSound)
			{
				this.auSource.volume = Mathf.Lerp(this.auSource.volume, 0.5f, Time.deltaTime * 5f);
			}
			if (this.movementType == moveMentAbility.type.jetpack)
			{
				this.rig.velocity *= 0.99f;
				this.rig.AddForce(Vector3.up * 0.7f, ForceMode.VelocityChange);
				Camera.main.GetComponent<cameraEffects>().SetShake(0.3f, Vector3.zero);
			}
			if (this.movementType == moveMentAbility.type.levitation)
			{
				this.rig.velocity = new Vector3(this.rig.velocity.x * 0.995f, this.rig.velocity.y * 0.75f, this.rig.velocity.z * 0.995f);
			}
		}
		else if (this.loopingSound)
		{
			this.auSource.volume = Mathf.Lerp(this.auSource.volume, 0f, Time.deltaTime * 5f);
		}
	}

	// Token: 0x06000FE6 RID: 4070 RVA: 0x00066F68 File Offset: 0x00065168
	public override void Go()
	{
		if (this.goSound)
		{
			this.auSource.PlayOneShot(this.goSound);
		}
		this.isGoing = true;
		this.timeSinceUse = 0f;
		this.used = true;
		if (this.movementType == moveMentAbility.type.dash)
		{
			this.rig.velocity *= 0.5f;
			this.rig.AddForce(Camera.main.transform.forward * 60f, ForceMode.VelocityChange);
			Camera.main.GetComponent<cameraEffects>().SetShake(0.5f, Vector3.zero);
		}
		if (this.movementType == moveMentAbility.type.doubleJump)
		{
			this.rig.GetComponent<player>().Jump(8f);
			Camera.main.GetComponent<cameraEffects>().SetShake(0.5f, Vector3.zero);
		}
	}

	// Token: 0x06000FE7 RID: 4071 RVA: 0x00067050 File Offset: 0x00065250
	public override void Stop()
	{
		this.isGoing = false;
	}

	// Token: 0x04000CBD RID: 3261
	private Rigidbody rig;

	// Token: 0x04000CBE RID: 3262
	private bool isGoing;

	// Token: 0x04000CBF RID: 3263
	private float timeSinceUse;

	// Token: 0x04000CC0 RID: 3264
	private bool used;

	// Token: 0x04000CC1 RID: 3265
	private AudioSource auSource;

	// Token: 0x04000CC2 RID: 3266
	public bool loopingSound;

	// Token: 0x04000CC3 RID: 3267
	public moveMentAbility.type movementType;

	// Token: 0x02000299 RID: 665
	public new enum type
	{
		// Token: 0x04000CC5 RID: 3269
		doubleJump,
		// Token: 0x04000CC6 RID: 3270
		dash,
		// Token: 0x04000CC7 RID: 3271
		jetpack,
		// Token: 0x04000CC8 RID: 3272
		levitation
	}
}
