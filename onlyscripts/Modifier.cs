using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000248 RID: 584
public class Modifier : getModifierOptions
{
	// Token: 0x06000E4D RID: 3661 RVA: 0x0005D4A8 File Offset: 0x0005B6A8
	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	// Token: 0x06000E4E RID: 3662 RVA: 0x0005D4B0 File Offset: 0x0005B6B0
	public override bool Equals(object o)
	{
		Modifier modifier = o as Modifier;
		return modifier.initParameters[0].Arguments[0] == this.initParameters[0].Arguments[0] && modifier.initParameters[modifier.initParameters.Length - 1].Arguments[0] == this.initParameters[this.initParameters.Length - 1].Arguments[0] && this.initParameters.Length == modifier.initParameters.Length;
	}

	// Token: 0x1700028C RID: 652
	// (get) Token: 0x06000E4F RID: 3663 RVA: 0x0005D53C File Offset: 0x0005B73C
	public string Name
	{
		get
		{
			return this._name;
		}
	}

	// Token: 0x1700028D RID: 653
	// (get) Token: 0x06000E50 RID: 3664 RVA: 0x0005D544 File Offset: 0x0005B744
	[SerializeField]
	private string _LEGEND
	{
		get
		{
			return "s = string, f = float, b = bool\nSlider: (s name,f min,f max, \nf value,b clamp) Bool: (s name, b startValue) Drop: (s name, s[] values,i value)";
		}
	}

	// Token: 0x06000E51 RID: 3665 RVA: 0x0005D54C File Offset: 0x0005B74C
	private void Awake()
	{
		this._name = base.gameObject.name.Split(new char[]
		{
			'('
		})[0];
		Modifier.DefaultParamList = this.populateParamListFromInitParams();
		if (this.roadPoint)
		{
			return;
		}
		DefaultParameters.addParameterToDictionary(base.name.Split(new char[]
		{
			'('
		})[0], Modifier.DefaultParamList.ToArray());
	}

	// Token: 0x06000E52 RID: 3666 RVA: 0x0005D5BC File Offset: 0x0005B7BC
	public void getOptions(GameObject g = null)
	{
		GameObject gameObject = g ? g : base.gameObject;
		Debug.Log(gameObject.name);
		modifierToolLogic.Instance.setObjectInfo(this.populateParamListFromInitParams(), gameObject, this);
	}

	// Token: 0x06000E53 RID: 3667 RVA: 0x0005D600 File Offset: 0x0005B800
	public void getOptions(GameObject[] g)
	{
		modifierToolLogic.Instance.setObjectInfo(this.populateParamListFromInitParams(), g, this);
	}

	// Token: 0x06000E54 RID: 3668 RVA: 0x0005D614 File Offset: 0x0005B814
	public static getModifierOptions.Parameter[] getDefaultParams()
	{
		if (Modifier.DefaultParamList == null)
		{
			return null;
		}
		return Modifier.DefaultParamList.ToArray();
	}

	// Token: 0x06000E55 RID: 3669 RVA: 0x0005D62C File Offset: 0x0005B82C
	private List<getModifierOptions.Parameter> populateParamListFromInitParams()
	{
		List<getModifierOptions.Parameter> list = new List<getModifierOptions.Parameter>();
		foreach (Modifier.fakeParameter fakeParameter in this.initParameters)
		{
			getModifierOptions.Parameter item;
			switch (fakeParameter.TYPE)
			{
			case Modifier.Faketypes.Slider:
				item = new getModifierOptions.SliderParameter(fakeParameter.Arguments[0], float.Parse(fakeParameter.Arguments[1]), float.Parse(fakeParameter.Arguments[2]), float.Parse(fakeParameter.Arguments[3]), bool.Parse(fakeParameter.Arguments[4]));
				break;
			case Modifier.Faketypes.Bool:
				item = new getModifierOptions.BoolParameter(fakeParameter.Arguments[0], bool.Parse(fakeParameter.Arguments[1]));
				break;
			case Modifier.Faketypes.Dropdown:
			{
				string[] array2 = new string[fakeParameter.Arguments.Length - 2];
				for (int j = 0; j < array2.Length; j++)
				{
					array2[j] = fakeParameter.Arguments[j + 1];
				}
				item = new getModifierOptions.DropdownParameter(fakeParameter.Arguments[0], array2, int.Parse(fakeParameter.Arguments[fakeParameter.Arguments.Length - 1]));
				break;
			}
			case Modifier.Faketypes.Color:
				item = new getModifierOptions.ColorParameter(fakeParameter.Arguments[0], extensionMethods.ColorToHex(base.GetComponentInChildren<Renderer>().material.color));
				break;
			case Modifier.Faketypes.Input:
				item = new getModifierOptions.InputFieldParameter(fakeParameter.Arguments[0], string.Empty);
				break;
			default:
				throw new Exception("Wrong or NULL TYPE!");
			}
			list.Add(item);
		}
		return list;
	}

	// Token: 0x04000AE9 RID: 2793
	public bool roadPoint;

	// Token: 0x04000AEA RID: 2794
	private string _name = string.Empty;

	// Token: 0x04000AEB RID: 2795
	[TextArea(0, 10)]
	public string LEGEND = "s = string, f = float, b = bool\nSlider: (s name,f min,f max, \nf value,b clamp) Bool: (s name, b startValue) Drop: (s name, s[] values,i value) Color: (s name, s hexValue)";

	// Token: 0x04000AEC RID: 2796
	public Modifier.fakeParameter[] initParameters;

	// Token: 0x04000AED RID: 2797
	private static List<getModifierOptions.Parameter> DefaultParamList;

	// Token: 0x02000249 RID: 585
	[Serializable]
	public enum Faketypes
	{
		// Token: 0x04000AEF RID: 2799
		Slider,
		// Token: 0x04000AF0 RID: 2800
		Bool,
		// Token: 0x04000AF1 RID: 2801
		Dropdown,
		// Token: 0x04000AF2 RID: 2802
		Color,
		// Token: 0x04000AF3 RID: 2803
		Input
	}

	// Token: 0x0200024A RID: 586
	[Serializable]
	public class fakeParameter
	{
		// Token: 0x04000AF4 RID: 2804
		[SerializeField]
		public Modifier.Faketypes TYPE;

		// Token: 0x04000AF5 RID: 2805
		public string[] Arguments;
	}
}
