using System;
using UnityEngine;

// Token: 0x02000132 RID: 306
public class TimeScaleTest : MonoBehaviour
{
	// Token: 0x060006A8 RID: 1704 RVA: 0x0002CC54 File Offset: 0x0002AE54
	private void Start()
	{
	}

	// Token: 0x060006A9 RID: 1705 RVA: 0x0002CC58 File Offset: 0x0002AE58
	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.P))
		{
			this.mToggle = !this.mToggle;
			MonoBehaviour.print("toggle time stuff");
			Time.timeScale = ((!this.mToggle) ? 0f : 1f);
		}
	}

	// Token: 0x040004B8 RID: 1208
	private bool mToggle;
}
