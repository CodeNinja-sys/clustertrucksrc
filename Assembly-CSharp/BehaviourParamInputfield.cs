using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000208 RID: 520
public class BehaviourParamInputfield : BehaviourParamCellSetBase
{
	// Token: 0x06000C4B RID: 3147 RVA: 0x0004C9D0 File Offset: 0x0004ABD0
	public override void Init(string description, string[] Parameters)
	{
		this._descriptionText.text = description;
		this._inputField.text = Parameters[0];
	}

	// Token: 0x06000C4C RID: 3148 RVA: 0x0004C9EC File Offset: 0x0004ABEC
	public override string getValue()
	{
		return this._inputField.text;
	}

	// Token: 0x040008DC RID: 2268
	[SerializeField]
	private Text _descriptionText;

	// Token: 0x040008DD RID: 2269
	[SerializeField]
	private InputField _inputField;
}
