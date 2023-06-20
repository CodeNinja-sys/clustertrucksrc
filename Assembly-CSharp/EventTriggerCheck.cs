using System;
using UnityEngine;

// Token: 0x020001EC RID: 492
public class EventTriggerCheck : MonoBehaviour
{
	// Token: 0x06000B98 RID: 2968 RVA: 0x00048178 File Offset: 0x00046378
	public void Init(ObjectEventHandler parent)
	{
		this._parentHandler = parent;
	}

	// Token: 0x06000B99 RID: 2969 RVA: 0x00048184 File Offset: 0x00046384
	private void OnCollisionEnter(Collision col)
	{
		Debug.Log("EventTriggerCheck: " + this._parentHandler.gameObject.name + " Collision: " + col.gameObject.name, col.gameObject);
		if (col.gameObject.GetComponent<player>())
		{
			base.StartCoroutine(this._parentHandler.FireEventsFor(EventToolLogic.EventTypes.OnPlayerTouch, null));
		}
		else if (col.transform.root.GetComponent<car>())
		{
			base.StartCoroutine(this._parentHandler.FireEventsFor(EventToolLogic.EventTypes.OnTruckTouch, null));
		}
	}

	// Token: 0x06000B9A RID: 2970 RVA: 0x00048224 File Offset: 0x00046424
	private void OnCollisionExit(Collision col)
	{
	}

	// Token: 0x06000B9B RID: 2971 RVA: 0x00048228 File Offset: 0x00046428
	private void OnCollisionStay(Collision col)
	{
	}

	// Token: 0x06000B9C RID: 2972 RVA: 0x0004822C File Offset: 0x0004642C
	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("EventTriggerCheck: " + this._parentHandler.gameObject.name + " Collision: " + other.gameObject.name, other.gameObject);
		if (other.gameObject.tag == "Player")
		{
			base.StartCoroutine(this._parentHandler.FireEventsFor(EventToolLogic.EventTypes.OnPlayerTouch, null));
		}
		else if (other.transform.root.GetComponent<car>())
		{
			base.StartCoroutine(this._parentHandler.FireEventsFor(EventToolLogic.EventTypes.OnTruckTouch, other.transform.root.gameObject));
		}
	}

	// Token: 0x06000B9D RID: 2973 RVA: 0x000482E0 File Offset: 0x000464E0
	private void OnTriggerStay(Collider other)
	{
	}

	// Token: 0x06000B9E RID: 2974 RVA: 0x000482E4 File Offset: 0x000464E4
	private void OnTriggerExit(Collider other)
	{
	}

	// Token: 0x06000B9F RID: 2975 RVA: 0x000482E8 File Offset: 0x000464E8
	public void OnPlayerTouch()
	{
		Debug.Log("OnPlayerTouch!");
		base.StartCoroutine(this._parentHandler.FireEventsFor(EventToolLogic.EventTypes.OnPlayerTouch, null));
	}

	// Token: 0x06000BA0 RID: 2976 RVA: 0x00048308 File Offset: 0x00046508
	public void OnTruckTouch()
	{
		Debug.Log("OnTruckTouch!");
		this._parentHandler.FireEventsFor(EventToolLogic.EventTypes.OnTruckTouch, null);
	}

	// Token: 0x0400082C RID: 2092
	private ObjectEventHandler _parentHandler;
}
