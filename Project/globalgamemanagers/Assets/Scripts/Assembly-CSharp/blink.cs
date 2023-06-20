using UnityEngine;

public class blink : MonoBehaviour
{
	public enum type
	{
		hard = 0,
		lerp = 1,
		curve = 2,
	}

	public Color c1;
	public Color c2;
	public float speed;
	public type currentType;
}
