using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace AmplifyColor
{
	// Token: 0x0200000D RID: 13
	[Serializable]
	public class VolumeEffectComponent
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00003F68 File Offset: 0x00002168
		public VolumeEffectComponent(string name)
		{
			this.componentName = name;
			this.fields = new List<VolumeEffectField>();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003F84 File Offset: 0x00002184
		public VolumeEffectComponent(Component c, VolumeEffectComponentFlags compFlags) : this(compFlags.componentName)
		{
			foreach (VolumeEffectFieldFlags volumeEffectFieldFlags in compFlags.componentFields)
			{
				if (volumeEffectFieldFlags.blendFlag)
				{
					FieldInfo field = c.GetType().GetField(volumeEffectFieldFlags.fieldName);
					VolumeEffectField volumeEffectField = (!VolumeEffectField.IsValidType(field.FieldType.FullName)) ? null : new VolumeEffectField(field, c);
					if (volumeEffectField != null)
					{
						this.fields.Add(volumeEffectField);
					}
				}
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00004048 File Offset: 0x00002248
		public VolumeEffectField AddField(FieldInfo pi, Component c)
		{
			return this.AddField(pi, c, -1);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00004054 File Offset: 0x00002254
		public VolumeEffectField AddField(FieldInfo pi, Component c, int position)
		{
			VolumeEffectField volumeEffectField = (!VolumeEffectField.IsValidType(pi.FieldType.FullName)) ? null : new VolumeEffectField(pi, c);
			if (volumeEffectField != null)
			{
				if (position < 0 || position >= this.fields.Count)
				{
					this.fields.Add(volumeEffectField);
				}
				else
				{
					this.fields.Insert(position, volumeEffectField);
				}
			}
			return volumeEffectField;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000040C4 File Offset: 0x000022C4
		public void RemoveEffectField(VolumeEffectField field)
		{
			this.fields.Remove(field);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000040D4 File Offset: 0x000022D4
		public void UpdateComponent(Component c, VolumeEffectComponentFlags compFlags)
		{
			VolumeEffectFieldFlags fieldFlags;
			foreach (VolumeEffectFieldFlags fieldFlags2 in compFlags.componentFields)
			{
				fieldFlags = fieldFlags2;
				if (fieldFlags.blendFlag)
				{
					if (!this.fields.Exists((VolumeEffectField s) => s.fieldName == fieldFlags.fieldName))
					{
						FieldInfo field = c.GetType().GetField(fieldFlags.fieldName);
						VolumeEffectField volumeEffectField = (!VolumeEffectField.IsValidType(field.FieldType.FullName)) ? null : new VolumeEffectField(field, c);
						if (volumeEffectField != null)
						{
							this.fields.Add(volumeEffectField);
						}
					}
				}
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000041BC File Offset: 0x000023BC
		public VolumeEffectField GetEffectField(string fieldName)
		{
			return this.fields.Find((VolumeEffectField s) => s.fieldName == fieldName);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000041F0 File Offset: 0x000023F0
		public static FieldInfo[] ListAcceptableFields(Component c)
		{
			if (c == null)
			{
				return new FieldInfo[0];
			}
			FieldInfo[] source = c.GetType().GetFields();
			return (from f in source
			where VolumeEffectField.IsValidType(f.FieldType.FullName)
			select f).ToArray<FieldInfo>();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00004244 File Offset: 0x00002444
		public string[] GetFieldNames()
		{
			return (from r in this.fields
			select r.fieldName).ToArray<string>();
		}

		// Token: 0x04000065 RID: 101
		public string componentName;

		// Token: 0x04000066 RID: 102
		public List<VolumeEffectField> fields;
	}
}
