using System;
using UnityEngine;

namespace RTEditor
{
	[Serializable]
	public class RotationGizmoSnapSettings
	{
		[SerializeField]
		private bool _isSnappingEnabled;
		[SerializeField]
		private float _stepValueInDegrees;
	}
}
