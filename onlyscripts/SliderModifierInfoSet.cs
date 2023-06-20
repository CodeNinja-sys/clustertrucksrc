using System;
using UnityEngine.UI;

// Token: 0x0200021A RID: 538
public class SliderModifierInfoSet : ModifierSetBase
{
	// Token: 0x06000C99 RID: 3225 RVA: 0x0004E19C File Offset: 0x0004C39C
	private void Update()
	{
	}

	// Token: 0x06000C9A RID: 3226 RVA: 0x0004E1A0 File Offset: 0x0004C3A0
	public void SetInfo(getModifierOptions.SliderParameter _param)
	{
		this._slider.minValue = _param.getMinValue();
		this._slider.maxValue = _param.getMaxValue();
		this._slider.value = float.Parse(_param.getValue());
		this._slider.wholeNumbers = _param._clamp;
		this._nameText.text = _param.getName();
	}

	// Token: 0x06000C9B RID: 3227 RVA: 0x0004E208 File Offset: 0x0004C408
	public void OnValueChanged()
	{
		this._valueText.text = this._slider.value.ToString("F1");
		modifierToolLogic.Instance.ParamValuesChanged();
	}

	// Token: 0x06000C9C RID: 3228 RVA: 0x0004E244 File Offset: 0x0004C444
	public override getModifierOptions.Parameter getParameter()
	{
		return new getModifierOptions.SliderParameter(this._nameText.text, this._slider.minValue, this._slider.maxValue, this._slider.value, this._slider.wholeNumbers);
	}

	// Token: 0x04000914 RID: 2324
	public Slider _slider;

	// Token: 0x04000915 RID: 2325
	public Text _nameText;

	// Token: 0x04000916 RID: 2326
	public Text _valueText;
}
