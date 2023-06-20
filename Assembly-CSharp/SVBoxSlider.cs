using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200002C RID: 44
[RequireComponent(typeof(BoxSlider), typeof(RawImage))]
[ExecuteInEditMode]
public class SVBoxSlider : MonoBehaviour
{
	// Token: 0x17000010 RID: 16
	// (get) Token: 0x060000EA RID: 234 RVA: 0x000073A0 File Offset: 0x000055A0
	public RectTransform rectTransform
	{
		get
		{
			return base.transform as RectTransform;
		}
	}

	// Token: 0x060000EB RID: 235 RVA: 0x000073B0 File Offset: 0x000055B0
	private void Awake()
	{
		this.slider = base.GetComponent<BoxSlider>();
		this.image = base.GetComponent<RawImage>();
		this.RegenerateSVTexture();
	}

	// Token: 0x060000EC RID: 236 RVA: 0x000073D0 File Offset: 0x000055D0
	private void OnEnable()
	{
		if (Application.isPlaying && this.picker != null)
		{
			this.slider.onValueChanged.AddListener(new UnityAction<float, float>(this.SliderChanged));
			this.picker.onHSVChanged.AddListener(new UnityAction<float, float, float>(this.HSVChanged));
		}
	}

	// Token: 0x060000ED RID: 237 RVA: 0x00007430 File Offset: 0x00005630
	private void OnDisable()
	{
		if (this.picker != null)
		{
			this.slider.onValueChanged.RemoveListener(new UnityAction<float, float>(this.SliderChanged));
			this.picker.onHSVChanged.RemoveListener(new UnityAction<float, float, float>(this.HSVChanged));
		}
	}

	// Token: 0x060000EE RID: 238 RVA: 0x00007488 File Offset: 0x00005688
	private void OnDestroy()
	{
		if (this.image.texture != null)
		{
			UnityEngine.Object.DestroyImmediate(this.image.texture);
		}
	}

	// Token: 0x060000EF RID: 239 RVA: 0x000074BC File Offset: 0x000056BC
	private void SliderChanged(float saturation, float value)
	{
		if (this.listen)
		{
			this.picker.AssignColor(ColorValues.Saturation, saturation);
			this.picker.AssignColor(ColorValues.Value, value);
		}
		this.listen = true;
	}

	// Token: 0x060000F0 RID: 240 RVA: 0x000074F8 File Offset: 0x000056F8
	private void HSVChanged(float h, float s, float v)
	{
		if (this.lastH != h)
		{
			this.lastH = h;
			this.RegenerateSVTexture();
		}
		if (s != this.slider.normalizedValue)
		{
			this.listen = false;
			this.slider.normalizedValue = s;
		}
		if (v != this.slider.normalizedValueY)
		{
			this.listen = false;
			this.slider.normalizedValueY = v;
		}
	}

	// Token: 0x060000F1 RID: 241 RVA: 0x00007568 File Offset: 0x00005768
	private void RegenerateSVTexture()
	{
		double h = (double)((!(this.picker != null)) ? 0f : (this.picker.H * 360f));
		if (this.image.texture != null)
		{
			UnityEngine.Object.DestroyImmediate(this.image.texture);
		}
		Texture2D texture2D = new Texture2D(100, 100);
		texture2D.hideFlags = HideFlags.DontSave;
		for (int i = 0; i < 100; i++)
		{
			Color32[] array = new Color32[100];
			for (int j = 0; j < 100; j++)
			{
				array[j] = HSVUtil.ConvertHsvToRgb(h, (double)((float)i / 100f), (double)((float)j / 100f), 1f);
			}
			texture2D.SetPixels32(i, 0, 1, 100, array);
		}
		texture2D.Apply();
		this.image.texture = texture2D;
	}

	// Token: 0x040000C0 RID: 192
	public ColorPicker picker;

	// Token: 0x040000C1 RID: 193
	private BoxSlider slider;

	// Token: 0x040000C2 RID: 194
	private RawImage image;

	// Token: 0x040000C3 RID: 195
	private float lastH = -1f;

	// Token: 0x040000C4 RID: 196
	private bool listen = true;
}
