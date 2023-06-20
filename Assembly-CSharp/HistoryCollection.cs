using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

// Token: 0x02000119 RID: 281
[Serializable]
public class HistoryCollection
{
	// Token: 0x06000607 RID: 1543 RVA: 0x0002AEF8 File Offset: 0x000290F8
	public HistoryCollection(DataRecordingPackage[][] histories)
	{
		this.mObjectHistories = new ObjectHistory[histories.Length];
		for (int i = 0; i < this.mObjectHistories.Length; i++)
		{
			this.mObjectHistories[i] = new ObjectHistory(histories[i]);
			this.mSceneName = this.mObjectHistories[i][i].mScene;
			this.mPrefab = this.mObjectHistories[i][i].mPrefabName;
		}
	}

	// Token: 0x06000608 RID: 1544 RVA: 0x0002AF74 File Offset: 0x00029174
	public HistoryCollection(byte[] data)
	{
		Debug.Log("creating HistoryCollection from bytes ");
		MemoryStream memoryStream = new MemoryStream();
		memoryStream.Write(data, 0, data.Length);
		memoryStream.Seek(0L, SeekOrigin.Begin);
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		HistoryCollection historyCollection = (HistoryCollection)binaryFormatter.Deserialize(memoryStream);
		memoryStream.Close();
		this.mObjectHistories = historyCollection.mObjectHistories;
	}

	// Token: 0x1700012E RID: 302
	// (get) Token: 0x06000609 RID: 1545 RVA: 0x0002AFD4 File Offset: 0x000291D4
	public float Time
	{
		get
		{
			ObjectHistory objectHistory = this.mObjectHistories[0];
			return objectHistory[objectHistory.Length - 1].mTime;
		}
	}

	// Token: 0x1700012F RID: 303
	// (get) Token: 0x0600060A RID: 1546 RVA: 0x0002B000 File Offset: 0x00029200
	public int Length
	{
		get
		{
			return this.mObjectHistories.Length;
		}
	}

	// Token: 0x0600060B RID: 1547 RVA: 0x0002B00C File Offset: 0x0002920C
	public static HistoryCollection FetchLocalFileByMap(int mapNumber)
	{
		string savePath = HistoryCollection.GetSavePath(mapNumber);
		Debug.Log("Loaded Recording from " + savePath);
		if (File.Exists(savePath))
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			FileStream fileStream = File.Open(savePath, FileMode.Open);
			HistoryCollection result = (HistoryCollection)binaryFormatter.Deserialize(fileStream);
			fileStream.Close();
			return result;
		}
		Debug.Log("File not found " + savePath);
		return null;
	}

	// Token: 0x0600060C RID: 1548 RVA: 0x0002B070 File Offset: 0x00029270
	public static string GetSavePath(int Scene)
	{
		return string.Concat(new object[]
		{
			Application.persistentDataPath,
			"/levelRecording_scene_",
			Scene,
			".dat"
		});
	}

	// Token: 0x0600060D RID: 1549 RVA: 0x0002B0AC File Offset: 0x000292AC
	public static byte[] GetFileData(int Map)
	{
		return File.ReadAllBytes(string.Concat(new object[]
		{
			Application.persistentDataPath,
			"/levelRecording_scene_",
			Map,
			".dat"
		}));
	}

	// Token: 0x0600060E RID: 1550 RVA: 0x0002B0E0 File Offset: 0x000292E0
	public void SaveToFile(int mapNumber)
	{
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		string savePath = HistoryCollection.GetSavePath(mapNumber);
		FileStream fileStream = File.Create(savePath);
		Debug.Log("Saving to: " + savePath);
		binaryFormatter.Serialize(fileStream, this);
		fileStream.Close();
	}

	// Token: 0x17000130 RID: 304
	public ObjectHistory this[int key]
	{
		get
		{
			return this.mObjectHistories[key];
		}
		set
		{
			this.mObjectHistories[key] = value;
		}
	}

	// Token: 0x04000469 RID: 1129
	public ObjectHistory[] mObjectHistories;

	// Token: 0x0400046A RID: 1130
	private int mSceneName;

	// Token: 0x0400046B RID: 1131
	private string mPrefab;
}
