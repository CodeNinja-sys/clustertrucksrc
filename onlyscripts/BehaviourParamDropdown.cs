using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000207 RID: 519
public class BehaviourParamDropdown : BehaviourParamCellSetBase
{
	// Token: 0x06000C48 RID: 3144 RVA: 0x0004C970 File Offset: 0x0004AB70
	public override void Init(string description, string[] Parameters)
	{
		List<string> range = Parameters.ToList().GetRange(1, Parameters.Length - 1);
		this._descriptionText.text = description;
		this._dropdown.AddOptions(range);
	}

	// Token: 0x06000C49 RID: 3145 RVA: 0x0004C9A8 File Offset: 0x0004ABA8
	public override string getValue()
	{
		return this._dropdown.value.ToString();
	}

	// Token: 0x040008DA RID: 2266
	[SerializeField]
	private Text _descriptionText;

	// Token: 0x040008DB RID: 2267
	[SerializeField]
	private Dropdown _dropdown;
}
