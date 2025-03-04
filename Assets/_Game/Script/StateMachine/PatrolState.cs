﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
	float timer;
	float randomTime;
	public void OnEnter(Enemy enemy)
	{
		timer = 0;
		randomTime = Random.Range(2f, 5f);
	}
	public void OnExecute(Enemy enemy)
	{
		timer += Time.deltaTime;

		if (enemy.Target != null)
		{
			enemy.ChangeDirection(enemy.Target.transform.position.x > enemy.transform.position.x);
            if (enemy.IsTargetInRange())
			{
				enemy.ChangeState(new AttackState());
			}
			else
			{
				enemy.Move();

			}
		}
		else
		{
			if (timer < randomTime)
			{
				enemy.Move();
			}
			else
			{
				enemy.ChangeState(new IdleState());
			}
		}
	}
	public void OnExit(Enemy enemy)
	{
	}

}
