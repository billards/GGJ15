using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Player))]
public class Movement : MonoBehaviour, Noun
{
	// Normal Movements Variables
	private float walkSpeed;
	private float curSpeed;
	private float maxSpeed;

	private bool isDribbling = false;

	public float WalkSpeed = 5.0f;
	public float DribbleSpeed = 4.0f;
	public float SpeedUpTime = 1.0f;
	private Player player;

	public float KickedForce = 1.0f;
	public float StunnedTime = 2.0f;

	private OrbitingAction actionCollider;
	
	void Start()
	{
		this.player = GetComponent<Player>();
		print (player.Number);
		this.actionCollider = this.transform.GetChild(0).GetComponent<OrbitingAction>();
	}

	void Update()
	{
		if (Input.GetButtonDown("Tag"+player.Number))
		{
			actionCollider.PerformAction(RuleManager.Verbtype.Tag, true);
		}
		else if (Input.GetButton("Kick"+player.Number))
		{
			actionCollider.PerformAction(RuleManager.Verbtype.Kick, true);
		}
		else if (Input.GetButtonDown("Grab"+player.Number))
		{
			actionCollider.PerformAction(RuleManager.Verbtype.Grab, true);
		}
	}

	void FixedUpdate()
	{
		curSpeed = isDribbling ? DribbleSpeed : WalkSpeed;
		maxSpeed = curSpeed;

		Vector2 input = new Vector2(Input.GetAxis ("Horizontal"+player.Number), Input.GetAxis("Vertical"+player.Number));

		/*rigidbody2D.velocity = new Vector2(Mathf.Lerp(0, input.x * curSpeed, SpeedUpTime),
		                                   Mathf.Lerp(0, input.y * curSpeed, SpeedUpTime));*/
		rigidbody2D.AddForce(input * curSpeed, ForceMode2D.Force);
	}

	public void ToggleDribbling()
	{
		this.isDribbling = !this.isDribbling;
	}

	private void Kick()
	{
		// see if anything is hit by our kick
		// activate our kick collider
	}

	// launch in the direction
	public void Kicked(int player, Vector3 direction)
	{
		rigidbody2D.AddForce(direction);
	}

	public void Tagged(int player)
	{
		StartCoroutine("Stunned");
	}

	public void Grabbed(int player)
	{
		// to implement
	}

	IEnumerator Stunned()
	{
		float timer = 0;
		while (timer < StunnedTime)
		{
			timer += Time.deltaTime;
			this.rigidbody2D.velocity = Vector2.zero;
			yield return new WaitForSeconds(1.0f);
		}
	}
}