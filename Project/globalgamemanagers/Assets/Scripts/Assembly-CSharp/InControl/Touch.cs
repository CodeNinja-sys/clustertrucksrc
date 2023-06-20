using UnityEngine;

namespace InControl
{
	public class Touch
	{
		internal Touch(int fingerId)
		{
		}

		public int fingerId;
		public TouchPhase phase;
		public int tapCount;
		public Vector2 position;
		public Vector2 deltaPosition;
		public Vector2 lastPosition;
		public float deltaTime;
		public ulong updateTick;
	}
}
