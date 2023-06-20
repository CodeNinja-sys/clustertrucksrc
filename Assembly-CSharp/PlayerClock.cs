using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000178 RID: 376
public class PlayerClock : MonoBehaviour
{
	// Token: 0x06000870 RID: 2160 RVA: 0x00037B40 File Offset: 0x00035D40
	public void Flush()
	{
		this._NameText.text = string.Empty;
		this._TimeText.text = string.Empty;
	}

	// Token: 0x06000871 RID: 2161 RVA: 0x00037B70 File Offset: 0x00035D70
	public void SetTimeText(string val)
	{
		this._TimeText.text = val;
		this._NameText.text = "TIME";
	}

	// Token: 0x040006B4 RID: 1716
	[SerializeField]
	private Text _NameText;

	// Token: 0x040006B5 RID: 1717
	[SerializeField]
	private Text _TimeText;
}
