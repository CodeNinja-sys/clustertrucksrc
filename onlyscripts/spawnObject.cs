using System;
using UnityEngine;

// Token: 0x020002B7 RID: 695
public class spawnObject : MonoBehaviour
{
	// Token: 0x06001081 RID: 4225 RVA: 0x0006B764 File Offset: 0x00069964
	private void Awake()
	{
		this.Replace();
	}

	// Token: 0x06001082 RID: 4226 RVA: 0x0006B76C File Offset: 0x0006996C
	private void Update()
	{
	}

	// Token: 0x06001083 RID: 4227 RVA: 0x0006B770 File Offset: 0x00069970
	private void Replace()
	{
		string[] array = base.name.Split(new char[]
		{
			' '
		});
		string str = array[0];
		GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(Resources.Load(this.path + "/" + str), base.transform.position, base.transform.rotation);
		for (int i = 0; i < this.currentID; i++)
		{
			if (this.targetLevers[i] != null)
			{
				this.targetLevers[i].GetComponent<Lever>().GetTarget(gameObject.transform, this.targetIDs[i]);
			}
		}
		if (this.info.Length > 0)
		{
			gameObject.SendMessage("sendInfo", this.info);
		}
		if (this.info2.Length > 0)
		{
			gameObject.SendMessage("sendInfo2", this.info2);
		}
		if (this.vectors.Length > 0)
		{
			gameObject.SendMessage("sendVectors", this.vectors);
		}
		if (this.transforms.Length > 0)
		{
			Vector3[] array2 = new Vector3[this.transforms.Length];
			for (int j = 0; j < this.transforms.Length; j++)
			{
				array2[j] = this.transforms[j].position;
			}
			gameObject.SendMessage("sendTransforms", array2);
		}
		gameObject.transform.parent = base.transform.parent;
		gameObject.transform.localScale = base.transform.localScale;
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06001084 RID: 4228 RVA: 0x0006B910 File Offset: 0x00069B10
	public void GetLever(Transform lever, int i)
	{
		this.targetIDs[this.currentID] = i;
		this.targetLevers[this.currentID] = lever;
		this.currentID++;
	}

	// Token: 0x04000D95 RID: 3477
	public string path = "props";

	// Token: 0x04000D96 RID: 3478
	private bool used;

	// Token: 0x04000D97 RID: 3479
	private bool firstFrame = true;

	// Token: 0x04000D98 RID: 3480
	public float[] info;

	// Token: 0x04000D99 RID: 3481
	public float[] info2;

	// Token: 0x04000D9A RID: 3482
	public Vector3[] vectors;

	// Token: 0x04000D9B RID: 3483
	public Transform[] transforms;

	// Token: 0x04000D9C RID: 3484
	private Transform[] targetLevers = new Transform[10];

	// Token: 0x04000D9D RID: 3485
	private int[] targetIDs = new int[10];

	// Token: 0x04000D9E RID: 3486
	private int currentID;
}
