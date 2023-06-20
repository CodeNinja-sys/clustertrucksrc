using System;
using UnityEngine;

namespace RTEditor
{
	// Token: 0x020001B3 RID: 435
	public class ObjectTransformSnapshot
	{
		// Token: 0x06000A5A RID: 2650 RVA: 0x0004089C File Offset: 0x0003EA9C
		public void TakeSnapshot(GameObject gameObject)
		{
			this._gameObject = gameObject;
			Transform transform = gameObject.transform;
			this._absolutePosition = transform.position;
			this._absoluteRotation = transform.rotation;
			this._absoluteScale = transform.lossyScale;
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x000408DC File Offset: 0x0003EADC
		public void ApplySnapshot()
		{
			if (this._gameObject != null)
			{
				Transform transform = this._gameObject.transform;
				transform.position = this._absolutePosition;
				transform.rotation = this._absoluteRotation;
				this._gameObject.SetAbsoluteScale(this._absoluteScale);
			}
		}

		// Token: 0x04000779 RID: 1913
		private GameObject _gameObject;

		// Token: 0x0400077A RID: 1914
		private Vector3 _absolutePosition;

		// Token: 0x0400077B RID: 1915
		private Quaternion _absoluteRotation;

		// Token: 0x0400077C RID: 1916
		private Vector3 _absoluteScale;
	}
}
