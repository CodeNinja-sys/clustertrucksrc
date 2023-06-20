using System;
using UnityEngine;

// Token: 0x02000276 RID: 630
public class dustHandler : MonoBehaviour
{
	// Token: 0x06000F2A RID: 3882 RVA: 0x00062E68 File Offset: 0x00061068
	private void Start()
	{
	}

	// Token: 0x06000F2B RID: 3883 RVA: 0x00062E6C File Offset: 0x0006106C
	private void Update()
	{
		if (this.hasTarget)
		{
			this.em.enabled = false;
		}
		RaycastHit raycastHit;
		if (Physics.Raycast(new Ray(base.transform.position, base.transform.forward), out raycastHit, 1f, this.mask))
		{
			string name = raycastHit.transform.GetComponent<Renderer>().sharedMaterial.name;
			if (name == "desert")
			{
				this.em.enabled = true;
				this.hasTarget = true;
				this.em = this.sand.emission;
			}
			if (name == "World3")
			{
				this.em.enabled = true;
				this.em = this.snow.emission;
				this.hasTarget = true;
			}
			if (name == "World2")
			{
				this.em.enabled = true;
				this.em = this.grass.emission;
				this.hasTarget = true;
			}
			if (name == "steampunkZap")
			{
				this.em.enabled = true;
				this.em = this.zap.emission;
				this.hasTarget = true;
			}
		}
		this.em.rate = new ParticleSystem.MinMaxCurve(Mathf.Clamp(this.rig.velocity.magnitude * 0.7f, 0f, 25f));
	}

	// Token: 0x04000BF1 RID: 3057
	public LayerMask mask;

	// Token: 0x04000BF2 RID: 3058
	public ParticleSystem sand;

	// Token: 0x04000BF3 RID: 3059
	public ParticleSystem snow;

	// Token: 0x04000BF4 RID: 3060
	public ParticleSystem grass;

	// Token: 0x04000BF5 RID: 3061
	public ParticleSystem zap;

	// Token: 0x04000BF6 RID: 3062
	public Rigidbody rig;

	// Token: 0x04000BF7 RID: 3063
	private ParticleSystem.EmissionModule em;

	// Token: 0x04000BF8 RID: 3064
	private bool hasTarget = true;
}
