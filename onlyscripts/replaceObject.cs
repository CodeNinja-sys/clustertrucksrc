using System;
using UnityEngine;

// Token: 0x020002AC RID: 684
public class replaceObject : MonoBehaviour
{
	// Token: 0x0600104D RID: 4173 RVA: 0x0006A52C File Offset: 0x0006872C
	private void Awake()
	{
		if (base.gameObject.name.Contains("goal"))
		{
			this.ObjectName = "goal";
		}
	}

	// Token: 0x0600104E RID: 4174 RVA: 0x0006A554 File Offset: 0x00068754
	private void Update()
	{
		this.counter += Time.deltaTime;
		if ((!this.used && !this.waitSpawn) || (this.waitSpawn && this.counter > this.waitStart))
		{
			this.Replace();
			this.used = true;
		}
		this.firstFrame = false;
	}

	// Token: 0x0600104F RID: 4175 RVA: 0x0006A5BC File Offset: 0x000687BC
	private void Replace()
	{
		Vector3 position = base.transform.position;
		float num = -30000f;
		foreach (RaycastHit raycastHit in Physics.RaycastAll(base.transform.position, Vector3.down, 25f))
		{
			if (raycastHit.point != Vector3.zero && raycastHit.transform.tag == "kill" && this.ObjectName != "goal")
			{
				if (raycastHit.point.y + 2.4f + Vector3.Angle(Vector3.up, raycastHit.normal) / 10f > num)
				{
					num = raycastHit.point.y + 2.4f + Vector3.Angle(Vector3.up, raycastHit.normal) / 10f;
				}
				position = new Vector3(base.transform.position.x, num, base.transform.position.z);
			}
		}
		GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(Resources.Load(this.ObjectName), position, base.transform.rotation);
		if (this.setParent)
		{
			gameObject.transform.parent = base.transform.parent;
		}
		else
		{
			gameObject.transform.parent = null;
		}
		if (this.waitStart > 0f && !this.waitSpawn)
		{
			gameObject.SendMessage("Wait", this.waitStart);
		}
		if (this.extraWait > 0f)
		{
			gameObject.SendMessage("Wait", this.extraWait);
		}
		if (this.ObjectName == "truck")
		{
			car component = gameObject.GetComponent<car>();
			component.setType(this.type);
			if (info.truckWidth != 1f)
			{
				component.mainRig.transform.localScale = new Vector3(info.truckWidth, 1f, 1f);
				component.secondRig.transform.localScale = new Vector3(info.truckWidth, 1f, 1f);
			}
		}
		if (this.goalTruck)
		{
			gameObject.transform.FindChild("load").GetComponentInChildren<goal>(true).gameObject.SetActive(true);
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x04000D53 RID: 3411
	public string ObjectName = "truck";

	// Token: 0x04000D54 RID: 3412
	public bool setParent;

	// Token: 0x04000D55 RID: 3413
	private LayerMask mask;

	// Token: 0x04000D56 RID: 3414
	private bool used;

	// Token: 0x04000D57 RID: 3415
	private bool firstFrame = true;

	// Token: 0x04000D58 RID: 3416
	public float waitStart;

	// Token: 0x04000D59 RID: 3417
	public float extraWait;

	// Token: 0x04000D5A RID: 3418
	public int type;

	// Token: 0x04000D5B RID: 3419
	public bool goalTruck;

	// Token: 0x04000D5C RID: 3420
	public bool waitSpawn;

	// Token: 0x04000D5D RID: 3421
	private float counter;
}
