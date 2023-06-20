using System;
using UnityEngine;

// Token: 0x020001F4 RID: 500
public class KillOnHeight : MonoBehaviour
{
	// Token: 0x06000BCB RID: 3019 RVA: 0x000491C4 File Offset: 0x000473C4
	private void Start()
	{
	}

	// Token: 0x06000BCC RID: 3020 RVA: 0x000491C8 File Offset: 0x000473C8
	private void Update()
	{
		if (!this.done)
		{
			if (this.player == null)
			{
				try
				{
					this.player = GameObject.Find("player").transform.FindChild("hitbox");
				}
				catch
				{
				}
			}
			else if (this.player.transform.position.y < base.transform.position.y)
			{
				this.done = true;
				this.player.SendMessage("Freeze");
			}
		}
	}

	// Token: 0x06000BCD RID: 3021 RVA: 0x00049284 File Offset: 0x00047484
	public void setPlayerRef(Transform playerRef)
	{
		this.player = playerRef;
	}

	// Token: 0x04000864 RID: 2148
	private Transform player;

	// Token: 0x04000865 RID: 2149
	private bool done;
}
