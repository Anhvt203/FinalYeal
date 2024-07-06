using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CointType
{
	Gold
}

public class Coin : MonoBehaviour
{
	public CointType cointType;

	[SerializeField] protected Animator anim;
	[SerializeField] protected SpriteRenderer spr;
	[SerializeField] private Sprite[] CointImg;
	protected virtual void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<Player>() != null)
		{
			PlayerManagement.instance.Coin++;
			Destroy(gameObject);
		}
	}

	public void CointSet(int conIndex)
	{
		for (int i = 0; i < anim.layerCount; i++)
		{
			anim.SetLayerWeight(i, 0);
		}
		anim.SetLayerWeight(conIndex, 1);
	}
}
