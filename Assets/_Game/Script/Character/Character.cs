using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	private float hp;
	private string currentAnim;

	[SerializeField] private Animator animator;
	[SerializeField] protected HealthUL healthUl;
	[SerializeField] protected CombatT combatTextPreb;

	public bool IsDead => hp <= 0;
	private void Start()
	{
		OnInit();
	}
	public virtual void OnInit()
	{
		hp = 100;
		healthUl.OnInit(100, transform);
	}

	public virtual void OnDespawn()
	{

	}
	protected virtual void OnDie()
	{
		AudioManager.instance.PlaySFx(0);
		ChangeAnim("Dead");
		Invoke(nameof(OnDespawn), 2f);
	}
	protected void ChangeAnim(string animCrr)
	{
		if (currentAnim != animCrr)
		{
			animator.ResetTrigger(animCrr);
			currentAnim = animCrr;
			animator.SetTrigger(currentAnim);
		}
	}
	public void OnHit(float damage)
	{
		if (!IsDead)
		{
			hp -= damage;
			Debug.Log("Character hit. HP: " + hp);
			if (IsDead)
			{
				hp = 0;
				OnDie();
			}
			else
			{
				AudioManager.instance.PlaySFx(1);
				ChangeAnim("Hurt");
			}
			healthUl.SetNewHp(hp);
			if (combatTextPreb != null)
			{
				Instantiate(combatTextPreb, transform.position + Vector3.up, Quaternion.identity).OnInit(damage);
			}
		}
	}
	public void ChangeAnimSkin() 
	{
		int skinId = PlayerManagement.instance.chosenSkinId;
		for (int i = 0; i < animator.layerCount; i++)
		{
			animator.SetLayerWeight(i, 0);
		}
		animator.SetLayerWeight(skinId, 1);
	}
}
