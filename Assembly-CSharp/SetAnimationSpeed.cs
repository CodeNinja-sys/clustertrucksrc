using System;
using UnityEngine;

// Token: 0x020002CA RID: 714
public class SetAnimationSpeed : MonoBehaviour
{
	// Token: 0x060010F3 RID: 4339 RVA: 0x0006E4C4 File Offset: 0x0006C6C4
	private void Start()
	{
		this.anim = base.GetComponent<Animator>();
		this.anim.speed *= this.speed;
		if (this.counter != this.wait)
		{
			this.anim.enabled = false;
		}
	}

	// Token: 0x060010F4 RID: 4340 RVA: 0x0006E514 File Offset: 0x0006C714
	private void Update()
	{
		if (!this.anim.enabled && this.counter < this.wait)
		{
			this.counter += Time.deltaTime;
			if (this.counter >= this.wait)
			{
				this.anim.enabled = true;
			}
		}
	}

	// Token: 0x04000E0F RID: 3599
	private Animator anim;

	// Token: 0x04000E10 RID: 3600
	public float speed = 1f;

	// Token: 0x04000E11 RID: 3601
	public float wait;

	// Token: 0x04000E12 RID: 3602
	private float counter;
}
