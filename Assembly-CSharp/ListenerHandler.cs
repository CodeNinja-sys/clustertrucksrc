using System;
using UnityEngine;

// Token: 0x02000244 RID: 580
public class ListenerHandler : MonoBehaviour
{
	// Token: 0x06000E1F RID: 3615 RVA: 0x0005C634 File Offset: 0x0005A834
	private void Start()
	{
		this.man = UnityEngine.Object.FindObjectOfType<Manager>();
	}

	// Token: 0x06000E20 RID: 3616 RVA: 0x0005C644 File Offset: 0x0005A844
	private void Update()
	{
		if (this.player == null)
		{
			if (info.playing)
			{
				player player = UnityEngine.Object.FindObjectOfType<player>();
				if (player != null)
				{
					this.player = player.transform;
				}
			}
		}
		else
		{
			base.transform.position = this.player.position;
			base.transform.rotation = this.player.rotation;
		}
	}

	// Token: 0x04000AC0 RID: 2752
	private Transform player;

	// Token: 0x04000AC1 RID: 2753
	private Manager man;
}
