using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000223 RID: 547
public class desBlockModifier : getModifierOptions
{
	// Token: 0x06000CD2 RID: 3282 RVA: 0x0004F1CC File Offset: 0x0004D3CC
	private void Start()
	{
		getModifierOptions.BoolParameter item = new getModifierOptions.BoolParameter("Bool11", false);
		getModifierOptions.SliderParameter item2 = new getModifierOptions.SliderParameter("SLider3", 2f, 10f, 5f, true);
		this._ParamList.Add(item);
		this._ParamList.Add(item2);
		desBlockModifier.DefaultParamList = this._ParamList;
	}

	// Token: 0x06000CD3 RID: 3283 RVA: 0x0004F224 File Offset: 0x0004D424
	public static getModifierOptions.Parameter[] getDefaultParams()
	{
		return desBlockModifier.DefaultParamList.ToArray();
	}

	// Token: 0x06000CD4 RID: 3284 RVA: 0x0004F230 File Offset: 0x0004D430
	public override void getOptions()
	{
		Debug.Log("DesBlock!");
		modifierToolLogic.Instance.setObjectInfo(this._ParamList, base.gameObject, new Modifier());
	}

	// Token: 0x06000CD5 RID: 3285 RVA: 0x0004F264 File Offset: 0x0004D464
	private void setInfo()
	{
	}

	// Token: 0x04000960 RID: 2400
	public string[] _ParamNames;

	// Token: 0x04000961 RID: 2401
	public List<getModifierOptions.Parameter> _ParamList = new List<getModifierOptions.Parameter>();

	// Token: 0x04000962 RID: 2402
	private static List<getModifierOptions.Parameter> DefaultParamList;

	// Token: 0x04000963 RID: 2403
	private string m_Name;
}
