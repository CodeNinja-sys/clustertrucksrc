using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200020D RID: 525
public class TruckModifierReciever : ModifierRecieverBase
{
	// Token: 0x06000C5B RID: 3163 RVA: 0x0004CF3C File Offset: 0x0004B13C
	private void Awake()
	{
		foreach (Renderer renderer in base.GetComponentsInChildren<Renderer>())
		{
			this._currentMaterials.Add(renderer.material);
		}
	}

	// Token: 0x06000C5C RID: 3164 RVA: 0x0004CF7C File Offset: 0x0004B17C
	public override void sendInfo(ObjectParameterContainer[] _params, bool temporary = false)
	{
		foreach (ObjectParameterContainer objectParameterContainer in _params)
		{
			if (objectParameterContainer.getName() == "Emission")
			{
				this._emissionColor = extensionMethods.HexToColor(objectParameterContainer.getValue());
			}
			else if (objectParameterContainer.getName() == "EmissionStrength")
			{
				this._emissionStrength = float.Parse(objectParameterContainer.getValue());
			}
		}
		foreach (Material material in this._currentMaterials)
		{
			material.SetColor("_EmissionColor", this._emissionColor * this._emissionStrength);
		}
	}

	// Token: 0x040008EE RID: 2286
	private const string EMISSION_MODIFIER_KEY = "Emission";

	// Token: 0x040008EF RID: 2287
	private const string EMISSION_STRENGTH_MODIFIER_KEY = "EmissionStrength";

	// Token: 0x040008F0 RID: 2288
	private Color _emissionColor = Color.black;

	// Token: 0x040008F1 RID: 2289
	private float _emissionStrength;

	// Token: 0x040008F2 RID: 2290
	private List<Material> _currentMaterials = new List<Material>();
}
