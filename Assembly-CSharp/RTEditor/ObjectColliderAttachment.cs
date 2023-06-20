using System;
using UnityEngine;

namespace RTEditor
{
	// Token: 0x020001D4 RID: 468
	public class ObjectColliderAttachment : SingletonBase<ObjectColliderAttachment>
	{
		// Token: 0x06000B15 RID: 2837 RVA: 0x00045444 File Offset: 0x00043644
		public void AttachCollidersToAllSceneObjects(ObjectColliderAttachmentSettings colliderAttachmentSettings)
		{
			GameObject[] array = UnityEngine.Object.FindObjectsOfType<GameObject>();
			foreach (GameObject gameObject in array)
			{
				this.AttachColliderToGameObject(gameObject, colliderAttachmentSettings);
			}
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0004547C File Offset: 0x0004367C
		public void AttachCollidersToObjectHierarchy(GameObject hierarchyRoot, ObjectColliderAttachmentSettings colliderAttachmentSettings)
		{
			Transform[] componentsInChildren = hierarchyRoot.GetComponentsInChildren<Transform>(true);
			foreach (Transform transform in componentsInChildren)
			{
				GameObject gameObject = transform.gameObject;
				this.AttachColliderToGameObject(gameObject, colliderAttachmentSettings);
			}
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x000454BC File Offset: 0x000436BC
		private void AttachColliderToGameObject(GameObject gameObject, ObjectColliderAttachmentSettings colliderAttachmentSettings)
		{
			if (gameObject.GetMesh() != null)
			{
				gameObject.RemoveAllColliders();
				this.AttachColliderToMeshObject(gameObject, colliderAttachmentSettings);
			}
			else if (gameObject.GetFirstLightComponent() != null)
			{
				gameObject.RemoveAllColliders();
				this.AttachColliderToLightObject(gameObject, colliderAttachmentSettings);
			}
			else if (gameObject.GetFirstParticleSystemComponent() != null)
			{
				gameObject.RemoveAllColliders();
				this.AttachColliderToParticleSystemObject(gameObject, colliderAttachmentSettings);
			}
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x00045530 File Offset: 0x00043730
		private void AttachColliderToMeshObject(GameObject gameObject, ObjectColliderAttachmentSettings colliderAttachmentSettings)
		{
			if (colliderAttachmentSettings.ColliderTypeForMeshObjects == ObjectColliderType.Box)
			{
				gameObject.AddComponent<BoxCollider>();
			}
			else if (colliderAttachmentSettings.ColliderTypeForMeshObjects == ObjectColliderType.Sphere)
			{
				gameObject.AddComponent<SphereCollider>();
			}
			else if (colliderAttachmentSettings.ColliderTypeForMeshObjects == ObjectColliderType.Capsule)
			{
				gameObject.AddComponent<CapsuleCollider>();
			}
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x00045580 File Offset: 0x00043780
		private void AttachColliderToLightObject(GameObject gameObject, ObjectColliderAttachmentSettings colliderAttachmentSettings)
		{
			if (colliderAttachmentSettings.ColliderTypeForLightObjects == ObjectColliderType.Box)
			{
				BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
				boxCollider.size = colliderAttachmentSettings.BoxColliderSizeForNonMeshObjects;
			}
			else if (colliderAttachmentSettings.ColliderTypeForLightObjects == ObjectColliderType.Sphere)
			{
				SphereCollider sphereCollider = gameObject.AddComponent<SphereCollider>();
				sphereCollider.radius = colliderAttachmentSettings.SphereColliderRadiusForNonMeshObjects;
			}
			else if (colliderAttachmentSettings.ColliderTypeForLightObjects == ObjectColliderType.Capsule)
			{
				CapsuleCollider capsuleCollider = gameObject.AddComponent<CapsuleCollider>();
				capsuleCollider.radius = colliderAttachmentSettings.CapsuleColliderRadiusForNonMeshObjects;
				capsuleCollider.height = colliderAttachmentSettings.CapsuleColliderHeightForNonMeshObjects;
			}
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x00045600 File Offset: 0x00043800
		private void AttachColliderToParticleSystemObject(GameObject gameObject, ObjectColliderAttachmentSettings colliderAttachmentSettings)
		{
			if (colliderAttachmentSettings.ColliderTypeForParticleSystemObjects == ObjectColliderType.Box)
			{
				BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
				boxCollider.size = colliderAttachmentSettings.BoxColliderSizeForNonMeshObjects;
			}
			else if (colliderAttachmentSettings.ColliderTypeForParticleSystemObjects == ObjectColliderType.Sphere)
			{
				SphereCollider sphereCollider = gameObject.AddComponent<SphereCollider>();
				sphereCollider.radius = colliderAttachmentSettings.SphereColliderRadiusForNonMeshObjects;
			}
			else if (colliderAttachmentSettings.ColliderTypeForParticleSystemObjects == ObjectColliderType.Capsule)
			{
				CapsuleCollider capsuleCollider = gameObject.AddComponent<CapsuleCollider>();
				capsuleCollider.radius = colliderAttachmentSettings.CapsuleColliderRadiusForNonMeshObjects;
				capsuleCollider.height = colliderAttachmentSettings.CapsuleColliderHeightForNonMeshObjects;
			}
		}
	}
}
