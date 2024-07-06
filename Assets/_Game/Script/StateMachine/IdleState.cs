using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
	float randomTime;
	float time;
	public void OnEnter(Enemy enemy)
	{
		enemy.StopMove();
		time = 0;
		randomTime = Random.Range(1f, 3f);
	}
	public void OnExecute(Enemy enemy)
	{
		time += Time.deltaTime;
		if (time > randomTime)
        {
			enemy.ChangeState(new PatrolState());
        }
    }
	public void OnExit(Enemy enemy)
	{
	}

}
