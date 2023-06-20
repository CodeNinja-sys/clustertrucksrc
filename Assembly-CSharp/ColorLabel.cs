using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000027 RID: 39
[RequireComponent(typeof(Text))]
public class ColorLabel : MonoBehaviour
{
	// Token: 0x060000B5 RID: 181 RVA: 0x0000613C File Offset: 0x0000433C
	private void Awake()
	{
		this.label = base.GetComponent<Text>();
	}

	// Token: 0x060000B6 RID: 182 RVA: 0x0000614C File Offset: 0x0000434C
	private void OnEnable()
	{
		if (Application.isPlaying && this.picker != null)
		{
			this.picker.onValueChanged.AddListener(new UnityAction<Color>(this.ColorChanged));
			this.picker.onHSVChanged.AddListener(new UnityAction<float, float, float>(this.HSVChanged));
		}
	}

	// Token: 0x060000B7 RID: 183 RVA: 0x000061AC File Offset: 0x000043AC
	private void OnDestroy()
	{
		if (this.picker != null)
		{
			this.picker.onValueChanged.RemoveListener(new UnityAction<Color>(this.ColorChanged));
			this.picker.onHSVChanged.RemoveListener(new UnityAction<float, float, float>(this.HSVChanged));
		}
	}

	// Token: 0x060000B8 RID: 184 RVA: 0x00006204 File Offset: 0x00004404
	private void ColorChanged(Color color)
	{
		this.UpdateValue();
	}

	// Token: 0x060000B9 RID: 185 RVA: 0x0000620C File Offset: 0x0000440C
	private void HSVChanged(float hue, float sateration, float value)
	{
		this.UpdateValue();
	}

	// Token: 0x060000BA RID: 186 RVA: 0x00006214 File Offset: 0x00004414
	private void UpdateValue()
	{
		if (this.picker == null)
		{
			this.label.text = this.prefix + "-";
		}
		else
		{
			float value = this.minValue + this.picker.GetValue(this.type) * (this.maxValue - this.minValue);
			this.label.text = this.prefix + this.ConvertToDisplayString(value);
		}
	}

	// Token: 0x060000BB RID: 187 RVA: 0x00006298 File Offset: 0x00004498
	private string ConvertToDisplayString(float value)
	{
		if (this.precision > 0)
		{
			return value.ToString("f " + this.precision);
		}
		return Mathf.FloorToInt(value).ToString();
	}

	// Token: 0x040000A4 RID: 164
	public ColorPicker picker;

	// Token: 0x040000A5 RID: 165
	public ColorValues type;

	// Token: 0x040000A6 RID: 166
	public string prefix = "R: ";

	// Token: 0x040000A7 RID: 167
	public float minValue;

	// Token: 0x040000A8 RID: 168
	public float maxValue = 255f;

	// Token: 0x040000A9 RID: 169
	public int precision;

	// Token: 0x040000AA RID: 170
	private Text label;
}
