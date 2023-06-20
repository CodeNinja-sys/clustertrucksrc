using System;
using UnityEngine;

namespace RTEditor
{
	// Token: 0x020001D5 RID: 469
	[Serializable]
	public class ObjectColliderAttachmentSettings
	{
		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000B1C RID: 2844 RVA: 0x000456C0 File Offset: 0x000438C0
		public static Vector3 MinBoxColliderSizeForNonMeshObjects
		{
			get
			{
				return Vector3.one * 0.1f;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x000456D4 File Offset: 0x000438D4
		public static float MinSphereColliderRadiusForNonMeshObjects
		{
			get
			{
				return 0.1f;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x000456DC File Offset: 0x000438DC
		public static float MinCapsuleColliderRadiusForNonMeshObjects
		{
			get
			{
				return 0.1f;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x000456E4 File Offset: 0x000438E4
		public static float MinCapsuleColliderHeightForNonMeshObjects
		{
			get
			{
				return 0.1f;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000B20 RID: 2848 RVA: 0x000456EC File Offset: 0x000438EC
		// (set) Token: 0x06000B21 RID: 2849 RVA: 0x000456F4 File Offset: 0x000438F4
		public ObjectColliderType ColliderTypeForMeshObjects
		{
			get
			{
				return this._colliderTypeForMeshObjects;
			}
			set
			{
				this._colliderTypeForMeshObjects = value;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x00045700 File Offset: 0x00043900
		// (set) Token: 0x06000B23 RID: 2851 RVA: 0x00045708 File Offset: 0x00043908
		public ObjectColliderType ColliderTypeForLightObjects
		{
			get
			{
				return this._colliderTypeForLightObjects;
			}
			set
			{
				this._colliderTypeForLightObjects = value;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x00045714 File Offset: 0x00043914
		// (set) Token: 0x06000B25 RID: 2853 RVA: 0x0004571C File Offset: 0x0004391C
		public ObjectColliderType ColliderTypeForParticleSystemObjects
		{
			get
			{
				return this._colliderTypeForParticleSystemObjects;
			}
			set
			{
				this._colliderTypeForParticleSystemObjects = value;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x00045728 File Offset: 0x00043928
		// (set) Token: 0x06000B27 RID: 2855 RVA: 0x00045730 File Offset: 0x00043930
		public Vector3 BoxColliderSizeForNonMeshObjects
		{
			get
			{
				return this._boxColliderSizeForNonMeshObjects;
			}
			set
			{
				this._boxColliderSizeForNonMeshObjects = Vector3.Max(value, ObjectColliderAttachmentSettings.MinBoxColliderSizeForNonMeshObjects);
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000B28 RID: 2856 RVA: 0x00045744 File Offset: 0x00043944
		// (set) Token: 0x06000B29 RID: 2857 RVA: 0x0004574C File Offset: 0x0004394C
		public float SphereColliderRadiusForNonMeshObjects
		{
			get
			{
				return this._sphereColliderRadiusForNonMeshObjects;
			}
			set
			{
				this._sphereColliderRadiusForNonMeshObjects = Mathf.Max(value, ObjectColliderAttachmentSettings.MinSphereColliderRadiusForNonMeshObjects);
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000B2A RID: 2858 RVA: 0x00045760 File Offset: 0x00043960
		// (set) Token: 0x06000B2B RID: 2859 RVA: 0x00045768 File Offset: 0x00043968
		public float CapsuleColliderRadiusForNonMeshObjects
		{
			get
			{
				return this._capsuleColliderRadiusForNonMeshObjects;
			}
			set
			{
				this._capsuleColliderRadiusForNonMeshObjects = Mathf.Max(value, ObjectColliderAttachmentSettings.MinCapsuleColliderRadiusForNonMeshObjects);
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000B2C RID: 2860 RVA: 0x0004577C File Offset: 0x0004397C
		// (set) Token: 0x06000B2D RID: 2861 RVA: 0x00045784 File Offset: 0x00043984
		public float CapsuleColliderHeightForNonMeshObjects
		{
			get
			{
				return this._capsuleColliderHeightForNonMeshObjects;
			}
			set
			{
				this._capsuleColliderHeightForNonMeshObjects = Mathf.Max(value, ObjectColliderAttachmentSettings.MinCapsuleColliderHeightForNonMeshObjects);
			}
		}

		// Token: 0x040007D3 RID: 2003
		[SerializeField]
		private ObjectColliderType _colliderTypeForMeshObjects;

		// Token: 0x040007D4 RID: 2004
		[SerializeField]
		private ObjectColliderType _colliderTypeForLightObjects;

		// Token: 0x040007D5 RID: 2005
		[SerializeField]
		private ObjectColliderType _colliderTypeForParticleSystemObjects;

		// Token: 0x040007D6 RID: 2006
		[SerializeField]
		private Vector3 _boxColliderSizeForNonMeshObjects = Vector3.one;

		// Token: 0x040007D7 RID: 2007
		[SerializeField]
		private float _sphereColliderRadiusForNonMeshObjects = 1f;

		// Token: 0x040007D8 RID: 2008
		[SerializeField]
		private float _capsuleColliderRadiusForNonMeshObjects = 1f;

		// Token: 0x040007D9 RID: 2009
		[SerializeField]
		private float _capsuleColliderHeightForNonMeshObjects = 1f;
	}
}
