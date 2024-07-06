using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : Character
{
	private IState currentState;
	private bool isRight = true;
	private Character target;

	public Character Target => target;

	[SerializeField] private float moveSpeed;
	[SerializeField] private float attackRange;
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private GameObject attackBase;
	//[SerializeField] private float respawnTime = 10f;

	private void Update()
	{

		if (currentState != null)
		{
			currentState.OnExecute(this);
		}
	}
	public override void OnInit()
	{
		base.OnInit();
		ChangeState(new IdleState());
		DeAttack();
	}
	public override void OnDespawn()
	{ 
		base.OnDespawn();
		Destroy(healthUl.gameObject);
		Destroy(gameObject);
	}
	protected override void OnDie()
	{
		ChangeState(null);
		rb.velocity = Vector2.zero;
		base.OnDie();
	}
	public void ChangeState(IState newState) 
	{
		if (currentState != null)
		{
			currentState.OnExit(this);
		}
		currentState = newState;

        if (currentState != null)
        {
			currentState.OnEnter(this);
        }
    }
	public void Move()
	{
		ChangeAnim("Enemy_run");
		rb.velocity = transform.right * moveSpeed;

	}
	public void StopMove()
	{
		ChangeAnim("Enemy_idle");
		rb.velocity = Vector2.zero;
	}
	public void Attack()
	{
		ChangeAnim("Enemy_att");
		AtAttack();
		Invoke(nameof(DeAttack), 0.5f);
	}
	public bool IsTargetInRange()
	{
		if (target != null && Vector2.Distance(target.transform.position, transform.position) <= attackRange)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("EnemyWall"))
		{
			ChangeDirection(!isRight);
		}
	}
	public void ChangeDirection(bool isRight)
	{
		this.isRight = isRight;
		transform.rotation = isRight ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(Vector3.up * 180);
	}
	internal void SetTarget(Character character)
	{
		this.target = character;
		if (IsTargetInRange())
		{
			ChangeState(new AttackState());
		}
		else if (Target != null)
		{
			ChangeState(new PatrolState());
		}
		else
		{
			ChangeState(new IdleState());

		}
	}
	private void AtAttack()
	{
		attackBase.SetActive(true);
	}
	private void DeAttack()
	{
		attackBase.SetActive(false);
	}
}
