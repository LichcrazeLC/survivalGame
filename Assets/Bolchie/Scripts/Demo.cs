using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Basic Player Script//
//controls: 
//A, D, Left, Right to move
//Left Alt to attack
//Space to jump
//Z is to see dead animation

public class Demo : MonoBehaviour {

	//variable for how fast player runs//
	private float speed = 5f;
	public GameObject gameOverText;

	private bool facingRight = true;
	private Animator anim;
	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;

	//variable for how high player jumps//
	[SerializeField]
	private float jumpForce = 300f;

	public Rigidbody2D rb { get; set; }

	bool dead = false;
	bool attack = false;
	public bool bigSlash = false;

	public ParticleSystem Slash;
	public ParticleSystem BigSlash;

	public ParticleSystem greenAura;
	public PlayerStats playerStats;
	void Start () {
		GetComponent<Rigidbody2D> ().freezeRotation = true;
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponentInChildren<Animator> ();
	}

	void Update()
	{
		HandleInput();
	}

	//movement//
	void FixedUpdate ()
	{
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);

		float horizontal = Input.GetAxis("Horizontal");
		if (!dead && !attack)
		{
			anim.SetFloat ("vSpeed", rb.velocity.y);
			anim.SetFloat ("Speed", Mathf.Abs (horizontal));
			rb.velocity = new Vector2 (horizontal * speed, rb.velocity.y);
		}
		if (horizontal > 0 && !facingRight && !dead && !attack) {
			Flip (horizontal);
		}

		else if (horizontal < 0 && facingRight && !dead && !attack){
			Flip (horizontal);
		}
	}

	private void HandleInput()
	{
		if (Input.GetKeyDown(KeyCode.Space) && !dead) 
		{
			attack = true;
			anim.SetBool ("Attack", true);
			anim.SetFloat ("Speed", 0);
			anim.SetBool("Ground", true);
			Slash.Play();
		}
		if (Input.GetKeyUp(KeyCode.Space))
			{
			attack = false;
			anim.SetBool ("Attack", false);
			}

		if (grounded && Input.GetKeyDown(KeyCode.W) && !dead)
		{
			anim.SetBool ("Ground", false);
			rb.AddForce (new Vector2 (0,jumpForce));
		}

		if (Input.GetKeyDown(KeyCode.E) && !dead && playerStats.slashPieces > 0){
			attack = true;
			anim.SetBool ("Attack", true);
			anim.SetFloat ("Speed", 0);
			anim.SetBool("Ground", true);
			bigSlash = true;
			BigSlash.Play();
			playerStats.AddSlashPiece(-1);
		}
		if (Input.GetKeyUp(KeyCode.E))
			{
			attack = false;
			anim.SetBool ("Attack", false);
			}

		if (Input.GetKeyDown(KeyCode.R) && !dead && playerStats.slashPieces > 0 && playerStats.shieldPieces > 0){
			StartCoroutine(GreenMode());
			playerStats.AddSlashPiece(-1);
			playerStats.AddShieldPiece(-1);
		}
	}

	public IEnumerator GreenMode(){
		greenAura.Play();
		float initJumpforce = jumpForce;
		float initSpeed = speed;

		jumpForce += 100;
		speed += 5;
		yield return new WaitForSeconds(15);

		jumpForce = initJumpforce;
		speed = initSpeed;
		greenAura.Stop();
	}

	public void Die(){
			anim.SetBool ("Dead", true);
			anim.SetFloat ("Speed", 0);
			dead = true;
			gameOverText.SetActive(true);
	}
		
	private void Flip (float horizontal)
	{
			facingRight = !facingRight;
			if (!facingRight)
			transform.localRotation = new Quaternion(0, 90,0, 1);
			else 
			transform.localRotation = new Quaternion(0, 0,0, 1);

	}
}
