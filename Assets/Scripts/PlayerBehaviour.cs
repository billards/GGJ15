using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Player))]
public class PlayerBehaviour : MonoBehaviour, Noun
{
	// Normal Movements Variables
	private float walkSpeed;
	private float curSpeed;
	private float maxSpeed;

	private bool isGrabbing = false;

	public float WalkSpeed = 5.0f;
	public float GrabSpeed = 4.0f;
	public float SpeedUpTime = 1.0f;
	private Player player;

	public float KickedForce = 1.0f;
	public float StunnedTime = 2.0f;
	public float LaunchSpeed = 100.0f;

	private bool isGrabbed = false;
	private Transform grabber;
	private OrbitingAction actionCollider;
	
	void Start()
	{
		this.player = GetComponent<Player>();
		print (player.Number);
		this.actionCollider = this.transform.GetChild(0).GetComponent<OrbitingAction>();
	}

	void Update()
	{
		if (Input.GetButtonDown("Tag"+player.Number) && this.transform.childCount < 2)
		{
			actionCollider.PerformAction(RuleManager.Verbtype.Tag, true);
		}
		else if (Input.GetButton("Kick"+player.Number))
		{
			actionCollider.PerformAction(RuleManager.Verbtype.Kick, true);
		}
		else if (Input.GetButtonDown("Grab"+player.Number) && this.transform.childCount < 2)
		{
			actionCollider.PerformAction(RuleManager.Verbtype.Grab, true);
		}
		if (this.transform.childCount > 1) // we're holding onto something..
			isGrabbing = true;
		else
			isGrabbing = false;
	}

	void FixedUpdate()
	{
		curSpeed = isGrabbing ? GrabSpeed : WalkSpeed;
		maxSpeed = curSpeed;

		Vector2 input = new Vector2(Input.GetAxis ("Horizontal"+player.Number), Input.GetAxis("Vertical"+player.Number));

		/*rigidbody2D.velocity = new Vector2(Mathf.Lerp(0, input.x * curSpeed, SpeedUpTime),
		                                   Mathf.Lerp(0, input.y * curSpeed, SpeedUpTime));*/
		rigidbody2D.AddForce(input * curSpeed, ForceMode2D.Force);

		if (isGrabbed)
		{
			Vector3 direction = this.transform.position - grabber.position;
			this.rigidbody2D.AddForce(new Vector2(direction.x, direction.y) * LaunchSpeed/4.0f);
		}
	}
	private void Kick()
	{
		// see if anything is hit by our kick
		// activate our kick collider
	}

	// launch in the direction
	public void Kicked(int player, Vector3 direction)
	{
		rigidbody2D.AddForce(direction.normalized * LaunchSpeed);
		AudioManager.Instance.PlayKickPlayer();
	}

	public void Tagged(int player)
	{
		StartCoroutine("Stunned");
		AudioManager.Instance.PlayTagPlayer();
	}

	public void Grabbed(int player)
	{
		// to implement
		AudioManager.Instance.PlayGrabPlayer();
		isGrabbed = true;
		grabber = Camera.main.GetComponent<RuleManager>().players[player-1].transform;
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