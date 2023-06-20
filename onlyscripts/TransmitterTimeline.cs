using System;

// Token: 0x02000133 RID: 307
public class TransmitterTimeline
{
	// Token: 0x060006AA RID: 1706 RVA: 0x0002CCAC File Offset: 0x0002AEAC
	public TransmitterTimeline(int size)
	{
		this.mFinishedTimeline = false;
		this.mRecordedObjects = new DataRecordingPackage[size];
	}

	// Token: 0x1700013A RID: 314
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

	// Token: 0x040004B9 RID: 1209
	private DataRecordingPackage[] mRecordedObjects;

	// Token: 0x040004BA RID: 1210
	public bool mFinishedTimeline;
}
