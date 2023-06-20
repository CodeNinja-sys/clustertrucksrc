using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000026 RID: 38
[RequireComponent(typeof(Image))]
public class ColorImage : MonoBehaviour
{
	// Token: 0x060000B1 RID: 177 RVA: 0x000060B4 File Offset: 0x000042B4
	private void Awake()
	{
		this.image = base.GetComponent<Image>();
		this.picker.onValueChanged.AddListener(new UnityAction<Color>(this.ColorChanged));
	}

	// Token: 0x060000B2 RID: 178 RVA: 0x000060EC File Offset: 0x000042EC
	private void OnDestroy()
	{
		this.picker.onValueChanged.RemoveListener(new UnityAction<Color>(this.ColorChanged));
	}

	// Token: 0x060000B3 RID: 179 RVA: 0x0000610C File Offset: 0x0000430C
	private void ColorChanged(Color newColor)
	{
		this.image.color = newColor;
	}

	// Token: 0x040000A2 RID: 162
	public ColorPicker picker;

	// Token: 0x040000A3 RID: 163
	private Image image;
}
