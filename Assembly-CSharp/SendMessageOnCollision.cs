using System;
using UnityEngine;

// Token: 0x020002C9 RID: 713
public class SendMessageOnCollision : MonoBehaviour
{
	// Token: 0x060010F0 RID: 4336 RVA: 0x0006E498 File Offset: 0x0006C698
	private void Start()
	{
	}

	// Token: 0x060010F1 RID: 4337 RVA: 0x0006E49C File Offset: 0x0006C69C
	private void OnCollisionEnter(Collision other)
	{
		this.target.SendMessage(this.message, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x04000E0D RID: 3597
	public Transform target;

	// Token: 0x04000E0E RID: 3598
	public string message = string.Empty;
}
