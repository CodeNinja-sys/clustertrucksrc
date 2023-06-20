using System;
using System.Collections.Generic;

namespace AmplifyColor
{
	[Serializable]
	public class VolumeEffect
	{
		public VolumeEffect(AmplifyColorBase effect)
		{
		}

		public AmplifyColorBase gameObject;
		public List<VolumeEffectComponent> components;
	}
}
