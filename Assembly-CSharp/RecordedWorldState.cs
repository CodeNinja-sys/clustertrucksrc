using System;

// Token: 0x0200012B RID: 299
public class RecordedWorldState
{
	// Token: 0x06000675 RID: 1653 RVA: 0x0002C3F8 File Offset: 0x0002A5F8
	public RecordedWorldState(int size)
	{
		this.mRecordedObjects = new DataRecordingPackage[size];
	}

	// Token: 0x17000136 RID: 310
	public DataRecordingPackage this[int key]
	{
		get
		{
			return this.mRecordedObjects[key];
		}
		set
		{
			this.mRecordedObjects[key] = value;
		}
	}

	// Token: 0x040004A5 RID: 1189
	public float mdTimeRecordingStart;

	// Token: 0x040004A6 RID: 1190
	private DataRecordingPackage[] mRecordedObjects;
}
