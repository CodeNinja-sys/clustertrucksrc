using System;
using System.Collections.Generic;
using RTEditor;
using UnityEngine;

// Token: 0x020001A4 RID: 420
public static class CameraExtensions
{
	// Token: 0x0600096E RID: 2414 RVA: 0x0003B2AC File Offset: 0x000394AC
	public static CameraViewVolume GetViewVolume(this Camera camera)
	{
		CameraViewVolume cameraViewVolume = new CameraViewVolume();
		cameraViewVolume.BuildForCamera(camera);
		return cameraViewVolume;
	}

	// Token: 0x0600096F RID: 2415 RVA: 0x0003B2C8 File Offset: 0x000394C8
	public static List<GameObject> GetVisibleGameObjects(this Camera camera)
	{
		CameraViewVolume cameraViewVolume = new CameraViewVolume();
		cameraViewVolume.BuildForCamera(camera);
		List<GameObject> pottentiallyVisibleGameObjects = camera.GetPottentiallyVisibleGameObjects();
		List<GameObject> list = new List<GameObject>(pottentiallyVisibleGameObjects.Count);
		foreach (GameObject gameObject in pottentiallyVisibleGameObjects)
		{
			if (gameObject.GetComponent<Renderer>())
			{
				if (gameObject.GetComponent<Renderer>().enabled)
				{
					if (!gameObject.GetComponent<World>())
					{
						Bounds worldSpaceAABB = gameObject.GetWorldSpaceAABB();
						if (cameraViewVolume.ContainsWorldSpaceAABB(worldSpaceAABB))
						{
							list.Add(gameObject);
						}
					}
				}
			}
		}
		return list;
	}

	// Token: 0x06000970 RID: 2416 RVA: 0x0003B3A8 File Offset: 0x000395A8
	private static List<GameObject> GetPottentiallyVisibleGameObjects(this Camera camera)
	{
		Transform transform = camera.transform;
		CameraViewVolume viewVolume = camera.GetViewVolume();
		Vector3 vector = transform.position + transform.forward * camera.farClipPlane * 0.5f;
		float radius = (viewVolume.TopLeftPointOnFarPlane - vector).magnitude * 1.01f;
		Collider[] array = Physics.OverlapSphere(vector, radius);
		if (array != null && array.Length != 0)
		{
			List<GameObject> list = new List<GameObject>(array.Length);
			foreach (Collider collider in array)
			{
				list.Add(collider.gameObject);
			}
			return list;
		}
		return new List<GameObject>();
	}
}
