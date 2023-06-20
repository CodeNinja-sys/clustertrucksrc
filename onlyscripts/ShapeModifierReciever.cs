using System;
using UnityEngine;

// Token: 0x0200020C RID: 524
public class ShapeModifierReciever : ModifierRecieverBase
{
	// Token: 0x06000C57 RID: 3159 RVA: 0x0004CCD8 File Offset: 0x0004AED8
	private void Awake()
	{
		this._currentMaterial = base.GetComponent<Renderer>().material;
	}

	// Token: 0x06000C58 RID: 3160 RVA: 0x0004CCEC File Offset: 0x0004AEEC
	private void Start()
	{
	}

	// Token: 0x06000C59 RID: 3161 RVA: 0x0004CCF0 File Offset: 0x0004AEF0
	public override void sendInfo(ObjectParameterContainer[] _params, bool temporary = false)
	{
		Debug.Log("Setting Modifiers For: " + base.gameObject.name, this);
		foreach (ObjectParameterContainer objectParameterContainer in _params)
		{
			if (objectParameterContainer.getName() == "Killable")
			{
				if (!temporary)
				{
					if (objectParameterContainer.getValue() == bool.FalseString)
					{
						foreach (Collider collider in base.GetComponentsInChildren<Collider>())
						{
							collider.tag = "Untagged";
						}
					}
					else if (objectParameterContainer.getValue() == bool.TrueString)
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
			else if (objectParameterContainer.getName() == "Smoothness")
			{
				this._currentMaterial.SetFloat("_Glossiness", float.Parse(objectParameterContainer.getValue()));
			}
			else if (objectParameterContainer.getName() == "Metallic")
			{
				this._currentMaterial.SetFloat("_Metallic", float.Parse(objectParameterContainer.getValue()));
			}
			else if (objectParameterContainer.getName() == "Color")
			{
				base.GetComponent<Renderer>().material.color = extensionMethods.HexToColor(objectParameterContainer.getValue());
			}
			else if (objectParameterContainer.getName() == "Emission")
			{
				this._emissionColor = extensionMethods.HexToColor(objectParameterContainer.getValue());
			}
			else if (objectParameterContainer.getName() == "EmissionStrength")
			{
				this._emissionStrength = float.Parse(objectParameterContainer.getValue());
			}
		}
		this._currentMaterial.SetColor("_EmissionColor", this._emissionColor * this._emissionStrength);
	}

	// Token: 0x040008E5 RID: 2277
	private const string KILLABLE_MODIFIER_KEY = "Killable";

	// Token: 0x040008E6 RID: 2278
	private const string SMOOTHNESS_MODIFIER_KEY = "Smoothness";

	// Token: 0x040008E7 RID: 2279
	private const string METALLIC_MODIFIER_KEY = "Metallic";

	// Token: 0x040008E8 RID: 2280
	private const string EMISSION_MODIFIER_KEY = "Emission";

	// Token: 0x040008E9 RID: 2281
	private const string EMISSION_STRENGTH_MODIFIER_KEY = "EmissionStrength";

	// Token: 0x040008EA RID: 2282
	private const string COLOR_MODIFIER_KEY = "Color";

	// Token: 0x040008EB RID: 2283
	private Material _currentMaterial;

	// Token: 0x040008EC RID: 2284
	private Color _emissionColor = Color.black;

	// Token: 0x040008ED RID: 2285
	private float _emissionStrength;
}
