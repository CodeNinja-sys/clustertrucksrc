using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AmplifyColor
{
	// Token: 0x02000012 RID: 18
	[Serializable]
	public class VolumeEffectFlags
	{
		// Token: 0x06000069 RID: 105 RVA: 0x000052E8 File Offset: 0x000034E8
		public VolumeEffectFlags()
		{
			this.components = new List<VolumeEffectComponentFlags>();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000052FC File Offset: 0x000034FC
		public void AddComponent(Component c)
		{
			VolumeEffectComponentFlags volumeEffectComponentFlags;
			if ((volumeEffectComponentFlags = this.components.Find((VolumeEffectComponentFlags s) => s.componentName == c.GetType() + string.Empty)) != null)
			{
				volumeEffectComponentFlags.UpdateComponentFlags(c);
			}
			else
			{
				this.components.Add(new VolumeEffectComponentFlags(c));
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x0000535C File Offset: 0x0000355C
		public void UpdateFlags(VolumeEffect effectVol)
		{
			VolumeEffectComponent comp;
			foreach (VolumeEffectComponent comp2 in effectVol.components)
			{
				comp = comp2;
				VolumeEffectComponentFlags volumeEffectComponentFlags;
				if ((volumeEffectComponentFlags = this.components.Find((VolumeEffectComponentFlags s) => s.componentName == comp.componentName)) == null)
				{
					this.components.Add(new VolumeEffectComponentFlags(comp));
				}
				else
				{
					volumeEffectComponentFlags.UpdateComponentFlags(comp);
				}
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00005410 File Offset: 0x00003610
		public static void UpdateCamFlags(AmplifyColorBase[] effects, AmplifyColorVolumeBase[] volumes)
		{
			foreach (AmplifyColorBase amplifyColorBase in effects)
			{
				amplifyColorBase.EffectFlags = new VolumeEffectFlags();
				foreach (AmplifyColorVolumeBase amplifyColorVolumeBase in volumes)
				{
					VolumeEffect volumeEffect = amplifyColorVolumeBase.EffectContainer.GetVolumeEffect(amplifyColorBase);
					if (volumeEffect != null)
					{
						amplifyColorBase.EffectFlags.UpdateFlags(volumeEffect);
					}
				}
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00005488 File Offset: 0x00003688
		public VolumeEffect GenerateEffectData(AmplifyColorBase go)
		{
			VolumeEffect volumeEffect = new VolumeEffect(go);
			foreach (VolumeEffectComponentFlags volumeEffectComponentFlags in this.components)
			{
				if (volumeEffectComponentFlags.blendFlag)
				{
					Component component = go.GetComponent(volumeEffectComponentFlags.componentName);
					if (component != null)
					{
						volumeEffect.AddComponent(component, volumeEffectComponentFlags);
					}
				}
			}
			return volumeEffect;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00005524 File Offset: 0x00003724
		public VolumeEffectComponentFlags GetComponentFlags(string compName)
		{
			return this.components.Find((VolumeEffectComponentFlags s) => s.componentName == compName);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00005558 File Offset: 0x00003758
		public string[] GetComponentNames()
		{
			return (from r in this.components
			where r.blendFlag
			select r.componentName).ToArray<string>();
		}

		// Token: 0x0400007A RID: 122
		public List<VolumeEffectComponentFlags> components;
	}
}
