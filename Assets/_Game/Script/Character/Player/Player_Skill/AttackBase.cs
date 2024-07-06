using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBase : MonoBehaviour
{
	public GameObject hitVFX;
	[SerializeField] private float damage;
	private void OnTriggerEnter2D(Collider2D collistion)
	{
		if (collistion.tag == "Player" || collistion.tag == "Enemy")
		{
			Character character = collistion.GetComponent<Character>();
			if (character != null)
			{
				character.OnHit(damage);
				GameObject hitVFXInstance = Instantiate(hitVFX, transform.position, transform.rotation);
			}
		}
	}

}
