using System;
using UnityEngine;

namespace RTEditor
{
	[Serializable]
	public class ScaleGizmoSnapSettings
	{
		[SerializeField]
		private bool _isSnappingEnabled;
		[SerializeField]
		private float _stepValueInWorldUnits;
	}
}
