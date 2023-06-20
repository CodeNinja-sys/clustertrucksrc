using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200002A RID: 42
[RequireComponent(typeof(RawImage))]
[ExecuteInEditMode]
public class ColorSliderImage : MonoBehaviour
{
	// Token: 0x1700000F RID: 15
	// (get) Token: 0x060000DA RID: 218 RVA: 0x00006904 File Offset: 0x00004B04
	private RectTransform rectTransform
	{
		get
		{
			return base.transform as RectTransform;
		}
	}

	// Token: 0x060000DB RID: 219 RVA: 0x00006914 File Offset: 0x00004B14
	private void Awake()
	{
		this.image = base.GetComponent<RawImage>();
		this.RegenerateTexture();
	}

	// Token: 0x060000DC RID: 220 RVA: 0x00006928 File Offset: 0x00004B28
	private void OnEnable()
	{
		if (this.picker != null && Application.isPlaying)
		{
			this.picker.onValueChanged.AddListener(new UnityAction<Color>(this.ColorChanged));
			this.picker.onHSVChanged.AddListener(new UnityAction<float, float, float>(this.HSVChanged));
		}
	}

	// Token: 0x060000DD RID: 221 RVA: 0x00006988 File Offset: 0x00004B88
	private void OnDisable()
	{
		if (this.picker != null)
		{
			this.picker.onValueChanged.RemoveListener(new UnityAction<Color>(this.ColorChanged));
			this.picker.onHSVChanged.RemoveListener(new UnityAction<float, float, float>(this.HSVChanged));
		}
	}

	// Token: 0x060000DE RID: 222 RVA: 0x000069E0 File Offset: 0x00004BE0
	private void OnDestroy()
	{
		if (this.image.texture != null)
		{
			UnityEngine.Object.DestroyImmediate(this.image.texture);
		}
	}

	// Token: 0x060000DF RID: 223 RVA: 0x00006A14 File Offset: 0x00004C14
	private void ColorChanged(Color newColor)
	{
		switch (this.type)
		{
		case ColorValues.R:
		case ColorValues.G:
		case ColorValues.B:
		case ColorValues.Saturation:
		case ColorValues.Value:
			this.RegenerateTexture();
			break;
		}
	}

	// Token: 0x060000E0 RID: 224 RVA: 0x00006A60 File Offset: 0x00004C60
	private void HSVChanged(float hue, float saturation, float value)
	{
		switch (this.type)
		{
		case ColorValues.R:
		case ColorValues.G:
		case ColorValues.B:
		case ColorValues.Saturation:
		case ColorValues.Value:
			this.RegenerateTexture();
			break;
		}
	}

	// Token: 0x060000E1 RID: 225 RVA: 0x00006AAC File Offset: 0x00004CAC
	private void RegenerateTexture()
	{
		Color32 color = (!(this.picker != null)) ? Color.black : this.picker.CurrentColor;
		float num = (!(this.picker != null)) ? 0f : this.picker.H;
		float num2 = (!(this.picker != null)) ? 0f : this.picker.S;
		float num3 = (!(this.picker != null)) ? 0f : this.picker.V;
		bool flag = this.direction == Slider.Direction.BottomToTop || this.direction == Slider.Direction.TopToBottom;
		bool flag2 = this.direction == Slider.Direction.TopToBottom || this.direction == Slider.Direction.RightToLeft;
		int num4;
		switch (this.type)
		{
		case ColorValues.R:
		case ColorValues.G:
		case ColorValues.B:
		case ColorValues.A:
			num4 = 255;
			break;
		case ColorValues.Hue:
			num4 = 360;
			break;
		case ColorValues.Saturation:
		case ColorValues.Value:
			num4 = 100;
			break;
		default:
			throw new NotImplementedException(string.Empty);
		}
		Texture2D texture2D;
		if (flag)
		{
			texture2D = new Texture2D(1, num4);
		}
		else
		{
			texture2D = new Texture2D(num4, 1);
		}
		texture2D.hideFlags = HideFlags.DontSave;
		Color32[] array = new Color32[num4];
		switch (this.type)
		{
		case ColorValues.R:
		{
			byte b = 0;
			while ((int)b < num4)
			{
				array[(!flag2) ? ((int)b) : (num4 - 1 - (int)b)] = new Color32(b, color.g, color.b, byte.MaxValue);
				b += 1;
			}
			break;
		}
		case ColorValues.G:
		{
			byte b2 = 0;
			while ((int)b2 < num4)
			{
				array[(!flag2) ? ((int)b2) : (num4 - 1 - (int)b2)] = new Color32(color.r, b2, color.b, byte.MaxValue);
				b2 += 1;
			}
			break;
		}
		case ColorValues.B:
		{
			byte b3 = 0;
			while ((int)b3 < num4)
			{
				array[(!flag2) ? ((int)b3) : (num4 - 1 - (int)b3)] = new Color32(color.r, color.g, b3, byte.MaxValue);
				b3 += 1;
			}
			break;
		}
		case ColorValues.A:
		{
			byte b4 = 0;
			while ((int)b4 < num4)
			{
				array[(!flag2) ? ((int)b4) : (num4 - 1 - (int)b4)] = new Color32(b4, b4, b4, byte.MaxValue);
				b4 += 1;
			}
			break;
		}
		case ColorValues.Hue:
			for (int i = 0; i < num4; i++)
			{
				array[(!flag2) ? i : (num4 - 1 - i)] = HSVUtil.ConvertHsvToRgb((double)i, 1.0, 1.0, 1f);
			}
			break;
		case ColorValues.Saturation:
			for (int j = 0; j < num4; j++)
			{
				array[(!flag2) ? j : (num4 - 1 - j)] = HSVUtil.ConvertHsvToRgb((double)(num * 360f), (double)((float)j / (float)num4), (double)num3, 1f);
			}
			break;
		case ColorValues.Value:
			for (int k = 0; k < num4; k++)
			{
				array[(!flag2) ? k : (num4 - 1 - k)] = HSVUtil.ConvertHsvToRgb((double)(num * 360f), (double)num2, (double)((float)k / (float)num4), 1f);
			}
			break;
		default:
			throw new NotImplementedException(string.Empty);
		}
		texture2D.SetPixels32(array);
		texture2D.Apply();
		if (this.image.texture != null)
		{
			UnityEngine.Object.DestroyImmediate(this.image.texture);
		}
		this.image.texture = texture2D;
		switch (this.direction)
		{
		case Slider.Direction.LeftToRight:
		case Slider.Direction.RightToLeft:
			this.image.uvRect = new Rect(0f, 0f, 1f, 2f);
			break;
		case Slider.Direction.BottomToTop:
		case Slider.Direction.TopToBottom:
			this.image.uvRect = new Rect(0f, 0f, 2f, 1f);
			break;
		}
	}

	// Token: 0x040000B8 RID: 184
	public ColorPicker picker;

	// Token: 0x040000B9 RID: 185
	public ColorValues type;

	// Token: 0x040000BA RID: 186
	public Slider.Direction direction;

	// Token: 0x040000BB RID: 187
	private RawImage image;
}
