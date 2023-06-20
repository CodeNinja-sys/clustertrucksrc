using System;
using System.Collections.Generic;
using UnityEngine.UI;

// Token: 0x02000224 RID: 548
public class dropDownModifierSet : ModifierSetBase
{
	// Token: 0x06000CD7 RID: 3287 RVA: 0x0004F270 File Offset: 0x0004D470
	private void Update()
	{
	}

	// Token: 0x06000CD8 RID: 3288 RVA: 0x0004F274 File Offset: 0x0004D474
	public void SetInfo(getModifierOptions.DropdownParameter _param)
	{
		this._nameText.text = _param.getName();
		this._Dropdown.AddOptions(new List<string>(_param.getOptions()));
		this._Dropdown.value = int.Parse(_param.getValue());
		this._parameter = _param;
	}

	// Token: 0x06000CD9 RID: 3289 RVA: 0x0004F2C8 File Offset: 0x0004D4C8
	public void OnValueChanged()
	{
		this._valueText.text = this._Dropdown.value.ToString();
	}

	// Token: 0x06000CDA RID: 3290 RVA: 0x0004F2F4 File Offset: 0x0004D4F4
	public override getModifierOptions.Parameter getParameter()
	{
		return new getModifierOptions.DropdownParameter(this._nameText.text, this._parameter.getOptions(), this._Dropdown.value);
	}

	// Token: 0x04000964 RID: 2404
	private getModifierOptions.DropdownParameter _parameter;

	// Token: 0x04000965 RID: 2405
	public Dropdown _Dropdown;

	// Token: 0x04000966 RID: 2406
	public Text _nameText;

	// Token: 0x04000967 RID: 2407
	public Text _valueText;
}
