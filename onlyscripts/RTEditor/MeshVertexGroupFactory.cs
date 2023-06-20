using System;
using System.Collections.Generic;
using UnityEngine;

namespace RTEditor
{
	// Token: 0x020001B9 RID: 441
	public static class MeshVertexGroupFactory
	{
		// Token: 0x06000AB1 RID: 2737 RVA: 0x000432A0 File Offset: 0x000414A0
		public static List<MeshVertexGroup> Create(Mesh mesh)
		{
			Bounds bounds = mesh.bounds;
			Vector3 size = bounds.size;
			Vector3[] vertices = mesh.vertices;
			float num = size.x / 2f;
			float num2 = size.y / 2f;
			float num3 = size.z / 2f;
			Vector3 vector = new Vector3(num, num2, num3);
			Vector3 vector2 = vector * 0.5f;
			int num4 = (int)(2f * size.x + 0.5f) + 1;
			int num5 = (int)(2f * size.y + 0.5f) + 1;
			int num6 = (int)(2f * size.z + 0.5f) + 1;
			List<MeshVertexGroup> list = new List<MeshVertexGroup>();
			for (int i = 0; i < num5; i++)
			{
				float y = bounds.min.y + vector2.y + (float)i * num2;
				for (int j = 0; j < num6; j++)
				{
					float z = bounds.min.z + vector2.z + (float)j * num3;
					for (int k = 0; k < num4; k++)
					{
						Vector3 center = new Vector3(bounds.min.x + vector2.x + (float)k * num, y, z);
						Bounds bounds2 = new Bounds(center, vector);
						List<Vector3> list2 = new List<Vector3>(vertices.Length / 2);
						foreach (Vector3 vector3 in vertices)
						{
							if (bounds2.Contains(vector3))
							{
								list2.Add(vector3);
							}
						}
						if (list2.Count != 0)
						{
							MeshVertexGroup item = new MeshVertexGroup(list2);
							list.Add(item);
						}
					}
				}
			}
			return list;
		}
	}
}
