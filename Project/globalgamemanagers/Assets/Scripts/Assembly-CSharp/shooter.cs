using UnityEngine;

public class shooter : MonoBehaviour
{
	public float wait;
	public int ammo;
	public float cd;
	public string projectile;
	public float spread;
	public Vector3 rot;
	public removeForceAndLerpBack forceScript;
	public float forceToAllChildren;
	public ParticleSystem[] parts;
	public AudioSource sound;
	public float randomTorque;
	public bool ignoreTruckDisable;
	public bool someRand;
}
