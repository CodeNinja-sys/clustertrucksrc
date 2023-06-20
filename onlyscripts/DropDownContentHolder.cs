using System;
using UnityEngine;

// Token: 0x0200001C RID: 28
public class DropDownContentHolder : MonoBehaviour
{
	// Token: 0x06000097 RID: 151 RVA: 0x00005D00 File Offset: 0x00003F00
	private void OnEnable()
	{
		float dropDown_contentPanel_Yposition = UnityEngine.Object.FindObjectOfType<leaderboardsManager>().DropDown_contentPanel_Yposition;
		if (this.rect == null)
		{
			this.rect = base.GetComponent<RectTransform>();
		}
		this.rect.localPosition = this.rect.localPosition.setYValue(dropDown_contentPanel_Yposition);
	}

	// Token: 0x0400008D RID: 141
	private RectTransform rect;
}
