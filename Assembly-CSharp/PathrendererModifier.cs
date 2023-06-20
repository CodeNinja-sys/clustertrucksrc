using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000252 RID: 594
public class PathrendererModifier : getModifierOptions
{
	// Token: 0x06000E84 RID: 3716 RVA: 0x0005E160 File Offset: 0x0005C360
	private void Awake()
	{
		foreach (getModifierOptions.Parameter parameter in this.ParamList)
		{
			Debug.Log("START: " + parameter.getName());
		}
		getModifierOptions.DropdownParameter item = new getModifierOptions.DropdownParameter("WaypointType", new string[]
		{
			"Red",
			"Yellow",
			"Green",
			"Blue"
		}, 0);
		this.ParamList.Add(item);
		PathrendererModifier.DefaultParamList = this.ParamList;
	}

	// Token: 0x06000E85 RID: 3717 RVA: 0x0005E220 File Offset: 0x0005C420
	private void Start()
	{
	}

	// Token: 0x06000E86 RID: 3718 RVA: 0x0005E224 File Offset: 0x0005C424
	public static getModifierOptions.Parameter[] getDefaultParams()
	{
		if (PathrendererModifier.DefaultParamList == null)
		{
			return null;
		}
		return PathrendererModifier.DefaultParamList.ToArray();
	}

	// Token: 0x06000E87 RID: 3719 RVA: 0x0005E23C File Offset: 0x0005C43C
	public override void getOptions()
	{
		Debug.Log("pathRenderer!");
		this.ParamList[0].setValue((base.GetComponentInChildren<wayPointType>().getType() - 1).ToString());
		modifierToolLogic.Instance.setObjectInfoPathRenderer(this.ParamList, base.gameObject);
	}

	// Token: 0x06000E88 RID: 3720 RVA: 0x0005E290 File Offset: 0x0005C490
	public IEnumerator setTypes(int _type)
	{
		Debug.Log(_type);
		int myIndex = int.Parse(base.gameObject.name.Split(new char[]
		{
			'_'
		})[1]);
		foreach (wayPointType item in base.GetComponentsInChildren<wayPointType>())
		{
			item.setType(_type + 1, false);
		}
		yield return 0;
		yield break;
	}

	// Token: 0x06000E89 RID: 3721 RVA: 0x0005E2BC File Offset: 0x0005C4BC
	private void setInfo()
	{
	}

	// Token: 0x04000B13 RID: 2835
	public List<getModifierOptions.Parameter> ParamList = new List<getModifierOptions.Parameter>(1);

	// Token: 0x04000B14 RID: 2836
	private static List<getModifierOptions.Parameter> DefaultParamList;

	// Token: 0x04000B15 RID: 2837
	private string m_Name;
}
