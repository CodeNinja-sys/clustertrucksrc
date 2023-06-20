using System;
using UnityEngine;

// Token: 0x0200026E RID: 622
public class carCheckDamage : MonoBehaviour
{
	// Token: 0x06000F08 RID: 3848 RVA: 0x00061F3C File Offset: 0x0006013C
	private void Start()
	{
		this.myCar = base.transform.root.GetComponent<car>();
		this.rig = base.GetComponent<Rigidbody>();
		this.eMulti = info.explosive;
	}

	// Token: 0x06000F09 RID: 3849 RVA: 0x00061F78 File Offset: 0x00060178
	private void Update()
	{
		this.deltaV = this.rig.velocity - this.lastVel;
		this.deltaAV = this.rig.angularVelocity - this.lastAngVel;
		this.timeFix = Mathf.Clamp(Time.timeScale, 0.7f, 1f);
		this.cantDie += Time.deltaTime;
		if (!this.used)
		{
			if ((this.deltaV.magnitude > 28f / this.eMulti * this.timeFix || this.deltaAV.magnitude > 22f / this.eMulti * this.timeFix) && this.cantDie > 0.3f)
			{
				this.Explode();
			}
		}
		else
		{
			base.gameObject.SetActive(false);
		}
		this.lastVel = this.rig.velocity;
		this.lastAngVel = this.rig.angularVelocity;
	}

	// Token: 0x06000F0A RID: 3850 RVA: 0x00062084 File Offset: 0x00060284
	private void OnCollisionEnter(Collision other)
	{
		if (other.rigidbody != null && other.transform.tag == "player")
		{
			this.cantDie = -0.2f;
		}
	}

	// Token: 0x06000F0B RID: 3851 RVA: 0x000620C8 File Offset: 0x000602C8
	private void OnCollisionStay(Collision other)
	{
		if (other.gameObject.layer == 8)
		{
			this.myCar.lastGrounded = 0f;
		}
	}

	// Token: 0x06000F0C RID: 3852 RVA: 0x000620EC File Offset: 0x000602EC
	public void SetImmunity(bool first)
	{
		this.cantDie = -0.2f;
		if (first)
		{
			this.other.SetImmunity(false);
		}
	}

	// Token: 0x06000F0D RID: 3853 RVA: 0x0006210C File Offset: 0x0006030C
	public void Explode()
	{
		if (!this.used)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(Resources.Load(this.expName), base.transform.position, base.transform.rotation);
			this.used = true;
		}
	}

	// Token: 0x04000BC5 RID: 3013
	private Rigidbody rig;

	// Token: 0x04000BC6 RID: 3014
	[HideInInspector]
	public Vector3 lastVel;

	// Token: 0x04000BC7 RID: 3015
	[HideInInspector]
	public Vector3 lastAngVel;

	// Token: 0x04000BC8 RID: 3016
	public carCheckDamage other;

	// Token: 0x04000BC9 RID: 3017
	[HideInInspector]
	public float timeFix;

	// Token: 0x04000BCA RID: 3018
	[HideInInspector]
	public Vector3 deltaV;

	// Token: 0x04000BCB RID: 3019
	[HideInInspector]
	public Vector3 deltaAV;

	// Token: 0x04000BCC RID: 3020
	public string expName = "explosionSpawner";

	// Token: 0x04000BCD RID: 3021
	private bool used;

	// Token: 0x04000BCE RID: 3022
	private float counter;

	// Token: 0x04000BCF RID: 3023
	private float eMulti = 1f;

	// Token: 0x04000BD0 RID: 3024
	private float cantDie;

	// Token: 0x04000BD1 RID: 3025
	private car myCar;
}
