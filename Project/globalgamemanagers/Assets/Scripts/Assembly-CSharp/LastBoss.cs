using UnityEngine;

public class LastBoss : MonoBehaviour
{
	public ParticleSystem fire;
	public ParticleSystem sparks;
	public ParticleSystem angryFire;
	public Collider fireCol;
	public Animator leftArm;
	public Animator rightArm;
	public RotationFix rotFix;
	public Transform rightArmRoot;
	public Transform lavaFloor;
	public Collider trigger;
	public Collider arm;
	public Collider hit;
	public Collider truckRemover;
	public Collider playerKiller;
	public GameObject lasers;
	public GameObject credits;
	public GameObject theRest;
	public Animator ButtonMouth;
	public GameObject endExplosion;
	[SerializeField]
	private AudioSource mMusicSource;
	[SerializeField]
	private AudioSource mBossScreamSource;
	[SerializeField]
	private AudioSource mAttackSource;
	[SerializeField]
	private AudioSource mAttackFlameSource;
	[SerializeField]
	private AudioSource mBackFireAudioSource;
	[SerializeField]
	private AudioClip[] mScreamClips;
	[SerializeField]
	private AudioClip[] mIdleClips;
	[SerializeField]
	private AudioClip mDeathClip;
	[SerializeField]
	private AudioClip mMusicIntro;
	[SerializeField]
	private AudioClip mMusicLoop;
	[SerializeField]
	private AudioClip mBossPunchClip;
	[SerializeField]
	private AudioClip mBossFlameClip;
	[SerializeField]
	private AudioClip mBossGroanClip;
	[SerializeField]
	private AudioClip mBossDamageClip;
	[SerializeField]
	private AudioClip mBossRecoverClip;
}
