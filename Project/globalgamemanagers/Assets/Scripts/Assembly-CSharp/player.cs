using UnityEngine;

public class player : MonoBehaviour
{
	public LayerMask truckMask;
	public Rigidbody rig;
	public Transform camHolder;
	public bool canMove;
	public bool dead;
	public bool frozen;
	public AudioClip jump;
	public AudioClip boost;
	public AudioSource myAudioSource;
	public AudioSource landSource;
	public AudioSource deathSounds;
	public CameraForce camForce;
	public Animator camAnim;
	public bool hasTouchedGround;
	public LayerMask mask;
	public float framesSinceStart;
	public float jumpCd;
	public AudioSource stepAU;
	public AudioClip[] step;
	public AudioClip[] land;
	public float stepSpeed;
	public CameraMovementRotation camRot;
	public float _xValue;
	public float _yValue;
	public float lastGrounded;
	public bool boosting;
	public bool hasLandedFirstTime;
	public car lastCar;
	public float recentCar;
	public float sinceLastJump;
	public AudioClip[] splat;
	public AudioClip[] burn;
	public AudioClip[] laser;
	public float cantMoveTime;
	public AudioSource windLoop;
}
