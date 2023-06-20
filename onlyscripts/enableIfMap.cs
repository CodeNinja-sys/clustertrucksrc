using System;
using UnityEngine;

// Token: 0x020002F0 RID: 752
public class enableIfMap : MonoBehaviour
{
	// Token: 0x0600119C RID: 4508 RVA: 0x00071B8C File Offset: 0x0006FD8C
	private void Awake()
	{
	}

	// Token: 0x0600119D RID: 4509 RVA: 0x00071B90 File Offset: 0x0006FD90
	private void OnEnable()
	{
		if (info.currentLevel == 4)
		{
			if (!this.hot.enabled)
			{
				this.superTruck.SetActive(true);
			}
			this.hot.enabled = true;
		}
		else if (this.hot.enabled)
		{
			info.playerControlledTime = 1f;
			this.hot.enabled = false;
		}
	}

	// Token: 0x04000EBD RID: 3773
	public superHot hot;

	// Token: 0x04000EBE RID: 3774
	public GameObject superTruck;
}
