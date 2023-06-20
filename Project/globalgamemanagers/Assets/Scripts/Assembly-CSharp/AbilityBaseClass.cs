using UnityEngine;

public class AbilityBaseClass : MonoBehaviour
{
	public enum type
	{
		energy = 0,
		charges = 1,
		singleUse = 2,
		cooldown = 3,
		oncePerJump = 4,
		other = 5,
		replaceJump = 6,
	}

	public type myType;
	public bool canHold;
	public float useTime;
	public float extraAirTime;
	public float rechargeTime;
	public float overTime;
	public int charges;
	public bool hasToBeGrounded;
	public GameObject uiStuff;
	public AudioClip goSound;
	[SerializeField]
	protected string _toolTip;
	[SerializeField]
	protected string _toolTipController;
}
