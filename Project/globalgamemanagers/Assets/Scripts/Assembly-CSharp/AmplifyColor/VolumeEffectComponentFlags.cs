using System;
using System.Collections.Generic;

namespace AmplifyColor
{
	[Serializable]
	public class VolumeEffectComponentFlags
	{
		public VolumeEffectComponentFlags(string name)
		{
		}

		public string componentName;
		public List<VolumeEffectFieldFlags> componentFields;
		public bool blendFlag;
	}
}
