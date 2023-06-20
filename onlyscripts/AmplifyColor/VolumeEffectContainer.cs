using System;
using System.Collections.Generic;
using System.Linq;

namespace AmplifyColor
{
	// Token: 0x0200000F RID: 15
	[Serializable]
	public class VolumeEffectContainer
	{
		// Token: 0x06000058 RID: 88 RVA: 0x00004E8C File Offset: 0x0000308C
		public VolumeEffectContainer()
		{
			this.volumes = new List<VolumeEffect>();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00004EA0 File Offset: 0x000030A0
		public void AddColorEffect(AmplifyColorBase colorEffect)
		{
			VolumeEffect volumeEffect;
			if ((volumeEffect = this.volumes.Find((VolumeEffect s) => s.gameObject == colorEffect)) != null)
			{
				volumeEffect.UpdateVolume();
			}
			else
			{
				volumeEffect = new VolumeEffect(colorEffect);
				this.volumes.Add(volumeEffect);
				volumeEffect.UpdateVolume();
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00004F04 File Offset: 0x00003104
		public VolumeEffect AddJustColorEffect(AmplifyColorBase colorEffect)
		{
			VolumeEffect volumeEffect = new VolumeEffect(colorEffect);
			this.volumes.Add(volumeEffect);
			return volumeEffect;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00004F28 File Offset: 0x00003128
		public VolumeEffect GetVolumeEffect(AmplifyColorBase colorEffect)
		{
			VolumeEffect volumeEffect = this.volumes.Find((VolumeEffect s) => s.gameObject == colorEffect);
			if (volumeEffect == null)
			{
				volumeEffect = this.volumes.Find((VolumeEffect s) => s.gameObject != null && s.gameObject.SharedInstanceID == colorEffect.SharedInstanceID);
			}
			return volumeEffect;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00004F7C File Offset: 0x0000317C
		public void RemoveVolumeEffect(VolumeEffect volume)
		{
			this.volumes.Remove(volume);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00004F8C File Offset: 0x0000318C
		public AmplifyColorBase[] GetStoredEffects()
		{
			return (from r in this.volumes
			select r.gameObject).ToArray<AmplifyColorBase>();
		}

		// Token: 0x04000070 RID: 112
		public List<VolumeEffect> volumes;
	}
}
