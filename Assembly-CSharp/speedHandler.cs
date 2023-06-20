using System;
using UnityEngine;

// Token: 0x020002B8 RID: 696
public class speedHandler : MonoBehaviour
{
	// Token: 0x06001086 RID: 4230 RVA: 0x0006B950 File Offset: 0x00069B50
	private void Start()
	{
	}

	// Token: 0x06001087 RID: 4231 RVA: 0x0006B954 File Offset: 0x00069B54
	private void Update()
	{
		if (this.rig.velocity != Vector3.zero)
		{
			base.transform.rotation = Quaternion.Lerp(base.transform.rotation, Quaternion.LookRotation(this.rig.velocity), Time.deltaTime * 2f);
		}
		base.transform.position = this.rig.position;
		this.parts.emissionRate = Mathf.Lerp(this.parts.emissionRate, Mathf.Clamp((this.rig.velocity.magnitude - 27f) * 8f, 0f, 100f), Time.deltaTime * 3f);
		this.parts.startSpeed = Mathf.Lerp(this.parts.startSpeed, this.rig.velocity.magnitude * 1f + 40f, Time.deltaTime * 3f);
	}

	// Token: 0x04000D9F RID: 3487
	public Rigidbody rig;

	// Token: 0x04000DA0 RID: 3488
	public ParticleSystem parts;
}
