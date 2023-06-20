using System;
using UnityEngine;

// Token: 0x020001DE RID: 478
public abstract class AbilityBaseClass : MonoBehaviour
{
	// Token: 0x06000B50 RID: 2896
	public abstract void Go();

	// Token: 0x06000B51 RID: 2897
	public abstract void Stop();

	// Token: 0x06000B52 RID: 2898
	public abstract string getToolTip();

	// Token: 0x06000B53 RID: 2899
	public abstract string getToolTipController();

	// Token: 0x040007F5 RID: 2037
	public AbilityBaseClass.type myType;

	// Token: 0x040007F6 RID: 2038
	public bool canHold;

	// Token: 0x040007F7 RID: 2039
	public float useTime = 2f;

	// Token: 0x040007F8 RID: 2040
	public float extraAirTime;

	// Token: 0x040007F9 RID: 2041
	public float rechargeTime = 1f;

	// Token: 0x040007FA RID: 2042
	public float overTime;

	// Token: 0x040007FB RID: 2043
	public int charges = 3;

	// Token: 0x040007FC RID: 2044
	public bool hasToBeGrounded;

	// Token: 0x040007FD RID: 2045
	public GameObject uiStuff;

	// Token: 0x040007FE RID: 2046
	public AudioClip goSound;

	// Token: 0x040007FF RID: 2047
	[SerializeField]
	protected string _toolTip;

	// Token: 0x04000800 RID: 2048
	[SerializeField]
	protected string _toolTipController;

	// Token: 0x020001DF RID: 479
	public enum type
	{
		// Token: 0x04000802 RID: 2050
		energy,
		// Token: 0x04000803 RID: 2051
		charges,
		// Token: 0x04000804 RID: 2052
		singleUse,
		// Token: 0x04000805 RID: 2053
		cooldown,
		// Token: 0x04000806 RID: 2054
		oncePerJump,
		// Token: 0x04000807 RID: 2055
		other,
		// Token: 0x04000808 RID: 2056
		replaceJump
	}
}
