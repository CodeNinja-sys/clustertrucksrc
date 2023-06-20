using System;

// Token: 0x0200011A RID: 282
[Serializable]
public class ObjectHistory
{
	// Token: 0x06000611 RID: 1553 RVA: 0x0002B138 File Offset: 0x00029338
	public ObjectHistory(DataRecordingPackage[] historyTimeline)
	{
		this.mHistoryTimeLine = historyTimeline;
	}

	// Token: 0x17000131 RID: 305
	// (get) Token: 0x06000612 RID: 1554 RVA: 0x0002B148 File Offset: 0x00029348
	public int Length
	{
		get
		{
			return this.mHistoryTimeLine.Length;
		}
	}

	// Token: 0x17000132 RID: 306
	public DataRecordingPackage this[int key]
	{
		get
		{
			return this.mHistoryTimeLine[key];
		}
		set
		{
			this.mHistoryTimeLine[key] = value;
		}
	}

	// Token: 0x0400046C RID: 1132
	public DataRecordingPackage[] mHistoryTimeLine;
}
