using System;
using UnityEngine.UI;

// Token: 0x020001F3 RID: 499
public class InputfieldModifierSet : ModifierSetBase
{
	// Token: 0x06000BC6 RID: 3014 RVA: 0x00049158 File Offset: 0x00047358
	private void Start()
	{
	}

	// Token: 0x06000BC7 RID: 3015 RVA: 0x0004915C File Offset: 0x0004735C
	private void Update()
	{
	}

	// Token: 0x06000BC8 RID: 3016 RVA: 0x00049160 File Offset: 0x00047360
	public void SetInfo(getModifierOptions.InputFieldParameter _param)
	{
		this._parameter = _param;
		this.NameText.text = this._parameter.getName();
		this.InputField.text = _param.getValue();
	}

	// Token: 0x06000BC9 RID: 3017 RVA: 0x0004919C File Offset: 0x0004739C
	public override getModifierOptions.Parameter getParameter()
	{
		return new getModifierOptions.InputFieldParameter(this.NameText.text, this.InputField.text);
	}

	// Token: 0x04000861 RID: 2145
	private getModifierOptions.InputFieldParameter _parameter;

	// Token: 0x04000862 RID: 2146
	public InputField InputField;

	// Token: 0x04000863 RID: 2147
	public Text NameText;
}
