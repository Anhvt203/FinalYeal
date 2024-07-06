using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
	[SerializeField] private Transform resPoint;
	private void Awake()
	{
		PlayerManagement.instance.respawnPoint = transform;
		PlayerManagement.instance.PlayerRes();
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.GetComponent<Player>()!= null)
		{
			if (!GameManager.instance.startTime)
			{
				GameManager.instance.startTime = true;
			}
			if (collision.GetComponent<Player>().IsDead)
			{
				PlayerManagement.instance.respawnPoint = resPoint;
			}
			GetComponent<BoxCollider2D>().enabled = false;
		}
	}

}
