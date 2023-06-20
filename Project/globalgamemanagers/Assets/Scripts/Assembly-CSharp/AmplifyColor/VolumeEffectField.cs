using System;
using UnityEngine;

namespace AmplifyColor
{
	[Serializable]
	public class VolumeEffectField
	{
		public VolumeEffectField(string fieldName, string fieldType)
		{
		}

		public string fieldName;
		public string fieldType;
		public float valueSingle;
		public Color valueColor;
		public bool valueBoolean;
		public Vector2 valueVector2;
		public Vector3 valueVector3;
		public Vector4 valueVector4;
	}
}
