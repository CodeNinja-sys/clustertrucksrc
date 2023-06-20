using System;
using UnityEngine;

// Token: 0x020001A5 RID: 421
public static class GameObjectExtensions
{
	// Token: 0x06000971 RID: 2417 RVA: 0x0003B464 File Offset: 0x00039664
	public static void SetAbsoluteScale(this GameObject gameObject, Vector3 absoluteScale)
	{
		Transform transform = gameObject.transform;
		Transform parent = transform.parent;
		transform.parent = null;
		transform.localScale = absoluteScale;
		transform.parent = parent;
	}

	// Token: 0x06000972 RID: 2418 RVA: 0x0003B494 File Offset: 0x00039694
	public static Vector3 GetWorldCenter(this GameObject gameObject)
	{
		Bounds worldSpaceAABB = gameObject.GetWorldSpaceAABB();
		if (worldSpaceAABB.IsValid())
		{
			return worldSpaceAABB.center;
		}
		return gameObject.transform.position;
	}

	// Token: 0x06000973 RID: 2419 RVA: 0x0003B4C8 File Offset: 0x000396C8
	public static void Rotate(this GameObject gameObject, Vector3 rotationAxis, float angleInDegrees, Vector3 pivotPoint)
	{
		Transform transform = gameObject.transform;
		Vector3 vector = transform.position - pivotPoint;
		Quaternion rotation = Quaternion.AngleAxis(angleInDegrees, rotationAxis);
		vector = rotation * vector;
		transform.Rotate(rotationAxis, angleInDegrees, Space.World);
		transform.position = pivotPoint + vector;
	}

	// Token: 0x06000974 RID: 2420 RVA: 0x0003B510 File Offset: 0x00039710
	public static Rect GetScreenRectangle(this GameObject gameObject, Camera camera)
	{
		Bounds worldSpaceAABB = gameObject.GetWorldSpaceAABB();
		if (!worldSpaceAABB.IsValid())
		{
			return new Rect(0f, 0f, 0f, 0f);
		}
		return worldSpaceAABB.GetScreenRectangle(camera);
	}

	// Token: 0x06000975 RID: 2421 RVA: 0x0003B550 File Offset: 0x00039750
	public static Bounds GetWorldSpaceAABB(this GameObject gameObject)
	{
		Bounds modelSpaceAABB = gameObject.GetModelSpaceAABB();
		if (!modelSpaceAABB.IsValid())
		{
			return BoundsExtensions.GetInvalidBoundsInstance();
		}
		return modelSpaceAABB.Transform(gameObject.transform.localToWorldMatrix);
	}

	// Token: 0x06000976 RID: 2422 RVA: 0x0003B588 File Offset: 0x00039788
	public static void SetLayerForEntireHierarchy(this GameObject gameObject, int layer)
	{
		gameObject.layer = layer;
		Transform[] componentsInChildren = gameObject.GetComponentsInChildren<Transform>();
		foreach (Transform transform in componentsInChildren)
		{
			transform.gameObject.layer = layer;
		}
	}

	// Token: 0x06000977 RID: 2423 RVA: 0x0003B5CC File Offset: 0x000397CC
	public static Bounds GetModelSpaceAABB(this GameObject gameObject)
	{
		if (gameObject)
		{
			Bounds modelSpaceMeshAABB = gameObject.GetModelSpaceMeshAABB();
			if (modelSpaceMeshAABB.IsValid())
			{
				return modelSpaceMeshAABB;
			}
		}
		return gameObject.GetModelSpaceColliderAABB();
	}

	// Token: 0x06000978 RID: 2424 RVA: 0x0003B600 File Offset: 0x00039800
	public static Bounds GetModelSpaceColliderAABB(this GameObject gameObject)
	{
		Collider collider = (!gameObject) ? null : gameObject.GetComponent<BoxCollider>();
		if (collider == null)
		{
			collider = ((!gameObject) ? null : gameObject.GetComponent<SphereCollider>());
		}
		if (collider == null)
		{
			collider = ((!gameObject) ? null : gameObject.GetComponent<CapsuleCollider>());
		}
		if (collider != null)
		{
			return new Bounds(Vector3.zero, collider.bounds.size);
		}
		return BoundsExtensions.GetInvalidBoundsInstance();
	}

	// Token: 0x06000979 RID: 2425 RVA: 0x0003B69C File Offset: 0x0003989C
	public static Bounds GetModelSpaceMeshAABB(this GameObject gameObject)
	{
		MeshFilter component = gameObject.GetComponent<MeshFilter>();
		if (component != null && component.sharedMesh != null)
		{
			return component.sharedMesh.bounds;
		}
		SkinnedMeshRenderer component2 = gameObject.GetComponent<SkinnedMeshRenderer>();
		if (component2 != null && component2.sharedMesh != null)
		{
			return component2.localBounds;
		}
		return BoundsExtensions.GetInvalidBoundsInstance();
	}

	// Token: 0x0600097A RID: 2426 RVA: 0x0003B70C File Offset: 0x0003990C
	public static Mesh GetMesh(this GameObject gameObject)
	{
		MeshFilter component = gameObject.GetComponent<MeshFilter>();
		if (component != null && component.sharedMesh != null)
		{
			return component.sharedMesh;
		}
		SkinnedMeshRenderer component2 = gameObject.GetComponent<SkinnedMeshRenderer>();
		if (component2 != null && component2.sharedMesh != null)
		{
			return component2.sharedMesh;
		}
		return null;
	}

	// Token: 0x0600097B RID: 2427 RVA: 0x0003B774 File Offset: 0x00039974
	public static void RemoveAllColliders(this GameObject gameObject)
	{
		Collider[] components = gameObject.GetComponents<Collider>();
		foreach (Collider obj in components)
		{
			UnityEngine.Object.Destroy(obj);
		}
	}

	// Token: 0x0600097C RID: 2428 RVA: 0x0003B7A8 File Offset: 0x000399A8
	public static void DestroyAllChildren(this GameObject gameObject)
	{
		Transform transform = gameObject.transform;
		Transform[] componentsInChildren = gameObject.GetComponentsInChildren<Transform>();
		foreach (Transform transform2 in componentsInChildren)
		{
			if (!(transform == transform2))
			{
				UnityEngine.Object.Destroy(transform2.gameObject);
			}
		}
	}

	// Token: 0x0600097D RID: 2429 RVA: 0x0003B800 File Offset: 0x00039A00
	public static Light[] GetAllLightComponents(this GameObject gameObject)
	{
		return gameObject.GetComponents<Light>();
	}

	// Token: 0x0600097E RID: 2430 RVA: 0x0003B808 File Offset: 0x00039A08
	public static Light GetFirstLightComponent(this GameObject gameObject)
	{
		Light[] allLightComponents = gameObject.GetAllLightComponents();
		if (allLightComponents.Length != 0)
		{
			return allLightComponents[0];
		}
		return null;
	}

	// Token: 0x0600097F RID: 2431 RVA: 0x0003B82C File Offset: 0x00039A2C
	public static ParticleSystem[] GetAllParticleSystemComponents(this GameObject gameObject)
	{
		return gameObject.GetComponents<ParticleSystem>();
	}

	// Token: 0x06000980 RID: 2432 RVA: 0x0003B834 File Offset: 0x00039A34
	public static ParticleSystem GetFirstParticleSystemComponent(this GameObject gameObject)
	{
		ParticleSystem[] allParticleSystemComponents = gameObject.GetAllParticleSystemComponents();
		if (allParticleSystemComponents.Length != 0)
		{
			return allParticleSystemComponents[0];
		}
		return null;
	}
}
