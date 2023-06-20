using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000020 RID: 32
public class UIWindowBase : MonoBehaviour, IDragHandler, IEventSystemHandler
{
	// Token: 0x060000A3 RID: 163 RVA: 0x00005E88 File Offset: 0x00004088
	private void Start()
	{
		this.m_transform = base.GetComponent<RectTransform>();
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x00005E98 File Offset: 0x00004098
	public void OnDrag(PointerEventData eventData)
	{
		this.m_transform.position += new Vector3(eventData.delta.x, eventData.delta.y);
	}

	// Token: 0x060000A5 RID: 165 RVA: 0x00005EDC File Offset: 0x000040DC
	public void ChangeStrength(float value)
	{
		base.GetComponent<Image>().material.SetFloat("_Size", value);
	}

	// Token: 0x060000A6 RID: 166 RVA: 0x00005EF4 File Offset: 0x000040F4
	public void ChangeVibrancy(float value)
	{
		base.GetComponent<Image>().material.SetFloat("_Vibrancy", value);
	}

	// Token: 0x04000093 RID: 147
	private RectTransform m_transform;
}
