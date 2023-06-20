using UnityEngine;
using System.Collections.Generic;

public class RoadHandle : MonoBehaviour
{
	public List<RoadHandle> otherRoadhandles;
	[SerializeField]
	public bool _PermanentConnection;
	public Transform connection;
	public bool test;
}
