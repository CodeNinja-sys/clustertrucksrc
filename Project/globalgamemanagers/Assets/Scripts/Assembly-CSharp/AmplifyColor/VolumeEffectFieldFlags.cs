using System;
using System.Reflection;

namespace AmplifyColor
{
	[Serializable]
	public class VolumeEffectFieldFlags
	{
		public VolumeEffectFieldFlags(FieldInfo pi)
		{
		}

		public string fieldName;
		public string fieldType;
		public bool blendFlag;
	}
}
