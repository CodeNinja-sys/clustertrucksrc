using System;
using UnityEngine;

// Token: 0x0200028A RID: 650
public class lerp : MonoBehaviour
{
	// Token: 0x06000FA1 RID: 4001 RVA: 0x000659A8 File Offset: 0x00063BA8
	private void Start()
	{
	}

	// Token: 0x06000FA2 RID: 4002 RVA: 0x000659AC File Offset: 0x00063BAC
	private void FixedUpdate()
	{
		if (this.target != null)
		{
			base.transform.position = Vector3.Lerp(base.transform.position, this.target.position, this.speed * Time.deltaTime);
		}
	}

	// Token: 0x04000C82 RID: 3202
	public float speed;

	// Token: 0x04000C83 RID: 3203
	public Transform target;
}
