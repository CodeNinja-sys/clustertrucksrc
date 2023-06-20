using System;
using UnityEngine;

// Token: 0x020002C5 RID: 709
public class weapon : AbilityBaseClass
{
	// Token: 0x060010E0 RID: 4320 RVA: 0x0006E274 File Offset: 0x0006C474
	public override string getToolTip()
	{
		return this._toolTip;
	}

	// Token: 0x060010E1 RID: 4321 RVA: 0x0006E27C File Offset: 0x0006C47C
	public override string getToolTipController()
	{
		return this._toolTipController;
	}

	// Token: 0x060010E2 RID: 4322 RVA: 0x0006E284 File Offset: 0x0006C484
	private void Start()
	{
	}

	// Token: 0x060010E3 RID: 4323 RVA: 0x0006E288 File Offset: 0x0006C488
	private void Update()
	{
	}

	// Token: 0x060010E4 RID: 4324 RVA: 0x0006E28C File Offset: 0x0006C48C
	public override void Go()
	{
		GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(Resources.Load(this.projectile), base.transform.position, base.transform.rotation);
		this.counter = 0f;
		gameObject.layer = base.transform.root.gameObject.layer;
		if (this.addForce > 0f)
		{
			foreach (Rigidbody rigidbody in gameObject.GetComponentsInChildren<Rigidbody>())
			{
				if (this.useRig != null)
				{
					rigidbody.velocity = this.useRig.velocity;
				}
				rigidbody.AddForce(base.transform.forward * this.addForce, ForceMode.VelocityChange);
			}
		}
		if (this.goSound)
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.goSound);
		}
	}

	// Token: 0x060010E5 RID: 4325 RVA: 0x0006E37C File Offset: 0x0006C57C
	public override void Stop()
	{
	}

	// Token: 0x04000E05 RID: 3589
	public float cd;

	// Token: 0x04000E06 RID: 3590
	public string projectile = string.Empty;

	// Token: 0x04000E07 RID: 3591
	private float counter = 10f;

	// Token: 0x04000E08 RID: 3592
	public float addForce;

	// Token: 0x04000E09 RID: 3593
	public Rigidbody useRig;
}
