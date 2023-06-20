using System;
using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200002B RID: 43
[RequireComponent(typeof(InputField))]
public class HexColorField : MonoBehaviour
{
	// Token: 0x060000E3 RID: 227 RVA: 0x00006F90 File Offset: 0x00005190
	private void Awake()
	{
		this.hexInputField = base.GetComponent<InputField>();
		this.hexInputField.onEndEdit.AddListener(new UnityAction<string>(this.UpdateColor));
		this.hsvpicker.onValueChanged.AddListener(new UnityAction<Color>(this.UpdateHex));
	}

	// Token: 0x060000E4 RID: 228 RVA: 0x00006FE4 File Offset: 0x000051E4
	private void OnDestroy()
	{
		this.hexInputField.onValueChanged.RemoveListener(new UnityAction<string>(this.UpdateColor));
		this.hsvpicker.onValueChanged.RemoveListener(new UnityAction<Color>(this.UpdateHex));
	}

	// Token: 0x060000E5 RID: 229 RVA: 0x0000702C File Offset: 0x0000522C
	private void UpdateHex(Color newColor)
	{
		this.hexInputField.text = this.ColorToHex(newColor);
	}

	// Token: 0x060000E6 RID: 230 RVA: 0x00007048 File Offset: 0x00005248
	private void UpdateColor(string newHex)
	{
		Color32 c;
		if (HexColorField.HexToColor(newHex, out c))
		{
			this.hsvpicker.CurrentColor = c;
		}
		else
		{
			Debug.Log("hex value is in the wrong format, valid formats are: #RGB, #RGBA, #RRGGBB and #RRGGBBAA (# is optional)");
		}
	}

	// Token: 0x060000E7 RID: 231 RVA: 0x00007084 File Offset: 0x00005284
	private string ColorToHex(Color32 color)
	{
		if (this.displayAlpha)
		{
			return string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", new object[]
			{
				color.r,
				color.g,
				color.b,
				color.a
			});
		}
		return string.Format("#{0:X2}{1:X2}{2:X2}", color.r, color.g, color.b);
	}

	// Token: 0x060000E8 RID: 232 RVA: 0x00007118 File Offset: 0x00005318
	public static bool HexToColor(string hex, out Color32 color)
	{
		if (Regex.IsMatch(hex, "^#?(?:[0-9a-fA-F]{3,4}){1,2}$"))
		{
			int num = (!hex.StartsWith("#")) ? 0 : 1;
			if (hex.Length == num + 8)
			{
				color = new Color32(byte.Parse(hex.Substring(num, 2), NumberStyles.AllowHexSpecifier), byte.Parse(hex.Substring(num + 2, 2), NumberStyles.AllowHexSpecifier), byte.Parse(hex.Substring(num + 4, 2), NumberStyles.AllowHexSpecifier), byte.Parse(hex.Substring(num + 6, 2), NumberStyles.AllowHexSpecifier));
			}
			else if (hex.Length == num + 6)
			{
				color = new Color32(byte.Parse(hex.Substring(num, 2), NumberStyles.AllowHexSpecifier), byte.Parse(hex.Substring(num + 2, 2), NumberStyles.AllowHexSpecifier), byte.Parse(hex.Substring(num + 4, 2), NumberStyles.AllowHexSpecifier), byte.MaxValue);
			}
			else if (hex.Length == num + 4)
			{
				color = new Color32(byte.Parse(string.Empty + hex[num] + hex[num], NumberStyles.AllowHexSpecifier), byte.Parse(string.Empty + hex[num + 1] + hex[num + 1], NumberStyles.AllowHexSpecifier), byte.Parse(string.Empty + hex[num + 2] + hex[num + 2], NumberStyles.AllowHexSpecifier), byte.Parse(string.Empty + hex[num + 3] + hex[num + 3], NumberStyles.AllowHexSpecifier));
			}
			else
			{
				color = new Color32(byte.Parse(string.Empty + hex[num] + hex[num], NumberStyles.AllowHexSpecifier), byte.Parse(string.Empty + hex[num + 1] + hex[num + 1], NumberStyles.AllowHexSpecifier), byte.Parse(string.Empty + hex[num + 2] + hex[num + 2], NumberStyles.AllowHexSpecifier), byte.MaxValue);
			}
			return true;
		}
		color = default(Color32);
		return false;
	}

	// Token: 0x040000BC RID: 188
	private const string hexRegex = "^#?(?:[0-9a-fA-F]{3,4}){1,2}$";

	// Token: 0x040000BD RID: 189
	public ColorPicker hsvpicker;

	// Token: 0x040000BE RID: 190
	public bool displayAlpha;

	// Token: 0x040000BF RID: 191
	private InputField hexInputField;
}
