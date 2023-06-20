using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000221 RID: 545
public class colorModifierSet : ModifierSetBase
{
	// Token: 0x06000CC2 RID: 3266 RVA: 0x0004EE34 File Offset: 0x0004D034
	private void Start()
	{
		this.ColorPicker.onValueChanged.AddListener(new UnityAction<Color>(this.OnColorPickerChanged));
	}

	// Token: 0x06000CC3 RID: 3267 RVA: 0x0004EE54 File Offset: 0x0004D054
	private void OnDisable()
	{
		this.ColorPicker.gameObject.SetActive(false);
	}

	// Token: 0x06000CC4 RID: 3268 RVA: 0x0004EE68 File Offset: 0x0004D068
	private void Update()
	{
	}

	// Token: 0x06000CC5 RID: 3269 RVA: 0x0004EE6C File Offset: 0x0004D06C
	public void SetInfo(getModifierOptions.ColorParameter _param)
	{
		this._parameter = _param;
		this.NameText.text = this._parameter.getName();
		this._colorButton.GetComponent<Image>().color = extensionMethods.HexToColor(_param.getValue());
	}

	// Token: 0x06000CC6 RID: 3270 RVA: 0x0004EEB4 File Offset: 0x0004D0B4
	public override getModifierOptions.Parameter getParameter()
	{
		return new getModifierOptions.ColorParameter(this.NameText.text, extensionMethods.ColorToHex(this._colorButton.GetComponent<Image>().color));
	}

	// Token: 0x06000CC7 RID: 3271 RVA: 0x0004EEEC File Offset: 0x0004D0EC
	public void Onsubmit(BaseEventData b)
	{
		if (colorModifierSet._currentImage && b.selectedObject.GetComponent<Image>() == colorModifierSet._currentImage)
		{
			this.ColorPicker.gameObject.SetActive(!this.ColorPicker.gameObject.activeInHierarchy);
			return;
		}
		if (!this.ColorPicker.gameObject.activeInHierarchy)
		{
			this.ColorPicker.gameObject.SetActive(true);
		}
		colorModifierSet._currentImage = b.selectedObject.GetComponent<Image>();
		Color color = colorModifierSet._currentImage.color;
		this.ColorPicker.AssignColor(ColorValues.R, color.r);
		this.ColorPicker.AssignColor(ColorValues.G, color.g);
		this.ColorPicker.AssignColor(ColorValues.B, color.b);
		this.ColorPicker.AssignColor(ColorValues.A, 1f);
	}

	// Token: 0x06000CC8 RID: 3272 RVA: 0x0004EFD4 File Offset: 0x0004D1D4
	public void OnColorPickerChanged(Color color)
	{
		if (colorModifierSet._currentImage != null)
		{
			colorModifierSet._currentImage.color = color;
			modifierToolLogic.Instance.ParamValuesChanged();
		}
	}

	// Token: 0x04000956 RID: 2390
	public ColorPicker ColorPicker;

	// Token: 0x04000957 RID: 2391
	public Text NameText;

	// Token: 0x04000958 RID: 2392
	public Button _colorButton;

	// Token: 0x04000959 RID: 2393
	private getModifierOptions.ColorParameter _parameter;

	// Token: 0x0400095A RID: 2394
	private static Image _currentImage;
}
