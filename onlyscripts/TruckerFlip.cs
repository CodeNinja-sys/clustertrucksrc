using System;
using UnityEngine;

// Token: 0x020002E0 RID: 736
public class TruckerFlip : AbilityBaseClass
{
	// Token: 0x0600114D RID: 4429 RVA: 0x00070628 File Offset: 0x0006E828
	private void Start()
	{
	}

	// Token: 0x0600114E RID: 4430 RVA: 0x0007062C File Offset: 0x0006E82C
	public override string getToolTip()
	{
		return this._toolTip;
	}

	// Token: 0x0600114F RID: 4431 RVA: 0x00070634 File Offset: 0x0006E834
	public override string getToolTipController()
	{
		return this._toolTipController;
	}

	// Token: 0x06001150 RID: 4432 RVA: 0x0007063C File Offset: 0x0006E83C
	private void Update()
	{
	}

	// Token: 0x06001151 RID: 4433 RVA: 0x00070640 File Offset: 0x0006E840
	public override void Go()
	{
		if (this.mPlayer.jumpCd < 0.1f)
		{
			return;
		}
		car component = this.mPlayer.lastCar.GetComponent<car>();
		if (component)
		{
			this.mPlayer.jumpCd = 0f;
			this.mPlayer.hasTouchedGround = false;
			this.mPlayer.rig.velocity = new Vector3(this.mPlayer.rig.velocity.x, 13f, this.mPlayer.rig.velocity.z);
			component.hasFallen = true;
			if (component.secondRig)
			{
				component.secondRig.GetComponent<carCheckDamage>().SetImmunity(true);
				component.secondRig.AddTorque(component.secondRig.transform.forward * 5.5f, ForceMode.VelocityChange);
				if (component.lastGrounded < 0.1f)
				{
					component.secondRig.velocity = new Vector3(component.secondRig.velocity.x, 8f, component.secondRig.velocity.z);
					component.secondRig.AddForce(component.secondRig.transform.forward * (this.mPlayer.rig.velocity.magnitude * 1f + 5f), ForceMode.VelocityChange);
					this.mPlayer.rig.AddForce(component.secondRig.transform.forward * 3f, ForceMode.VelocityChange);
				}
				component.secondRig.mass = 500f;
			}
			if (component.mainRig)
			{
				component.mainRig.AddTorque(component.secondRig.transform.forward * 5.5f, ForceMode.VelocityChange);
				if (component.lastGrounded < 0.1f)
				{
					component.mainRig.velocity = new Vector3(component.mainRig.velocity.x, 8f, component.mainRig.velocity.z);
					component.mainRig.AddForce(component.mainRig.transform.forward * 8f, ForceMode.VelocityChange);
				}
				component.mainRig.mass = 500f;
			}
			base.GetComponent<AudioSource>().PlayOneShot(this.goSound);
		}
	}

	// Token: 0x06001152 RID: 4434 RVA: 0x000708CC File Offset: 0x0006EACC
	public override void Stop()
	{
	}

	// Token: 0x04000E72 RID: 3698
	[SerializeField]
	private player mPlayer;
}
