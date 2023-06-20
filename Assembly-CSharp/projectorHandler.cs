using System;
using UnityEngine;

// Token: 0x020002A7 RID: 679
public class projectorHandler : MonoBehaviour
{
	// Token: 0x06001038 RID: 4152 RVA: 0x00069BEC File Offset: 0x00067DEC
	private void Start()
	{
		this.proj = base.GetComponent<Projector>();
	}

	// Token: 0x06001039 RID: 4153 RVA: 0x00069BFC File Offset: 0x00067DFC
	private void Update()
	{
		if (this.shrink)
		{
			if (this.proj.orthographicSize > 0f)
			{
				this.proj.orthographicSize -= Time.deltaTime * this.shrinkSpeed;
			}
			else
			{
				this.proj.orthographicSize = 0f;
			}
		}
	}

	// Token: 0x04000D39 RID: 3385
	private Projector proj;

	// Token: 0x04000D3A RID: 3386
	public bool shrink;

	// Token: 0x04000D3B RID: 3387
	public float shrinkSpeed = 0.2f;
}
