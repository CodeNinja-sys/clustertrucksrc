using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace AmplifyColor
{
	// Token: 0x0200000C RID: 12
	[Serializable]
	public class VolumeEffectField
	{
		// Token: 0x0600003D RID: 61 RVA: 0x00003D74 File Offset: 0x00001F74
		public VolumeEffectField(string fieldName, string fieldType)
		{
			this.fieldName = fieldName;
			this.fieldType = fieldType;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003D8C File Offset: 0x00001F8C
		public VolumeEffectField(FieldInfo pi, Component c) : this(pi.Name, pi.FieldType.FullName)
		{
			object value = pi.GetValue(c);
			this.UpdateValue(value);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003DC0 File Offset: 0x00001FC0
		public static bool IsValidType(string type)
		{
			if (type != null)
			{
				if (VolumeEffectField.<>f__switch$map0 == null)
				{
					VolumeEffectField.<>f__switch$map0 = new Dictionary<string, int>(6)
					{
						{
							"System.Single",
							0
						},
						{
							"System.Boolean",
							0
						},
						{
							"UnityEngine.Color",
							0
						},
						{
							"UnityEngine.Vector2",
							0
						},
						{
							"UnityEngine.Vector3",
							0
						},
						{
							"UnityEngine.Vector4",
							0
						}
					};
				}
				int num;
				if (VolumeEffectField.<>f__switch$map0.TryGetValue(type, out num))
				{
					if (num == 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003E54 File Offset: 0x00002054
		public void UpdateValue(object val)
		{
			string text = this.fieldType;
			switch (text)
			{
			case "System.Single":
				this.valueSingle = (float)val;
				break;
			case "System.Boolean":
				this.valueBoolean = (bool)val;
				break;
			case "UnityEngine.Color":
				this.valueColor = (Color)val;
				break;
			case "UnityEngine.Vector2":
				this.valueVector2 = (Vector2)val;
				break;
			case "UnityEngine.Vector3":
				this.valueVector3 = (Vector3)val;
				break;
			case "UnityEngine.Vector4":
				this.valueVector4 = (Vector4)val;
				break;
			}
		}

		// Token: 0x0400005B RID: 91
		public string fieldName;

		// Token: 0x0400005C RID: 92
		public string fieldType;

		// Token: 0x0400005D RID: 93
		public float valueSingle;

		// Token: 0x0400005E RID: 94
		public Color valueColor;

		// Token: 0x0400005F RID: 95
		public bool valueBoolean;

		// Token: 0x04000060 RID: 96
		public Vector2 valueVector2;

		// Token: 0x04000061 RID: 97
		public Vector3 valueVector3;

		// Token: 0x04000062 RID: 98
		public Vector4 valueVector4;
	}
}
