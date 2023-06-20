public class moveMentAbility : AbilityBaseClass
{
	public enum type
	{
		doubleJump = 0,
		dash = 1,
		jetpack = 2,
		levitation = 3,
	}

	public bool loopingSound;
	public type movementType;
}
