using System;
using UnityEngine;

// Token: 0x020002CE RID: 718
public class SetRandomPitch : MonoBehaviour
{
	// Token: 0x06001100 RID: 4352 RVA: 0x0006E888 File Offset: 0x0006CA88
	private void Start()
	{
		base.GetComponent<AudioSource>().pitch *= UnityEngine.Random.Range(this.min, this.max);
		if (this.animate)
		{
			this.anim = base.GetComponent<Animator>();
			this.anim.speed *= UnityEngine.Random.Range(this.min, this.max);
		}
	}

	// Token: 0x04000E19 RID: 3609
	public float min = 0.9f;

	// Token: 0x04000E1A RID: 3610
	public float max = 1.1f;

	// Token: 0x04000E1B RID: 3611
	public bool animate;

	// Token: 0x04000E1C RID: 3612
	private Animator anim;
}
