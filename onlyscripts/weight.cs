using System;
using UnityEngine;

// Token: 0x02000312 RID: 786
public class weight : MonoBehaviour
{
	// Token: 0x06001257 RID: 4695 RVA: 0x0007555C File Offset: 0x0007375C
	private void OnEnable()
	{
		if (!this.heavy)
		{
			info.drag = 0.995f;
			info.speedMultiplier = 0.85f;
		}
		else
		{
			info.drag = 0.995f;
			info.speedMultiplier = 0.7f;
		}
	}

	// Token: 0x06001258 RID: 4696 RVA: 0x000755A4 File Offset: 0x000737A4
	private void Update()
	{
	}

	// Token: 0x04000FAC RID: 4012
	public bool heavy = true;
}
