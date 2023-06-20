using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000209 RID: 521
public class BehaviourParamSlider : BehaviourParamCellSetBase
{
	// Token: 0x06000C4E RID: 3150 RVA: 0x0004CA04 File Offset: 0x0004AC04
	public override void Init(string description, string[] Parameters)
	{
		this._descriptionText.text = description;
		this._slider.maxValue = float.Parse(Parameters[2]);
		this._slider.minValue = float.Parse(Parameters[1]);
		this._slider.value = float.Parse(Parameters[0]);
		this._slider.wholeNumbers = (bool.TrueString == Parameters[3]);
		this._valueText.text = this._slider.value.ToString("F2");
		Debug.Log("Initializing: Slider: " + this._descriptionText.text, this);
		Debug.Log("Value: " + Parameters[0], this);
		Debug.Log("Min: " + Parameters[1], this);
		Debug.Log("Max: " + float.Parse(Parameters[2]), this);
		Debug.Log("Clamp: " + Parameters[3], this);
	}

	// Token: 0x06000C4F RID: 3151 RVA: 0x0004CB04 File Offset: 0x0004AD04
	public override string getValue()
	{
		return this._slider.value.ToString("F2");
	}

	// Token: 0x06000C50 RID: 3152 RVA: 0x0004CB2C File Offset: 0x0004AD2C
	public void OnValueChanged()
	{
		this._valueText.text = this._slider.value.ToString("F2");
	}

	// Token: 0x040008DE RID: 2270
	[SerializeField]
	private Text _descriptionText;

	// Token: 0x040008DF RID: 2271
	[SerializeField]
	private Text _valueText;

	// Token: 0x040008E0 RID: 2272
	[SerializeField]
	private Slider _slider;
}
