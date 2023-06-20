using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001FB RID: 507
public class DefaultParameters : getModifierOptions
{
	// Token: 0x06000C06 RID: 3078 RVA: 0x0004AB68 File Offset: 0x00048D68
	private void Update()
	{
		this.Parameters.Clear();
		foreach (KeyValuePair<string, getModifierOptions.Parameter[]> keyValuePair in DefaultParameters._dictionary)
		{
			getModifierOptions.Parameter[] array;
			DefaultParameters._dictionary.TryGetValue(keyValuePair.Key, out array);
			for (int i = 0; i < array.Length; i++)
			{
				this.Parameters.Add(string.Concat(new string[]
				{
					keyValuePair.Key,
					"Type: ",
					array[i].getName(),
					" Value: ",
					array[i].getValue()
				}));
			}
		}
	}

	// Token: 0x06000C07 RID: 3079 RVA: 0x0004AC40 File Offset: 0x00048E40
	public static getModifierOptions.Parameter[] getDefaultParameters(string _name)
	{
		if (_name.ToLower().Contains("roadpoint"))
		{
			return PathrendererModifier.getDefaultParams();
		}
		getModifierOptions.Parameter[] arr;
		DefaultParameters._dictionary.TryGetValue(_name, out arr);
		return arr.Copy();
	}

	// Token: 0x06000C08 RID: 3080 RVA: 0x0004AC80 File Offset: 0x00048E80
	public static void addParameterToDictionary(string _name, getModifierOptions.Parameter[] _params)
	{
		if (DefaultParameters._dictionary.ContainsKey(_name))
		{
			return;
		}
		DefaultParameters._dictionary.Add(_name, _params);
		Debug.Log(_name + " Added To dictionary");
	}

	// Token: 0x0400089D RID: 2205
	public List<string> Parameters = new List<string>();

	// Token: 0x0400089E RID: 2206
	public static Dictionary<string, getModifierOptions.Parameter[]> _dictionary = new Dictionary<string, getModifierOptions.Parameter[]>();
}
