using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200013A RID: 314
public class LevelWorldHandler : MonoBehaviour
{
	// Token: 0x060006CC RID: 1740 RVA: 0x0002DC28 File Offset: 0x0002BE28
	private void Start()
	{
	}

	// Token: 0x060006CD RID: 1741 RVA: 0x0002DC2C File Offset: 0x0002BE2C
	private void Update()
	{
		this.worldText.text = "WORLD " + (info.currentWorld + 1).ToString();
		switch (info.currentWorld)
		{
		case 0:
			this.worldTextUnder.text = "DESERT";
			break;
		case 1:
			this.worldTextUnder.text = "FOREST";
			break;
		case 2:
			this.worldTextUnder.text = "WINTER";
			break;
		case 3:
			this.worldTextUnder.text = "LASER";
			break;
		case 4:
			this.worldTextUnder.text = "MEDIEVAL";
			break;
		case 5:
			this.worldTextUnder.text = "ANCIENT";
			break;
		case 6:
			this.worldTextUnder.text = "SCI-FI";
			break;
		case 7:
			this.worldTextUnder.text = "STEAMPUNK";
			break;
		case 8:
			this.worldTextUnder.text = "HELL";
			break;
		}
	}

	// Token: 0x04000501 RID: 1281
	public LevelSeletHandler handler;

	// Token: 0x04000502 RID: 1282
	public Text worldText;

	// Token: 0x04000503 RID: 1283
	public Text worldTextUnder;

	// Token: 0x04000504 RID: 1284
	private float counter1;

	// Token: 0x04000505 RID: 1285
	private float counter2;
}
