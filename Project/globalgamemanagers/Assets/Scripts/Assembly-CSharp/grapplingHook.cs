using UnityEngine;

public class grapplingHook : AbilityBaseClass
{
	public Transform hook;
	public Rigidbody hitBoxRig;
	public LineRenderer line;
	public ParticleSystem parts;
	public GameObject spinner;
	public abilityManager man;
	public AudioSource auSource;
	public AudioSource inLoop;
	public AudioClip hitSound;
	public LayerMask mask;
}
