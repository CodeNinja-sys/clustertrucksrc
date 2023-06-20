using System;
using System.Collections.Generic;
using UnityEngine;

namespace RTEditor
{
	// Token: 0x020001BA RID: 442
	public class MeshVertexGroupMappings : SingletonBase<MeshVertexGroupMappings>
	{
		// Token: 0x06000AB3 RID: 2739 RVA: 0x000434A0 File Offset: 0x000416A0
		public bool CreateMappingForMesh(Mesh mesh)
		{
			if (mesh == null)
			{
				return false;
			}
			if (this._meshToVertexGroups.ContainsKey(mesh))
			{
				this._meshToVertexGroups.Remove(mesh);
			}
			List<MeshVertexGroup> list = MeshVertexGroupFactory.Create(mesh);
			if (list.Count != 0)
			{
				this._meshToVertexGroups.Add(mesh, list);
				return true;
			}
			return false;
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x000434FC File Offset: 0x000416FC
		public void CreateMappingsForAllSceneMeshObjects()
		{
			GameObject[] array = UnityEngine.Object.FindObjectsOfType<GameObject>();
			foreach (GameObject gameObject in array)
			{
				Mesh mesh = gameObject.GetMesh();
				if (mesh != null)
				{
					this.CreateMappingForMesh(mesh);
				}
			}
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x00043548 File Offset: 0x00041748
		public List<MeshVertexGroup> GetMeshVertexGroups(Mesh mesh)
		{
			List<MeshVertexGroup> result = new List<MeshVertexGroup>();
			if (this._meshToVertexGroups.ContainsKey(mesh))
			{
				return new List<MeshVertexGroup>(this._meshToVertexGroups[mesh]);
			}
			return result;
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00043580 File Offset: 0x00041780
		public bool ContainsMappingForMesh(Mesh mesh)
		{
			return mesh != null && this._meshToVertexGroups.ContainsKey(mesh);
		}

		// Token: 0x040007A3 RID: 1955
		private Dictionary<Mesh, List<MeshVertexGroup>> _meshToVertexGroups = new Dictionary<Mesh, List<MeshVertexGroup>>();
	}
}
