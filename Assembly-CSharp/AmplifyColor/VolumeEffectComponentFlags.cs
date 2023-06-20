using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace AmplifyColor
{
	// Token: 0x02000011 RID: 17
	[Serializable]
	public class VolumeEffectComponentFlags
	{
		// Token: 0x06000061 RID: 97 RVA: 0x0000501C File Offset: 0x0000321C
		public VolumeEffectComponentFlags(string name)
		{
			this.componentName = name;
			this.componentFields = new List<VolumeEffectFieldFlags>();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00005038 File Offset: 0x00003238
		public VolumeEffectComponentFlags(VolumeEffectComponent comp) : this(comp.componentName)
		{
			this.blendFlag = true;
			foreach (VolumeEffectField volumeEffectField in comp.fields)
			{
				if (VolumeEffectField.IsValidType(volumeEffectField.fieldType))
				{
					this.componentFields.Add(new VolumeEffectFieldFlags(volumeEffectField));
				}
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000050CC File Offset: 0x000032CC
		public VolumeEffectComponentFlags(Component c) : this(c.GetType() + string.Empty)
		{
			FieldInfo[] fields = c.GetType().GetFields();
			foreach (FieldInfo fieldInfo in fields)
			{
				if (VolumeEffectField.IsValidType(fieldInfo.FieldType.FullName))
				{
					this.componentFields.Add(new VolumeEffectFieldFlags(fieldInfo));
				}
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000513C File Offset: 0x0000333C
		public void UpdateComponentFlags(VolumeEffectComponent comp)
		{
			VolumeEffectField field;
			foreach (VolumeEffectField field2 in comp.fields)
			{
				field = field2;
				if (this.componentFields.Find((VolumeEffectFieldFlags s) => s.fieldName == field.fieldName) == null && VolumeEffectField.IsValidType(field.fieldType))
				{
					this.componentFields.Add(new VolumeEffectFieldFlags(field));
				}
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000051F0 File Offset: 0x000033F0
		public void UpdateComponentFlags(Component c)
		{
			FieldInfo[] fields = c.GetType().GetFields();
			foreach (FieldInfo pi in fields)
			{
				if (!this.componentFields.Exists((VolumeEffectFieldFlags s) => s.fieldName == pi.Name) && VolumeEffectField.IsValidType(pi.FieldType.FullName))
				{
					this.componentFields.Add(new VolumeEffectFieldFlags(pi));
				}
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000527C File Offset: 0x0000347C
		public string[] GetFieldNames()
		{
			return (from r in this.componentFields
			where r.blendFlag
			select r.fieldName).ToArray<string>();
		}

		// Token: 0x04000075 RID: 117
		public string componentName;

		// Token: 0x04000076 RID: 118
		public List<VolumeEffectFieldFlags> componentFields;

		// Token: 0x04000077 RID: 119
		public bool blendFlag;
	}
}
