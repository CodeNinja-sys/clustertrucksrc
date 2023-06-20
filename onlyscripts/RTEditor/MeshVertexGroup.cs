using System;
using System.Collections.Generic;
using UnityEngine;

namespace RTEditor
{
	// Token: 0x020001B8 RID: 440
	public class MeshVertexGroup
	{
		// Token: 0x06000AAD RID: 2733 RVA: 0x00043170 File Offset: 0x00041370
		public MeshVertexGroup(List<Vector3> vertices)
		{
			this._modelSpaceVertices = new List<Vector3>(vertices);
			this.CalculateGroupAABB();
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x00043198 File Offset: 0x00041398
		public Bounds GroupAABB
		{
			get
			{
				return this._groupAABB;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000AAF RID: 2735 RVA: 0x000431A0 File Offset: 0x000413A0
		public List<Vector3> ModelSpaceVertices
		{
			get
			{
				return new List<Vector3>(this._modelSpaceVertices);
			}
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x000431B0 File Offset: 0x000413B0
		private void CalculateGroupAABB()
		{
			Vector3 vector = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
			Vector3 vector2 = new Vector3(float.MinValue, float.MinValue, float.MinValue);
			foreach (Vector3 rhs in this._modelSpaceVertices)
			{
				vector = Vector3.Min(vector, rhs);
				vector2 = Vector3.Max(vector2, rhs);
			}
			this._groupAABB = default(Bounds);
			this._groupAABB.SetMinMax(vector, vector2);
			if (this._groupAABB.size.magnitude < 1E-05f)
			{
				this._groupAABB.size = Vector3.one * 0.3f;
			}
		}

		// Token: 0x040007A1 RID: 1953
		private List<Vector3> _modelSpaceVertices = new List<Vector3>();

		// Token: 0x040007A2 RID: 1954
		private Bounds _groupAABB;
	}
}
