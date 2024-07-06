using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Suriken : Traps
{
    private Animator anim;
	private int movePointIndex;
	private float cooldownTime;

	[SerializeField] private float cooldown;
	[SerializeField] private float speed;
	[SerializeField] private Transform[] movePoint;

	private void Start()
	{
		anim = GetComponent<Animator>();
		transform.position = movePoint[0].position;
	}
	// Update is called once per frame
	void Update()
    {
		cooldownTime -= Time.deltaTime;

		bool isWorking = cooldownTime < 0;
        anim.SetBool("isWorking", isWorking);

		if (isWorking)
		{
			transform.position = Vector3.MoveTowards(transform.position, movePoint[movePointIndex].position, speed * Time.deltaTime);
		}
		if (Vector2.Distance(transform.position, movePoint[movePointIndex].position) < 0.15f)
		{
			Flip();
			cooldownTime = cooldown;
			movePointIndex++;

			if (movePointIndex >= movePoint.Length)
			{
				movePointIndex = 0;
			}
		}
    }
	private void Flip()
	{
		transform.localScale = new Vector3(1, transform.localScale.y * -1);
	}
}
