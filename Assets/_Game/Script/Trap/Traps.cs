using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{
	protected virtual void OnTriggerEnter2D(Collider2D collision)
	{
		Player player = collision.GetComponent<Player>();
		if (player != null)
		{
			Debug.Log("Player hit by trap");
			player.OnHit(30f);
			player.HandleTrapCollision();
		}
	}
}
