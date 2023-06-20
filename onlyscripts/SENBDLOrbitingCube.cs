using System;
using UnityEngine;

// Token: 0x020002D6 RID: 726
public class SENBDLOrbitingCube : MonoBehaviour
{
	// Token: 0x0600111D RID: 4381 RVA: 0x0006F24C File Offset: 0x0006D44C
	private Vector3 Vec3(float x)
	{
		return new Vector3(x, x, x);
	}

	// Token: 0x0600111E RID: 4382 RVA: 0x0006F258 File Offset: 0x0006D458
	private void Start()
	{
		this.transf = base.transform;
		this.rotationVector = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
		this.rotationVector = Vector3.Normalize(this.rotationVector);
		this.spherePosition = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
		this.spherePosition = Vector3.Normalize(this.spherePosition);
		this.spherePosition *= UnityEngine.Random.Range(16.5f, 18f);
		this.randomSphereRotation = new Vector3(UnityEngine.Random.Range(-1.1f, 1f), UnityEngine.Random.Range(0f, 0.1f), UnityEngine.Random.Range(0.5f, 1f));
		this.randomSphereRotation = Vector3.Normalize(this.randomSphereRotation);
		this.sphereRotationSpeed = UnityEngine.Random.Range(10f, 20f);
		this.rotationSpeed = UnityEngine.Random.Range(1f, 90f);
		this.transf.localScale = this.Vec3(UnityEngine.Random.Range(1f, 2f));
	}

	// Token: 0x0600111F RID: 4383 RVA: 0x0006F3B8 File Offset: 0x0006D5B8
	private void Update()
	{
		Quaternion rotation = Quaternion.Euler(this.randomSphereRotation * Time.time * this.sphereRotationSpeed);
		Vector3 vector = rotation * this.spherePosition;
		vector += this.spherePosition * (Mathf.Sin(Time.time - this.spherePosition.magnitude / 10f) * 0.5f + 0.5f);
		vector += rotation * this.spherePosition * (Mathf.Sin(Time.time * 3.1415265f / 4f - this.spherePosition.magnitude / 10f) * 0.5f + 0.5f);
		this.transf.position = vector;
		this.transf.rotation = Quaternion.Euler(this.rotationVector * Time.time * this.rotationSpeed);
	}

	// Token: 0x04000E3C RID: 3644
	private Transform transf;

	// Token: 0x04000E3D RID: 3645
	private Vector3 rotationVector;

	// Token: 0x04000E3E RID: 3646
	private float rotationSpeed;

	// Token: 0x04000E3F RID: 3647
	private Vector3 spherePosition;

	// Token: 0x04000E40 RID: 3648
	private Vector3 randomSphereRotation;

	// Token: 0x04000E41 RID: 3649
	private float sphereRotationSpeed;
}
