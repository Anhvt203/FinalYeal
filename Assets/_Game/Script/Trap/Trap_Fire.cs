using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Fire : Traps
{
	private Animator anim;
	public bool isWorking = true;

	public float repeatRate;

	private void Start()
	{
		anim = GetComponent<Animator>();
		if (transform.parent == null)
		{
			InvokeRepeating("FireSw", 0, repeatRate);
		}
	}
	private void Update()
	{
		anim.SetBool("isWorking", isWorking);
	}
	public void FireSw()
	{
        isWorking = !isWorking;
	}
	public void FireAf(float two)
	{
		CancelInvoke();
		FireSw();
		Invoke("FireSw", two);	
	}
	protected override void OnTriggerEnter2D(Collider2D collision)
	{
        if (isWorking)
        {
			base.OnTriggerEnter2D(collision);
		}
        else
        {
           Debug.Log("Trap is not working");
        }
    }
}		

