using System;
using InControl;
using UnityEngine;

// Token: 0x02000280 RID: 640
public class grapplingHook : AbilityBaseClass
{
	// Token: 0x06000F61 RID: 3937 RVA: 0x00063FBC File Offset: 0x000621BC
	public override string getToolTip()
	{
		return this._toolTip;
	}

	// Token: 0x06000F62 RID: 3938 RVA: 0x00063FC4 File Offset: 0x000621C4
	public override string getToolTipController()
	{
		return this._toolTipController;
	}

	// Token: 0x06000F63 RID: 3939 RVA: 0x00063FCC File Offset: 0x000621CC
	private void OnEnable()
	{
		this.away = false;
		this.stuck = false;
		this.returning = false;
		this.hook.gameObject.SetActive(true);
		this.hook.position = base.transform.position;
		this.line.SetPosition(0, Vector3.zero);
		this.line.SetPosition(1, Vector3.zero);
	}

	// Token: 0x06000F64 RID: 3940 RVA: 0x00064038 File Offset: 0x00062238
	private void FixedUpdate()
	{
		this.inLoop.volume = 0f;
		if (this.sinceHit < 0.1f)
		{
			this.line.SetColors(Color.white, Color.white);
		}
		else
		{
			this.line.SetColors(this.c, this.c);
		}
		if (this.sinceLetGo < 0.3f)
		{
			this.hitBoxRig.velocity *= 0.95f;
		}
		this.sinceHit += Time.deltaTime;
		this.sinceLetGo += Time.deltaTime;
		if (Input.GetKeyUp(KeyCode.Mouse0) || InputManager.ActiveDevice.LeftBumper.WasReleased || (!this.stuck && Vector3.Distance(base.transform.position, this.hook.position) > 33f) || (this.stuck && Vector3.Distance(base.transform.position, this.hook.position) < 7f))
		{
			if (!this.stuck && !this.returning && this.away)
			{
				this.man.cd = 100f;
			}
			if (this.stuck)
			{
				this.sinceLetGo = 0f;
			}
			this.stuck = false;
			this.returning = true;
		}
		if (this.returning)
		{
			this.hook.position += Vector3.Normalize(base.transform.position - this.hook.position) * 3f;
			if (this.away)
			{
				this.inLoop.volume = 0.5f;
			}
			if (Vector3.Distance(base.transform.position, this.hook.position) < 2f)
			{
				this.away = false;
				this.returning = false;
				this.line.SetPosition(0, Vector3.zero);
				this.line.SetPosition(1, Vector3.zero);
				this.hook.gameObject.SetActive(true);
			}
		}
		RaycastHit raycastHit;
		if (Physics.Raycast(base.transform.position, Camera.main.transform.forward, out raycastHit, 37f, this.mask) && this.man.cd > this.rechargeTime)
		{
			if (raycastHit.collider.gameObject.layer != 10 && !raycastHit.collider.isTrigger)
			{
				this.spinner.SetActive(true);
				this.spinner.transform.localScale = Vector3.Lerp(this.spinner.transform.localScale, Vector3.one, Time.deltaTime * 10f);
			}
		}
		else
		{
			this.spinner.transform.localScale = Vector3.Lerp(this.spinner.transform.localScale, Vector3.zero, Time.deltaTime * 10f);
			if (this.spinner.transform.localScale.x < 0.1f)
			{
				this.spinner.SetActive(false);
			}
		}
		if (Physics.Raycast(this.hook.position, this.hook.forward, out raycastHit, 7f, this.mask) && raycastHit.collider != null && !this.returning && !this.stuck && this.away && raycastHit.collider.gameObject.layer != 10 && !raycastHit.collider.isTrigger)
		{
			Camera.main.GetComponent<cameraEffects>().SetShake(0.3f, Vector3.zero);
			this.parts.Play();
			this.sinceHit = 0f;
			this.stuck = true;
			this.stuckTarget = raycastHit.transform;
			this.hook.position = raycastHit.point;
			this.stuckPos = this.hook.position - raycastHit.transform.position;
			this.auSource.PlayOneShot(this.hitSound);
		}
		if (this.stuck)
		{
			if (this.stuckTarget)
			{
				this.hook.position = this.stuckTarget.position + this.stuckPos;
			}
			if (this.away)
			{
				this.hitBoxRig.AddForce(5f * Vector3.Normalize(this.hook.position + Vector3.up * 3f - base.transform.position), ForceMode.VelocityChange);
				this.hitBoxRig.AddForce(0.5f * Vector3.up, ForceMode.VelocityChange);
				this.hitBoxRig.velocity *= 0.91f;
				this.inLoop.volume = 0.5f;
			}
		}
		if (!this.away)
		{
			this.hook.position = base.transform.position;
			this.hook.gameObject.SetActive(false);
		}
		else
		{
			if (!this.stuck && !this.returning)
			{
				this.hook.position += this.hook.forward * 5f;
			}
			this.line.SetPosition(0, base.transform.position - base.transform.forward * 0.2f);
			this.line.SetPosition(1, this.hook.position);
		}
	}

	// Token: 0x06000F65 RID: 3941 RVA: 0x0006465C File Offset: 0x0006285C
	public override void Go()
	{
		this.auSource.PlayOneShot(this.goSound, 0.4f);
		this.hook.gameObject.SetActive(true);
		this.hook.position = base.transform.position;
		this.hook.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
		this.away = true;
		this.stuck = false;
		this.returning = false;
	}

	// Token: 0x06000F66 RID: 3942 RVA: 0x000646DC File Offset: 0x000628DC
	public override void Stop()
	{
	}

	// Token: 0x04000C1B RID: 3099
	public Transform hook;

	// Token: 0x04000C1C RID: 3100
	private float sinceLetGo;

	// Token: 0x04000C1D RID: 3101
	private bool away;

	// Token: 0x04000C1E RID: 3102
	private bool stuck;

	// Token: 0x04000C1F RID: 3103
	private bool returning;

	// Token: 0x04000C20 RID: 3104
	public Rigidbody hitBoxRig;

	// Token: 0x04000C21 RID: 3105
	public LineRenderer line;

	// Token: 0x04000C22 RID: 3106
	private Transform stuckTarget;

	// Token: 0x04000C23 RID: 3107
	private Vector3 stuckPos;

	// Token: 0x04000C24 RID: 3108
	public ParticleSystem parts;

	// Token: 0x04000C25 RID: 3109
	private Color c = new Color(0.1f, 0.1f, 0.1f);

	// Token: 0x04000C26 RID: 3110
	private float sinceHit;

	// Token: 0x04000C27 RID: 3111
	public GameObject spinner;

	// Token: 0x04000C28 RID: 3112
	public abilityManager man;

	// Token: 0x04000C29 RID: 3113
	public AudioSource auSource;

	// Token: 0x04000C2A RID: 3114
	public AudioSource inLoop;

	// Token: 0x04000C2B RID: 3115
	public AudioClip hitSound;

	// Token: 0x04000C2C RID: 3116
	public LayerMask mask;
}
