using System;
using UnityEngine;

// Token: 0x0200027D RID: 637
public class followTransform : MonoBehaviour
{
	// Token: 0x06000F58 RID: 3928 RVA: 0x00063E8C File Offset: 0x0006208C
	private void Start()
	{
	}

	// Token: 0x06000F59 RID: 3929 RVA: 0x00063E90 File Offset: 0x00062090
	private void Update()
	{
		if (this.to != null)
		{
			base.transform.position = this.to.position;
			if (this.rot)
			{
				base.transform.rotation = this.to.rotation;
			}
		}
	}

	// Token: 0x04000C16 RID: 3094
	public Transform to;

	// Token: 0x04000C17 RID: 3095
	public bool rot;
}
