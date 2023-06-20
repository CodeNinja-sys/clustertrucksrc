using System;
using UnityEngine;

// Token: 0x0200017A RID: 378
public class PropModifierReciever : ModifierRecieverBase
{
	// Token: 0x06000876 RID: 2166 RVA: 0x00037C1C File Offset: 0x00035E1C
	private void Awake()
	{
	}

	// Token: 0x06000877 RID: 2167 RVA: 0x00037C20 File Offset: 0x00035E20
	public override void sendInfo(ObjectParameterContainer[] _params, bool temporary = false)
	{
		if (temporary)
		{
			return;
		}
		Debug.Log("Initalizing Modifiers for: " + base.gameObject.name);
		if (_params.Length > 1)
		{
			Debug.LogError("Overflow modifiers");
			foreach (ObjectParameterContainer objectParameterContainer in _params)
			{
				Debug.Log("Name: " + objectParameterContainer.getName() + " Valye: " + objectParameterContainer.getValue());
			}
		}
		if (_params[0].getValue() == bool.FalseString)
		{
			foreach (Collider collider in base.GetComponentsInChildren<Collider>())
			{
				collider.tag = "Untagged";
			}
		}
		else if (_params[0].getValue() == bool.TrueString)
		{
			foreach (Collider collider2 in base.GetComponentsInChildren<Collider>())
			{
				collider2.tag = "kill";
			}
		}
		else
		{
			Debug.LogError("NOT BOOL: " + _params[0]);
		}
	}
}
