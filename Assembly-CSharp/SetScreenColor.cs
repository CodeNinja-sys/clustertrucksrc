using System;
using UnityEngine;

// Token: 0x020002CF RID: 719
public class SetScreenColor : MonoBehaviour
{
	// Token: 0x06001102 RID: 4354 RVA: 0x0006E8FC File Offset: 0x0006CAFC
	private void Start()
	{
	}

	// Token: 0x06001103 RID: 4355 RVA: 0x0006E900 File Offset: 0x0006CB00
	private void OnEnable()
	{
		this.orange.SetActive(false);
	}

	// Token: 0x06001104 RID: 4356 RVA: 0x0006E910 File Offset: 0x0006CB10
	public void SetColor(int colorID)
	{
		if (colorID == 0)
		{
			this.orange.SetActive(true);
		}
	}

	// Token: 0x04000E1D RID: 3613
	public GameObject orange;
}
