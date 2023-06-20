using System;
using UnityEngine;

// Token: 0x020002CC RID: 716
public class SetPlayerColor : MonoBehaviour
{
	// Token: 0x060010FA RID: 4346 RVA: 0x0006E674 File Offset: 0x0006C874
	private void Start()
	{
	}

	// Token: 0x060010FB RID: 4347 RVA: 0x0006E678 File Offset: 0x0006C878
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
			else if (this.player.transform.position.y + 1f < base.transform.position.y)
			{
				this.done = true;
				this.player.GetComponent<SetScreenColor>().SetColor(this.colorID);
			}
		}
	}

	// Token: 0x04000E16 RID: 3606
	private Transform player;

	// Token: 0x04000E17 RID: 3607
	private bool done;

	// Token: 0x04000E18 RID: 3608
	public int colorID;
}
