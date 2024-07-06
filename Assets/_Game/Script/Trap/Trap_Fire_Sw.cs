using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Fire_Sw : MonoBehaviour
{
    public Trap_Fire trap_Fire;
    private Animator anim;

    [SerializeField] private float timeNotActive = 3;

    private void Start()
	{
		anim = GetComponent<Animator>();
        trap_Fire = GetComponentInChildren<Trap_Fire>();  
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.GetComponent<Player>() != null)
        {
            anim.SetTrigger("Pessed");
            trap_Fire.FireAf(timeNotActive);
        }
    }
}
