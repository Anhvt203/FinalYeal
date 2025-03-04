using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCoin : Coin
{
	[SerializeField] private Vector2 speed;
	[SerializeField] protected Color transperentColor;


	protected bool canPickUp;
	protected virtual void Start()
	{
		StartCoroutine(BlinkImage());
	}
	private void Update()
	{
		transform.position += new Vector3(speed.x, speed.y) * Time.deltaTime;
	}
	protected override void OnTriggerEnter2D(Collider2D collision)
	{
		if (canPickUp)
		{
			base.OnTriggerEnter2D(collision);
		}
	}
	protected virtual IEnumerator BlinkImage()
	{
		anim.speed = 0;
		spr.color = transperentColor;

		speed.x *= -1;
		yield return new WaitForSeconds(.1f);
		spr.color = Color.white;

		speed.x *= -1;
		yield return new WaitForSeconds(.1f);
		spr.color = transperentColor;



		speed.x *= -1;
		yield return new WaitForSeconds(.1f);
		spr.color = Color.white;

		speed.x *= -1;
		yield return new WaitForSeconds(.1f);
		spr.color = transperentColor;



		speed.x *= -1;
		yield return new WaitForSeconds(.2f);
		spr.color = Color.white;

		speed.x *= -1;
		yield return new WaitForSeconds(.2f);
		spr.color = transperentColor;

		speed.x *= -1;
		yield return new WaitForSeconds(.3f);
		spr.color = Color.white;

		speed.x *= -1;
		yield return new WaitForSeconds(.3f);
		spr.color = transperentColor;

		speed.x *= -1;
		yield return new WaitForSeconds(.3f);
		spr.color = Color.white;


		speed.x *= -1;
		yield return new WaitForSeconds(.3f);

		speed.x = 0;
		anim.speed = 1;
		canPickUp = true;
	}
}
