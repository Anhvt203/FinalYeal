using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
	private InGameUI inGameUI;
	private void Start()
	{
		inGameUI = GameObject.Find("Canvas").GetComponent<InGameUI>();
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<Player>() != null)
		{
			GetComponent<Animator>().SetTrigger("actived");

			Destroy(collision.gameObject);

			inGameUI.OnLvFinished();

			GameManager.instance.SaveTime();
			GameManager.instance.SaveCoin();
			GameManager.instance.SaveMap();
		}
	}
	
}
