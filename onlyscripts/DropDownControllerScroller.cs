using System;
using InControl;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020001EA RID: 490
public class DropDownControllerScroller : MonoBehaviour, IEventSystemHandler, IUpdateSelectedHandler
{
	// Token: 0x06000B83 RID: 2947 RVA: 0x00047B2C File Offset: 0x00045D2C
	public void Awake()
	{
		this.sr = base.gameObject.GetComponent<ScrollRect>();
	}

	// Token: 0x06000B84 RID: 2948 RVA: 0x00047B40 File Offset: 0x00045D40
	public void OnUpdateSelected(BaseEventData eventData)
	{
		if (InputManager.ActiveDevice.LeftStick.Value.magnitude <= 0f && !InputManager.ActiveDevice.DPad.IsPressed)
		{
			return;
		}
		float height = this.sr.content.rect.height;
		float height2 = this.sr.viewport.rect.height;
		float y = eventData.selectedObject.transform.localPosition.y;
		float num = y + eventData.selectedObject.GetComponent<RectTransform>().rect.height / 2f;
		float num2 = y - eventData.selectedObject.GetComponent<RectTransform>().rect.height / 2f;
		float num3 = (height - height2) * this.sr.normalizedPosition.y - height;
		float num4 = num3 + height2;
		float num5;
		if (num > num4)
		{
			num5 = num - height2 + eventData.selectedObject.GetComponent<RectTransform>().rect.height * DropDownControllerScroller.SCROLL_MARGIN;
		}
		else
		{
			if (num2 >= num3)
			{
				return;
			}
			num5 = num2 - eventData.selectedObject.GetComponent<RectTransform>().rect.height * DropDownControllerScroller.SCROLL_MARGIN;
		}
		float value = (num5 + height) / (height - height2);
		this.sr.normalizedPosition = new Vector2(0f, Mathf.Clamp01(value));
	}

	// Token: 0x04000829 RID: 2089
	private static float SCROLL_MARGIN;

	// Token: 0x0400082A RID: 2090
	private ScrollRect sr;
}
