using UnityEngine;

public class car : MonoBehaviour
{
	public Rigidbody mainRig;
	public Rigidbody secondRig;
	public bool hasFallen;
	public AudioSource engine;
	public float speedMultiplier;
	public float turnMultiplier;
	public bool tankMovement;
	public Vector3 myVelocity;
	public float lastGrounded;
	public float waitTime;
	public float electricity;
	public Renderer rend1;
	public Renderer rend2;
	public Color electricityColor;
	public ParticleSystem electricityParts;
	public GameObject killField;
	public GameObject breakObject;
}
