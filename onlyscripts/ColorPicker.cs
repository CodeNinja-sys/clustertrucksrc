using System;
using UnityEngine;

// Token: 0x02000028 RID: 40
public class ColorPicker : MonoBehaviour
{
	// Token: 0x17000007 RID: 7
	// (get) Token: 0x060000BD RID: 189 RVA: 0x00006308 File Offset: 0x00004508
	// (set) Token: 0x060000BE RID: 190 RVA: 0x00006328 File Offset: 0x00004528
	public Color CurrentColor
	{
		get
		{
			return new Color(this._red, this._green, this._blue, this._alpha);
		}
		set
		{
			if (this.CurrentColor == value)
			{
				return;
			}
			this._red = value.r;
			this._green = value.g;
			this._blue = value.b;
			this._alpha = value.a;
			this.RGBChanged();
			this.SendChangedEvent();
		}
	}

	// Token: 0x060000BF RID: 191 RVA: 0x00006388 File Offset: 0x00004588
	private void Start()
	{
		this.SendChangedEvent();
	}

	// Token: 0x17000008 RID: 8
	// (get) Token: 0x060000C0 RID: 192 RVA: 0x00006390 File Offset: 0x00004590
	// (set) Token: 0x060000C1 RID: 193 RVA: 0x00006398 File Offset: 0x00004598
	public float H
	{
		get
		{
			return this._hue;
		}
		set
		{
			if (this._hue == value)
			{
				return;
			}
			this._hue = value;
			this.HSVChanged();
			this.SendChangedEvent();
		}
	}

	// Token: 0x17000009 RID: 9
	// (get) Token: 0x060000C2 RID: 194 RVA: 0x000063C8 File Offset: 0x000045C8
	// (set) Token: 0x060000C3 RID: 195 RVA: 0x000063D0 File Offset: 0x000045D0
	public float S
	{
		get
		{
			return this._saturation;
		}
		set
		{
			if (this._saturation == value)
			{
				return;
			}
			this._saturation = value;
			this.HSVChanged();
			this.SendChangedEvent();
		}
	}

	// Token: 0x1700000A RID: 10
	// (get) Token: 0x060000C4 RID: 196 RVA: 0x00006400 File Offset: 0x00004600
	// (set) Token: 0x060000C5 RID: 197 RVA: 0x00006408 File Offset: 0x00004608
	public float V
	{
		get
		{
			return this._brightness;
		}
		set
		{
			if (this._brightness == value)
			{
				return;
			}
			this._brightness = value;
			this.HSVChanged();
			this.SendChangedEvent();
		}
	}

	// Token: 0x1700000B RID: 11
	// (get) Token: 0x060000C6 RID: 198 RVA: 0x00006438 File Offset: 0x00004638
	// (set) Token: 0x060000C7 RID: 199 RVA: 0x00006440 File Offset: 0x00004640
	public float R
	{
		get
		{
			return this._red;
		}
		set
		{
			if (this._red == value)
			{
				return;
			}
			this._red = value;
			this.RGBChanged();
			this.SendChangedEvent();
		}
	}

	// Token: 0x1700000C RID: 12
	// (get) Token: 0x060000C8 RID: 200 RVA: 0x00006470 File Offset: 0x00004670
	// (set) Token: 0x060000C9 RID: 201 RVA: 0x00006478 File Offset: 0x00004678
	public float G
	{
		get
		{
			return this._green;
		}
		set
		{
			if (this._green == value)
			{
				return;
			}
			this._green = value;
			this.RGBChanged();
			this.SendChangedEvent();
		}
	}

	// Token: 0x1700000D RID: 13
	// (get) Token: 0x060000CA RID: 202 RVA: 0x000064A8 File Offset: 0x000046A8
	// (set) Token: 0x060000CB RID: 203 RVA: 0x000064B0 File Offset: 0x000046B0
	public float B
	{
		get
		{
			return this._blue;
		}
		set
		{
			if (this._blue == value)
			{
				return;
			}
			this._blue = value;
			this.RGBChanged();
			this.SendChangedEvent();
		}
	}

	// Token: 0x1700000E RID: 14
	// (get) Token: 0x060000CC RID: 204 RVA: 0x000064E0 File Offset: 0x000046E0
	// (set) Token: 0x060000CD RID: 205 RVA: 0x000064E8 File Offset: 0x000046E8
	private float A
	{
		get
		{
			return this._alpha;
		}
		set
		{
			if (this._alpha == value)
			{
				return;
			}
			this._alpha = value;
			this.SendChangedEvent();
		}
	}

	// Token: 0x060000CE RID: 206 RVA: 0x00006504 File Offset: 0x00004704
	private void RGBChanged()
	{
		HsvColor hsvColor = HSVUtil.ConvertRgbToHsv(this.CurrentColor);
		this._hue = hsvColor.normalizedH;
		this._saturation = hsvColor.normalizedS;
		this._brightness = hsvColor.normalizedV;
	}

	// Token: 0x060000CF RID: 207 RVA: 0x00006544 File Offset: 0x00004744
	private void HSVChanged()
	{
		Color color = HSVUtil.ConvertHsvToRgb((double)(this._hue * 360f), (double)this._saturation, (double)this._brightness, this._alpha);
		this._red = color.r;
		this._green = color.g;
		this._blue = color.b;
	}

	// Token: 0x060000D0 RID: 208 RVA: 0x000065A0 File Offset: 0x000047A0
	private void SendChangedEvent()
	{
		this.onValueChanged.Invoke(this.CurrentColor);
		this.onHSVChanged.Invoke(this._hue, this._saturation, this._brightness);
	}

	// Token: 0x060000D1 RID: 209 RVA: 0x000065DC File Offset: 0x000047DC
	public void AssignColor(ColorValues type, float value)
	{
		switch (type)
		{
		case ColorValues.R:
			this.R = value;
			break;
		case ColorValues.G:
			this.G = value;
			break;
		case ColorValues.B:
			this.B = value;
			break;
		case ColorValues.A:
			this.A = value;
			break;
		case ColorValues.Hue:
			this.H = value;
			break;
		case ColorValues.Saturation:
			this.S = value;
			break;
		case ColorValues.Value:
			this.V = value;
			break;
		}
	}

	// Token: 0x060000D2 RID: 210 RVA: 0x0000666C File Offset: 0x0000486C
	public float GetValue(ColorValues type)
	{
		switch (type)
		{
		case ColorValues.R:
			return this.R;
		case ColorValues.G:
			return this.G;
		case ColorValues.B:
			return this.B;
		case ColorValues.A:
			return this.A;
		case ColorValues.Hue:
			return this.H;
		case ColorValues.Saturation:
			return this.S;
		case ColorValues.Value:
			return this.V;
		default:
			throw new NotImplementedException(string.Empty);
		}
	}

	// Token: 0x040000AB RID: 171
	private float _hue;

	// Token: 0x040000AC RID: 172
	private float _saturation;

	// Token: 0x040000AD RID: 173
	private float _brightness;

	// Token: 0x040000AE RID: 174
	private float _red;

	// Token: 0x040000AF RID: 175
	private float _green;

	// Token: 0x040000B0 RID: 176
	private float _blue;

	// Token: 0x040000B1 RID: 177
	private float _alpha = 1f;

	// Token: 0x040000B2 RID: 178
	public ColorChangedEvent onValueChanged = new ColorChangedEvent();

	// Token: 0x040000B3 RID: 179
	public HSVChangedEvent onHSVChanged = new HSVChangedEvent();
}
