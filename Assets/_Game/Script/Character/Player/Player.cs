using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Character
{
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private float speed = 3;
	[SerializeField] private float jumpForce;
	[SerializeField] private float skill1Cooldown = 3f;
	[SerializeField] private float lastSkill1UseTime = -3f;
	[SerializeField] private float HurtbackTime = 0.5f;
	[SerializeField] private Transform wallCheck;
	[SerializeField] private GameObject attackBase;
	[SerializeField] private float jumpBufferLength;
	[SerializeField] private float coyoteTimeLength;
	[SerializeField] private SkillLight lightPrefab;
	[SerializeField] private Transform lightPoint;
	[SerializeField] private LayerMask groundLayer;
	[SerializeField] private LayerMask wallLayer;
	[SerializeField] private LayerMask trapLayer;
	[SerializeField] private Vector2 hurtBack = new Vector2(1, 2);
	[SerializeField] private GameObject traps;

	private bool isHurt = false;
	private bool isGrounded = true;
	private bool isJumping = false;
	private bool isdoubleJump = true;
	private bool isAttacking = false;
	private bool isWallSliding = false;
	private float wallSlidingSpeed = 2f;
	private bool isWallJumping = false;
	private float wallJumpTime = 0.2f;
	private float wallJumpCounter;
	private float wallJumpDirection;
	private float wallJumpingDirection = 0.4f;
	private Vector2 wallJumpingPower = new Vector2(6f, 10f);
	private float jumpBufferCount;
	private float coyoteTimeCount;
	private bool isDeath = false;
	private float horizontal;
	private Vector3 savePoint;

	// Update is called once per frame
	void Update()
	{
		isGrounded = CollisionCheck();
		isWallSliding = IsTouchingWall();
		horizontal = Input.GetAxisRaw("Horizontal");
		if (!isHurt)
		{
			HandleDead();
			HandleJump();
			HandleAttack();
			HandleRun();
			WallSliding();
			WallJump();
		}
		HandleTrapCollision();

	}
	public override void OnInit()
	{
		base.OnInit();
		ChangeAnimSkin();
		isAttacking = false;
		isHurt = false;
		isDeath = false;

		DeAttack();
	}
	public override void OnDespawn()
	{
		base.OnDespawn();
		OnInit();
		Debug.Log("Player died312321");
		Destroy(gameObject);

	}
	protected override void OnDie()
	{
		base.OnDie();
		PlayerManagement.instance.PlayerRes();
	}
	private void HandleDead()
	{
		if (isDeath)
		{
			return;
		}
	}
	public void HandleTrapCollision()
	{
		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, trapLayer);

		if (hit.collider != null)
		{
			Vector2 pushDirection = (transform.position - hit.transform.position).normalized;

			rb.AddForce(pushDirection * hurtBack, ForceMode2D.Impulse);

			isHurt = true;

			StartCoroutine(HandleHurtState());
		}
	}
	private IEnumerator HandleHurtState()
	{
		yield return new WaitForSeconds(HurtbackTime);

		isHurt = false;
		ChangeAnim("Idle");
	}
	private void ResetHurt()
	{
		isHurt = false;
	}
	public void HandleJump()
	{
		if (isGrounded)
		{
			coyoteTimeCount = coyoteTimeLength;
		}
		else
		{
			coyoteTimeCount -= Time.deltaTime;
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			jumpBufferCount = jumpBufferLength;
		}
		else
		{
			jumpBufferCount -= Time.deltaTime;
		}

		if (jumpBufferCount >= 0 && (coyoteTimeCount > 0 || isdoubleJump) && !isAttacking)
		{
			Jump();
			jumpBufferCount = 0;

			if (!isGrounded)
			{
				isdoubleJump = false;
			}
			else if (isdoubleJump)
			{
				isdoubleJump = false;
			}
		}

		if (!isGrounded && rb.velocity.y < 0)
		{
			ChangeAnim("Fall");
			isJumping = false;
		}

		if (!isdoubleJump && isGrounded)
		{
			isdoubleJump = true;
		}
	}
	private void HandleAttack()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1) && !isJumping && isGrounded)
		{
			Attacking();
		}
		if (Input.GetKeyDown(KeyCode.Alpha2) && !isJumping && isGrounded && Time.time - lastSkill1UseTime >= skill1Cooldown)
		{
			Skill_1();
			lastSkill1UseTime = Time.time;
		}
	}
	public void Attacking()
	{
		rb.velocity = Vector2.zero;
		ChangeAnim("Attack");
		isAttacking = true;
		Invoke(nameof(ResetAttack), 0.5f);
		AtAttack();
		Invoke(nameof(DeAttack), 0.5f);

	}
	public void Skill_1()
	{
		rb.velocity = Vector2.zero;
		ChangeAnim("Skill1");
		isAttacking = true;
		Invoke(nameof(ResetAttack), 0.5f);

		Instantiate(lightPrefab, lightPoint.position, lightPoint.rotation);
	}
	private void ResetAttack()
	{
		isAttacking = false;
		ChangeAnim("Idle");
	}
	public void HandleRun()
	{
		if (isGrounded && Mathf.Abs(horizontal) > 0.1f && !isAttacking)
		{
			ChangeAnim("Run");
		}
		if (Mathf.Abs(horizontal) > 0.1f && !isAttacking)
		{
			rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
			transform.rotation = Quaternion.Euler(new Vector3(0, horizontal > 0 ? 0 : 180, 0));
		}
		else if (isGrounded && !isAttacking)
		{
			ChangeAnim("Idle");
			rb.velocity = Vector2.zero;
		}
	}
	public void Jump()
	{
		isJumping = true;
		ChangeAnim("Jump");
		rb.AddForce(jumpForce * Vector2.up);
	}
	private bool CollisionCheck()
	{
		Debug.DrawLine(transform.position, transform.position + Vector3.down * 1.1f, Color.red);
		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);
		return hit.collider != null;
	}
	private bool IsTouchingWall()
	{
		Debug.DrawRay(wallCheck.position, new Vector2(horizontal, 0), Color.red);
		RaycastHit2D hit = Physics2D.Raycast(wallCheck.position, new Vector2(horizontal, 1.1f), 1.1f, wallLayer);
		return hit.collider != null;
	}
	private void WallSliding()
	{
		bool isTouchingWall = IsTouchingWall();
		isWallSliding = isTouchingWall && !isGrounded;
		if (isWallSliding)
		{
			float wallSlideDirection = horizontal > 0 ? -1 : 1;
			rb.AddForce(new Vector2(wallSlideDirection * wallSlidingSpeed, 0), ForceMode2D.Force);

			rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(-wallSlidingSpeed, rb.velocity.y));
			ChangeAnim("WallSlide");
		}
		else if (!isTouchingWall)
		{
			isWallSliding = false;
		}
		if (!isGrounded && !isJumping && !isWallSliding)
		{
			ChangeAnim("Fall");
		}
	}
	public void WallJump()
	{
		if (isWallSliding && Input.GetKeyDown(KeyCode.Space))
		{
			isWallJumping = true;
			wallJumpCounter = wallJumpTime;
			wallJumpDirection = -Mathf.Sign(horizontal);
			ChangeAnim("WallJump");
			isdoubleJump = true;
		}
		if (isWallJumping)
		{
			rb.velocity = new Vector2(wallJumpingPower.x * wallJumpDirection * wallJumpingDirection, wallJumpingPower.y);
			wallJumpCounter -= Time.deltaTime;
			if (wallJumpCounter <= 0)
			{
				isWallJumping = false;
			}
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
	public void SetMove(float horizon)
	{
		this.horizontal = horizon;
	}


}
