using System;
using UnityEngine;

namespace RTEditor
{
	[Serializable]
	public class ObjectColliderAttachmentSettings
	{
		[SerializeField]
		private ObjectColliderType _colliderTypeForMeshObjects;
		[SerializeField]
		private ObjectColliderType _colliderTypeForLightObjects;
		[SerializeField]
		private ObjectColliderType _colliderTypeForParticleSystemObjects;
		[SerializeField]
		private Vector3 _boxColliderSizeForNonMeshObjects;
		[SerializeField]
		private float _sphereColliderRadiusForNonMeshObjects;
		[SerializeField]
		private float _capsuleColliderRadiusForNonMeshObjects;
		[SerializeField]
		private float _capsuleColliderHeightForNonMeshObjects;
	}
}
