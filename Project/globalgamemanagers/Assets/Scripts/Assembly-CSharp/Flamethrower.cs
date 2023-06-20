using UnityEngine;

public class Flamethrower : MonoBehaviour
{
	public bool isOn;
	public bool waiting;
	public Collider collider1;
	public Collider collider2;
	public Collider collider3;
	public ParticleSystem parts;
	public ParticleSystem parts2;
	public ParticleSystem parts3;
	public Light light1;
	public Light light2;
	public float interval;
	public float intervalCounter;
	public bool useInterval;
	public AudioSource au;
}
