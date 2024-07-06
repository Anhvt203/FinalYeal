using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLight : MonoBehaviour
{
	public GameObject hitVFX;
	public Rigidbody2D rb;
    [SerializeField] private float damage;
    void Start()
    {
        OnInit();
    }
    public void OnInit()
    {
        rb.velocity = transform.right * 5f;
        Invoke(nameof(OnDespawn), 3f);
    }
    public void OnDespawn()
    {
        Destroy(gameObject);
    }
	private void OnTriggerEnter2D(Collider2D collision)
	{
		Enemy enemy = collision.GetComponent<Enemy>();
		if (collision.tag == "Enemy")
		{
			enemy.OnHit(damage);
			Instantiate(hitVFX, transform.position, transform.rotation);
			OnDespawn();
		}
	}
}
