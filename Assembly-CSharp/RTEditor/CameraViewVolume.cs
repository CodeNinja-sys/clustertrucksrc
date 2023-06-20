using System;
using UnityEngine;

namespace RTEditor
{
	// Token: 0x020001A1 RID: 417
	public class CameraViewVolume
	{
		// Token: 0x170001AC RID: 428
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x0003ACA0 File Offset: 0x00038EA0
		public Vector3[] WorldSpaceVolumePoints
		{
			get
			{
				return this._worldSpaceVolumePoints.Clone() as Vector3[];
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x0600095B RID: 2395 RVA: 0x0003ACB4 File Offset: 0x00038EB4
		public Vector3 TopLeftPointOnNearPlane
		{
			get
			{
				return this._worldSpaceVolumePoints[0];
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x0600095C RID: 2396 RVA: 0x0003ACC8 File Offset: 0x00038EC8
		public Vector3 TopRightPointOnNearPlane
		{
			get
			{
				return this._worldSpaceVolumePoints[1];
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x0003ACDC File Offset: 0x00038EDC
		public Vector3 BottomRightPointOnNearPlane
		{
			get
			{
				return this._worldSpaceVolumePoints[2];
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x0600095E RID: 2398 RVA: 0x0003ACF0 File Offset: 0x00038EF0
		public Vector3 BottomLeftPointOnNearPlane
		{
			get
			{
				return this._worldSpaceVolumePoints[3];
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x0003AD04 File Offset: 0x00038F04
		public Vector3 TopLeftPointOnFarPlane
		{
			get
			{
				return this._worldSpaceVolumePoints[4];
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x0003AD18 File Offset: 0x00038F18
		public Vector3 TopRightPointOnFarPlane
		{
			get
			{
				return this._worldSpaceVolumePoints[5];
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000961 RID: 2401 RVA: 0x0003AD2C File Offset: 0x00038F2C
		public Vector3 BottomRightPointOnFarPlane
		{
			get
			{
				return this._worldSpaceVolumePoints[6];
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x0003AD40 File Offset: 0x00038F40
		public Vector3 BottomLeftPointOnFarPlane
		{
			get
			{
				return this._worldSpaceVolumePoints[7];
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000963 RID: 2403 RVA: 0x0003AD54 File Offset: 0x00038F54
		public Plane[] WorldSpacePlanes
		{
			get
			{
				return this._worldSpacePlanes.Clone() as Plane[];
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x0003AD68 File Offset: 0x00038F68
		public Plane LeftPlane
		{
			get
			{
				return this._worldSpacePlanes[0];
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x0003AD7C File Offset: 0x00038F7C
		public Plane RightPlane
		{
			get
			{
				return this._worldSpacePlanes[1];
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000966 RID: 2406 RVA: 0x0003AD90 File Offset: 0x00038F90
		public Plane BottomPlane
		{
			get
			{
				return this._worldSpacePlanes[2];
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x0003ADA4 File Offset: 0x00038FA4
		public Plane TopPlane
		{
			get
			{
				return this._worldSpacePlanes[3];
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x0003ADB8 File Offset: 0x00038FB8
		public Plane NearPlane
		{
			get
			{
				return this._worldSpacePlanes[4];
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x0003ADCC File Offset: 0x00038FCC
		public Plane FarPlane
		{
			get
			{
				return this._worldSpacePlanes[5];
			}
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0003ADE0 File Offset: 0x00038FE0
		public void BuildForCamera(Camera camera)
		{
			this.IdentifyVolumeWorldSpacePointsForCamera(camera);
			this.CalculateWorldSpacePlanes(camera);
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x0003ADF0 File Offset: 0x00038FF0
		public bool ContainsWorldSpaceAABB(Bounds worldSpaceAABB)
		{
			return GeometryUtility.TestPlanesAABB(this._worldSpacePlanes, worldSpaceAABB);
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x0003AE00 File Offset: 0x00039000
		private void IdentifyVolumeWorldSpacePointsForCamera(Camera camera)
		{
			if (camera == null)
			{
				return;
			}
			Transform transform = camera.transform;
			Vector3 position = transform.position;
			Vector3 right = transform.right;
			Vector3 up = transform.up;
			Vector3 forward = transform.forward;
			if (camera.orthographic)
			{
				float orthographicSize = camera.orthographicSize;
				float d = orthographicSize * camera.aspect;
				this._worldSpaceVolumePoints[0] = position + forward * camera.nearClipPlane - right * d + up * orthographicSize;
				this._worldSpaceVolumePoints[1] = position + forward * camera.nearClipPlane + right * d + up * orthographicSize;
				this._worldSpaceVolumePoints[2] = position + forward * camera.nearClipPlane + right * d - up * orthographicSize;
				this._worldSpaceVolumePoints[3] = position + forward * camera.nearClipPlane - right * d - up * orthographicSize;
				this._worldSpaceVolumePoints[4] = position + forward * camera.farClipPlane - right * d + up * orthographicSize;
				this._worldSpaceVolumePoints[5] = position + forward * camera.farClipPlane - right * d + up * orthographicSize;
				this._worldSpaceVolumePoints[6] = position + forward * camera.farClipPlane - right * d + up * orthographicSize;
				this._worldSpaceVolumePoints[7] = position + forward * camera.farClipPlane - right * d + up * orthographicSize;
			}
			else
			{
				float f = camera.fieldOfView * 0.5f * 0.017453292f;
				float num = Mathf.Tan(f);
				float num2 = num * camera.aspect;
				float num3 = num;
				float d2 = num2 * camera.nearClipPlane;
				float d3 = num2 * camera.farClipPlane;
				float d4 = num3 * camera.nearClipPlane;
				float d5 = num3 * camera.farClipPlane;
				this._worldSpaceVolumePoints[0] = position + forward * camera.nearClipPlane - right * d2 + up * d4;
				this._worldSpaceVolumePoints[1] = position + forward * camera.nearClipPlane + right * d2 + up * d4;
				this._worldSpaceVolumePoints[2] = position + forward * camera.nearClipPlane + right * d2 - up * d4;
				this._worldSpaceVolumePoints[3] = position + forward * camera.nearClipPlane - right * d2 - up * d4;
				this._worldSpaceVolumePoints[4] = position + forward * camera.farClipPlane - right * d3 + up * d5;
				this._worldSpaceVolumePoints[5] = position + forward * camera.farClipPlane + right * d3 + up * d5;
				this._worldSpaceVolumePoints[6] = position + forward * camera.farClipPlane + right * d3 - up * d5;
				this._worldSpaceVolumePoints[7] = position + forward * camera.farClipPlane - right * d3 - up * d5;
			}
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x0003B29C File Offset: 0x0003949C
		private void CalculateWorldSpacePlanes(Camera camera)
		{
			this._worldSpacePlanes = GeometryUtility.CalculateFrustumPlanes(camera);
		}

		// Token: 0x04000700 RID: 1792
		private Vector3[] _worldSpaceVolumePoints = new Vector3[8];

		// Token: 0x04000701 RID: 1793
		private Plane[] _worldSpacePlanes = new Plane[6];
	}
}
