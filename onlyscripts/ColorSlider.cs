using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000029 RID: 41
[RequireComponent(typeof(Slider))]
public class ColorSlider : MonoBehaviour
{
	// Token: 0x060000D4 RID: 212 RVA: 0x000066F0 File Offset: 0x000048F0
	private void Awake()
	{
		this.slider = base.GetComponent<Slider>();
		this.hsvpicker.onValueChanged.AddListener(new UnityAction<Color>(this.ColorChanged));
		this.hsvpicker.onHSVChanged.AddListener(new UnityAction<float, float, float>(this.HSVChanged));
		this.slider.onValueChanged.AddListener(new UnityAction<float>(this.SliderChanged));
	}

	// Token: 0x060000D5 RID: 213 RVA: 0x00006760 File Offset: 0x00004960
	private void OnDestroy()
	{
		this.hsvpicker.onValueChanged.RemoveListener(new UnityAction<Color>(this.ColorChanged));
		this.hsvpicker.onHSVChanged.RemoveListener(new UnityAction<float, float, float>(this.HSVChanged));
		this.slider.onValueChanged.RemoveListener(new UnityAction<float>(this.SliderChanged));
	}

	// Token: 0x060000D6 RID: 214 RVA: 0x000067C4 File Offset: 0x000049C4
	private void ColorChanged(Color newColor)
	{
		this.listen = false;
		switch (this.type)
		{
		case ColorValues.R:
			this.slider.normalizedValue = newColor.r;
			break;
		case ColorValues.G:
			this.slider.normalizedValue = newColor.g;
			break;
		case ColorValues.B:
			this.slider.normalizedValue = newColor.b;
			break;
		case ColorValues.A:
			this.slider.normalizedValue = newColor.a;
			break;
		}
	}

	// Token: 0x060000D7 RID: 215 RVA: 0x0000685C File Offset: 0x00004A5C
	private void HSVChanged(float hue, float saturation, float value)
	{
		this.listen = false;
		switch (this.type)
		{
		case ColorValues.Hue:
			this.slider.normalizedValue = hue;
			break;
		case ColorValues.Saturation:
			this.slider.normalizedValue = saturation;
			break;
		case ColorValues.Value:
			this.slider.normalizedValue = value;
			break;
		}
	}

	// Token: 0x060000D8 RID: 216 RVA: 0x000068C8 File Offset: 0x00004AC8
	private void SliderChanged(float newValue)
	{
		if (this.listen)
		{
			newValue = this.slider.normalizedValue;
			this.hsvpicker.AssignColor(this.type, newValue);
		}
		this.listen = true;
	}

	// Token: 0x040000B4 RID: 180
	public ColorPicker hsvpicker;

	// Token: 0x040000B5 RID: 181
	public ColorValues type;

	// Token: 0x040000B6 RID: 182
	private Slider slider;

	// Token: 0x040000B7 RID: 183
	private bool listen = true;
}
