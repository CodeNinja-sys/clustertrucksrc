using System;
using UnityEngine;

// Token: 0x0200020E RID: 526
public class killableModifierReciever : ModifierRecieverBase
{
	// Token: 0x06000C5E RID: 3166 RVA: 0x0004D074 File Offset: 0x0004B274
	private void Awake()
	{
	}

	// Token: 0x06000C5F RID: 3167 RVA: 0x0004D078 File Offset: 0x0004B278
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
			foreach (Collider collider in base.GetComponentsInChildren<Collider>(true))
			{
				collider.tag = "Untagged";
			}
		}
		else if (_params[0].getValue() == bool.TrueString)
		{
			foreach (Collider collider2 in base.GetComponentsInChildren<Collider>(true))
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
