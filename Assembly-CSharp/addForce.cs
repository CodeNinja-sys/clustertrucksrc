using System;
using UnityEngine;

// Token: 0x02000264 RID: 612
public class addForce : MonoBehaviour
{
	// Token: 0x06000EDA RID: 3802 RVA: 0x00060344 File Offset: 0x0005E544
	private void Start()
	{
		this.rig = base.GetComponent<Rigidbody>();
		if (this.forward)
		{
			this.rig.AddForce(base.transform.forward * this.force.magnitude, ForceMode.Impulse);
		}
		else
		{
			this.rig.AddForce(this.force, ForceMode.Impulse);
		}
	}

	// Token: 0x06000EDB RID: 3803 RVA: 0x000603A8 File Offset: 0x0005E5A8
	private void Update()
	{
		if (this.wait > 0f)
		{
			this.rig.useGravity = false;
			this.wait -= Time.deltaTime;
		}
		else if (this.forward)
		{
			this.rig.AddForce(base.transform.forward * Time.deltaTime * this.overTimeForce.magnitude, ForceMode.Impulse);
		}
		else
		{
			this.rig.useGravity = true;
			this.rig.AddForce(this.overTimeForce * Time.deltaTime, ForceMode.Impulse);
		}
	}

	// Token: 0x04000B78 RID: 2936
	public Vector3 force;

	// Token: 0x04000B79 RID: 2937
	public Vector3 overTimeForce;

	// Token: 0x04000B7A RID: 2938
	public float wait;

	// Token: 0x04000B7B RID: 2939
	public bool forward;

	// Token: 0x04000B7C RID: 2940
	private Rigidbody rig;
}
