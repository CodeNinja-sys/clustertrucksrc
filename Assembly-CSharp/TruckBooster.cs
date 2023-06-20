using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002DE RID: 734
public class TruckBooster : AbilityBaseClass
{
	// Token: 0x06001142 RID: 4418 RVA: 0x000704F0 File Offset: 0x0006E6F0
	private void Start()
	{
	}

	// Token: 0x06001143 RID: 4419 RVA: 0x000704F4 File Offset: 0x0006E6F4
	public override string getToolTip()
	{
		return this._toolTip;
	}

	// Token: 0x06001144 RID: 4420 RVA: 0x000704FC File Offset: 0x0006E6FC
	public override string getToolTipController()
	{
		return this._toolTipController;
	}

	// Token: 0x06001145 RID: 4421 RVA: 0x00070504 File Offset: 0x0006E704
	private void Update()
	{
	}

	// Token: 0x06001146 RID: 4422 RVA: 0x00070508 File Offset: 0x0006E708
	private IEnumerator duck()
	{
		this.mPlayer.cantMoveTime = 0.8f;
		this.anim.SetBool("ducking", true);
		this.anim.Play("CameraDuck");
		yield return new WaitForSeconds(0.8f);
		this.anim.SetBool("ducking", false);
		yield break;
	}

	// Token: 0x06001147 RID: 4423 RVA: 0x00070524 File Offset: 0x0006E724
	public override void Go()
	{
		car component = this.mPlayer.lastCar.GetComponent<car>();
		if (component)
		{
			base.StartCoroutine(this.duck());
			component.secondRig.AddForce(component.secondRig.transform.forward * 50f, ForceMode.VelocityChange);
			this.mPlayer.rig.AddForce(component.secondRig.transform.forward * 35f, ForceMode.VelocityChange);
			component.secondRig.mass = 500f;
			component.secondRig.GetComponent<carCheckDamage>().SetImmunity(true);
			base.GetComponent<AudioSource>().PlayOneShot(this.goSound);
		}
	}

	// Token: 0x06001148 RID: 4424 RVA: 0x000705E0 File Offset: 0x0006E7E0
	public override void Stop()
	{
	}

	// Token: 0x04000E6F RID: 3695
	[SerializeField]
	private player mPlayer;

	// Token: 0x04000E70 RID: 3696
	[SerializeField]
	private Animator anim;
}
