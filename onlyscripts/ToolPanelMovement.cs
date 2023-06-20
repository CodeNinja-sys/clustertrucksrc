using System;
using UnityEngine;

// Token: 0x0200021B RID: 539
public class ToolPanelMovement : MonoBehaviour
{
	// Token: 0x06000C9E RID: 3230 RVA: 0x0004E2B0 File Offset: 0x0004C4B0
	private void Awake()
	{
		this.startPosition = this.toolPanelObject.position;
	}

	// Token: 0x06000C9F RID: 3231 RVA: 0x0004E2C4 File Offset: 0x0004C4C4
	private void Update()
	{
		this.toolPanelObject.position = Vector3.Lerp(this.toolPanelObject.position, (!levelEditorManager.Instance().IsBusy) ? this.startPosition : this.hidePosition.position, Time.deltaTime * this.speed);
	}

	// Token: 0x04000917 RID: 2327
	public float speed = 2f;

	// Token: 0x04000918 RID: 2328
	public Transform toolPanelObject;

	// Token: 0x04000919 RID: 2329
	public Transform hidePosition;

	// Token: 0x0400091A RID: 2330
	private Vector3 startPosition = Vector3.zero;
}
