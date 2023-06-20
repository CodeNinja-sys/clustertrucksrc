using System;
using UnityEngine;

// Token: 0x02000260 RID: 608
public class abilityInfo : MonoBehaviour
{
	// Token: 0x1700029C RID: 668
	// (get) Token: 0x06000EC1 RID: 3777 RVA: 0x0005EF3C File Offset: 0x0005D13C
	public int AbilityCost
	{
		get
		{
			return this._cost;
		}
	}

	// Token: 0x1700029D RID: 669
	// (get) Token: 0x06000EC2 RID: 3778 RVA: 0x0005EF44 File Offset: 0x0005D144
	public bool Unlocked
	{
		get
		{
			return this._unlocked;
		}
	}

	// Token: 0x06000EC3 RID: 3779 RVA: 0x0005EF4C File Offset: 0x0005D14C
	private void Awake()
	{
		this.CheckMe();
	}

	// Token: 0x06000EC4 RID: 3780 RVA: 0x0005EF54 File Offset: 0x0005D154
	private void CheckMe()
	{
		if (PlayerPrefs.GetInt(base.gameObject.name) == 1)
		{
			this._unlocked = true;
		}
	}

	// Token: 0x06000EC5 RID: 3781 RVA: 0x0005EF74 File Offset: 0x0005D174
	public void UnlockMe()
	{
		Debug.Log("Unlocked!");
		PlayerPrefs.SetInt(base.gameObject.name, 1);
		this._unlocked = true;
	}

	// Token: 0x04000B4C RID: 2892
	[SerializeField]
	private int _cost;

	// Token: 0x04000B4D RID: 2893
	public string[] infoField = new string[3];

	// Token: 0x04000B4E RID: 2894
	public bool movement = true;

	// Token: 0x04000B4F RID: 2895
	[SerializeField]
	private bool _unlocked;
}
