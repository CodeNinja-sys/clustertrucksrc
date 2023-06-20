using System;
using UnityEngine.UI;

// Token: 0x020001F9 RID: 505
public class BoolModifierInfoSet : ModifierSetBase
{
	// Token: 0x06000BEF RID: 3055 RVA: 0x00049EA0 File Offset: 0x000480A0
	public void SetInfo(getModifierOptions.BoolParameter _param)
	{
		this._toggle.isOn = bool.Parse(_param.getValue());
		this._nameText.text = _param.getName();
	}

	// Token: 0x06000BF0 RID: 3056 RVA: 0x00049ED4 File Offset: 0x000480D4
	public override getModifierOptions.Parameter getParameter()
	{
		return new getModifierOptions.BoolParameter(this._nameText.text, this._toggle.isOn);
	}

	// Token: 0x04000881 RID: 2177
	public Toggle _toggle;

	// Token: 0x04000882 RID: 2178
	public Text _nameText;
}
