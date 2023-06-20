using System;
using System.Reflection;

namespace AmplifyColor
{
	// Token: 0x02000010 RID: 16
	[Serializable]
	public class VolumeEffectFieldFlags
	{
		// Token: 0x0600005F RID: 95 RVA: 0x00004FC4 File Offset: 0x000031C4
		public VolumeEffectFieldFlags(FieldInfo pi)
		{
			this.fieldName = pi.Name;
			this.fieldType = pi.FieldType.FullName;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00004FF4 File Offset: 0x000031F4
		public VolumeEffectFieldFlags(VolumeEffectField field)
		{
			this.fieldName = field.fieldName;
			this.fieldType = field.fieldType;
			this.blendFlag = true;
		}

		// Token: 0x04000072 RID: 114
		public string fieldName;

		// Token: 0x04000073 RID: 115
		public string fieldType;

		// Token: 0x04000074 RID: 116
		public bool blendFlag;
	}
}
