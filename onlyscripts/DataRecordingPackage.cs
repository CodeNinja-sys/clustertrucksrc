using System;

// Token: 0x02000115 RID: 277
[Serializable]
public class DataRecordingPackage
{
	// Token: 0x0400045C RID: 1116
	public SerializableVector3 mPosition;

	// Token: 0x0400045D RID: 1117
	public SerializableQuaternion mQuaternion;

	// Token: 0x0400045E RID: 1118
	public SerializableVector3 mAngularVelocity;

	// Token: 0x0400045F RID: 1119
	public SerializableVector3 mVelocity;

	// Token: 0x04000460 RID: 1120
	public SerializableVector3 mExtra;

	// Token: 0x04000461 RID: 1121
	public int mScene;

	// Token: 0x04000462 RID: 1122
	public int mInstanceID;

	// Token: 0x04000463 RID: 1123
	public bool mAddon;

	// Token: 0x04000464 RID: 1124
	public float mTime;

	// Token: 0x04000465 RID: 1125
	public string mName;

	// Token: 0x04000466 RID: 1126
	public string mPrefabName;

	// Token: 0x04000467 RID: 1127
	public bool mFirstPackage;
}
