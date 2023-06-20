using System;
using UnityEngine;

// Token: 0x020002DD RID: 733
public class TriggerAnimation : MonoBehaviour
{
	// Token: 0x0600113F RID: 4415 RVA: 0x0007047C File Offset: 0x0006E67C
	private void Start()
	{
	}

	// Token: 0x06001140 RID: 4416 RVA: 0x00070480 File Offset: 0x0006E680
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag != "Player" && this.onlyPlayer)
		{
			return;
		}
		if (this.animName != string.Empty && this.anim != null)
		{
			this.anim.Play(this.animName);
		}
	}

	// Token: 0x04000E6C RID: 3692
	public string animName = string.Empty;

	// Token: 0x04000E6D RID: 3693
	public Animator anim;

	// Token: 0x04000E6E RID: 3694
	public bool onlyPlayer = true;
}
