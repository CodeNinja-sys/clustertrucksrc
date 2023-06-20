using System;
using System.Collections.Generic;

namespace AmplifyColor
{
	[Serializable]
	public class VolumeEffectComponent
	{
		public VolumeEffectComponent(string name)
		{
		}

		public string componentName;
		public List<VolumeEffectField> fields;
	}
}
