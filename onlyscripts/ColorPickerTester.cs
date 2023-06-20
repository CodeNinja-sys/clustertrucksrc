using System;
using UnityEngine;

// Token: 0x02000024 RID: 36
public class ColorPickerTester : MonoBehaviour
{
	// Token: 0x060000AA RID: 170 RVA: 0x00005F24 File Offset: 0x00004124
	private void Start()
	{
		this.picker.onValueChanged.AddListener(delegate(Color color)
		{
			this.renderer.material.color = color;
		});
		this.renderer.material.color = this.picker.CurrentColor;
	}

	// Token: 0x060000AB RID: 171 RVA: 0x00005F68 File Offset: 0x00004168
	private void Update()
	{
	}

	// Token: 0x0400009C RID: 156
	public Renderer renderer;

	// Token: 0x0400009D RID: 157
	public ColorPicker picker;
}
