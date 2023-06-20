using System;
using UnityEngine;

// Token: 0x0200010C RID: 268
public class CursorLock : MonoBehaviour
{
	// Token: 0x060005D1 RID: 1489 RVA: 0x0002A6D4 File Offset: 0x000288D4
	private void SetCursorState()
	{
		Cursor.lockState = this.wantedMode;
		Cursor.visible = (CursorLockMode.Locked != this.wantedMode);
	}

	// Token: 0x060005D2 RID: 1490 RVA: 0x0002A6F4 File Offset: 0x000288F4
	private void Start()
	{
	}

	// Token: 0x060005D3 RID: 1491 RVA: 0x0002A6F8 File Offset: 0x000288F8
	private void Update()
	{
		if (Cursor.lockState != this.wantedMode)
		{
			this.SetCursorState();
		}
	}

	// Token: 0x04000447 RID: 1095
	private CursorLockMode wantedMode = CursorLockMode.Locked;
}
