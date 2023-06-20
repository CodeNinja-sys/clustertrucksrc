using System;
using UnityEngine;

// Token: 0x02000265 RID: 613
public class addForceToTag : MonoBehaviour
{
	// Token: 0x06000EDD RID: 3805 RVA: 0x00060468 File Offset: 0x0005E668
	public void sendInfo(float[] info)
	{
		this.forwardForce = info[0];
		if (info.Length > 1 && info[1] > 0f)
		{
			this.once = true;
		}
	}

	// Token: 0x06000EDE RID: 3806 RVA: 0x0006049C File Offset: 0x0005E69C
	private void OnTriggerEnter(Collider other)
	{
		if ((other.tag == this.targetTag || this.targetTag == string.Empty) && this.once && !other.name.Contains("not"))
		{
			Rigidbody component = other.GetComponent<Rigidbody>();
			if (component)
			{
				component.velocity = new Vector3(0f, 0f, 0f);
				float d = 1f;
				component.velocity = base.transform.forward * this.forwardForce * d;
				if (component.tag == "Player")
				{
					component.gameObject.GetComponent<player>().setBoost();
				}
			}
			else
			{
				component = other.transform.parent.GetComponent<Rigidbody>();
				if (!component)
				{
					return;
				}
				component.GetComponent<carCheckDamage>().SetImmunity(true);
				component.velocity = new Vector3(0f, 0f, 0f);
				component.AddForce(base.transform.forward * this.forwardForce * 0.8f, ForceMode.VelocityChange);
				component.drag = 0.1f;
			}
		}
	}

	// Token: 0x06000EDF RID: 3807 RVA: 0x000605E8 File Offset: 0x0005E7E8
	private void OnTriggerStay(Collider other)
	{
		if ((other.tag == this.targetTag || this.targetTag == string.Empty) && !other.name.Contains("not") && !this.once)
		{
			float d = 1f;
			Rigidbody component = other.GetComponent<Rigidbody>();
			if (!component)
			{
				component = other.transform.parent.GetComponent<Rigidbody>();
			}
			if (this.toPoint)
			{
				component.AddForce(Vector3.Normalize(other.transform.position - this.toPoint.position) * this.forwardForce * Time.deltaTime * d, ForceMode.Acceleration);
				component.velocity = Vector3.Lerp(component.velocity, Vector3.zero, Time.deltaTime);
			}
			else
			{
				component.AddForce(base.transform.forward * this.forwardForce * Time.deltaTime * d, ForceMode.Force);
			}
		}
	}

	// Token: 0x04000B7D RID: 2941
	public string targetTag = string.Empty;

	// Token: 0x04000B7E RID: 2942
	public Transform toPoint;

	// Token: 0x04000B7F RID: 2943
	public bool once;

	// Token: 0x04000B80 RID: 2944
	public float forwardForce;
}
