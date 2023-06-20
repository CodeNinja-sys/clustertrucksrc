using System;
using UnityEngine;

// Token: 0x02000205 RID: 517
public class PopUpDialoguePanel : MonoBehaviour
{
	// Token: 0x06000C42 RID: 3138 RVA: 0x0004C940 File Offset: 0x0004AB40
	private void OnEnable()
	{
		this._UIBlocker.Activate();
	}

	// Token: 0x06000C43 RID: 3139 RVA: 0x0004C950 File Offset: 0x0004AB50
	private void OnDisable()
	{
		this._UIBlocker.DeActivate();
	}

	// Token: 0x040008D9 RID: 2265
	public UIblocker _UIBlocker;
}
