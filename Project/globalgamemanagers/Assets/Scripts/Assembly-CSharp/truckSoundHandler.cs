using UnityEngine;

public class truckSoundHandler : MonoBehaviour
{
	public AudioSource au;
	public AudioSource loopSource;
	public AudioClip[] engineSounds;
	public AudioClip honk1;
	public AudioClip honk2;
	public AudioClip doubleHonk1;
	public AudioClip doubleHonk2;
	public AudioClip longHonk1;
	public AudioClip longHonk2;
	public AudioClip impact1;
	public AudioClip impact2;
	public AudioClip impact3;
	public carCheckDamage carDamageFront;
	public carCheckDamage carDamageLoad;
	public Rigidbody frontRig;
	public Rigidbody loadRig;
}
