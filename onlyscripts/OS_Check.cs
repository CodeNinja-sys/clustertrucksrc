using System;
using UnityEngine;

// Token: 0x0200016D RID: 365
public class OS_Check : MonoBehaviour
{
	// Token: 0x0600082E RID: 2094 RVA: 0x00035C2C File Offset: 0x00033E2C
	private void Start()
	{
		if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer)
		{
			this.SetOSX();
		}
		if (Application.platform == RuntimePlatform.LinuxPlayer)
		{
			this.SetOSX();
		}
	}

	// Token: 0x0600082F RID: 2095 RVA: 0x00035C5C File Offset: 0x00033E5C
	private void SetOSX()
	{
		this.tmpAA1.enabled = false;
		this.tmpAA2.enabled = false;
		this.tmpAA3.enabled = false;
	}

	// Token: 0x04000658 RID: 1624
	public MonoBehaviour tmpAA1;

	// Token: 0x04000659 RID: 1625
	public MonoBehaviour tmpAA2;

	// Token: 0x0400065A RID: 1626
	public MonoBehaviour tmpAA3;
}
