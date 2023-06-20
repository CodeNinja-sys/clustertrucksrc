using System;
using UnityEngine;

// Token: 0x020002E3 RID: 739
public class SpawnObjects : MonoBehaviour
{
	// Token: 0x0600115D RID: 4445 RVA: 0x00070B30 File Offset: 0x0006ED30
	private void Start()
	{
		this.SpawnTimer = 0f;
		this.SpawnCounter = 0;
	}

	// Token: 0x0600115E RID: 4446 RVA: 0x00070B44 File Offset: 0x0006ED44
	private void Update()
	{
		if ((float)this.SpawnCounter < this.NumObjects)
		{
			this.SpawnTimer -= Time.deltaTime;
			if (this.SpawnTimer < 0f)
			{
				UnityEngine.Object.Instantiate(this.ObjectToSpawn, base.transform.position, base.transform.rotation);
				this.SpawnTimer = this.Interval;
				this.SpawnCounter++;
			}
		}
	}

	// Token: 0x04000E79 RID: 3705
	public GameObject ObjectToSpawn;

	// Token: 0x04000E7A RID: 3706
	public float Interval;

	// Token: 0x04000E7B RID: 3707
	public float NumObjects;

	// Token: 0x04000E7C RID: 3708
	private float SpawnTimer;

	// Token: 0x04000E7D RID: 3709
	private int SpawnCounter;
}
