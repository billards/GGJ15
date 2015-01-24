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
	public int KicksToFree = 3;
	private Player player;
	private int kicksTaken = 0;

	public float KickedForce = 1.0f;
	public float StunnedTime = 2.0f;
	public float LaunchSpeed = 100.0f;

	private bool isGrabbed = false;
	private Transform grabber;
	private OrbitingAction actionCollider;

	private float timer = 0;
	public float GlobalCooldown = 0.25f;
	
	void Start()
	{
		this.player = GetComponent<Player>();
		this.actionCollider = this.transform.GetChild(0).GetComponent<OrbitingAction>();
		timer = GlobalCooldown;
	}

	void Update()
	{ 
		// check if we're on the global cooldown - to prevent spamming
		if ((timer += Time.deltaTime) >= GlobalCooldown)
		{
			if (Input.GetButtonDown("Tag"+player.Number) && this.transform.childCount < 2)
			{
				actionCollider.PerformAction(RuleManager.Verbtype.Tag, true);
				timer = 0;
			}
			else if (Input.GetButton("Kick"+player.Number))
			{
				actionCollider.PerformAction(RuleManager.Verbtype.Kick, true);
				timer = 0;
			}
			else if (Input.GetButtonDown("Grab"+player.Number) && this.transform.childCount < 2 && !isGrabbed)
			{
				actionCollider.PerformAction(RuleManager.Verbtype.Grab, true);
				timer = 0;
			}
			else if (Input.GetButtonDown ("Dash"+player.Number) && !isGrabbed)
			{
				Dash(new Vector2(Input.GetAxis ("Horizontal"+player.Number), Input.GetAxis("Vertical"+player.Number)));
				timer = 0;
			}
		}
		if (this.transform.childCount > 1) // we're holding onto something..
			isGrabbing = true;
		else
			isGrabbing = false;

		// if our grabber has taken enough kicks, free ourselves
		if (grabber != null && grabber.gameObject.GetComponent<PlayerBehaviour>().hasTakenEnoughKicks())
			isGrabbed = false;
	}

	void FixedUpdate()
	{
		curSpeed = isGrabbing ? GrabSpeed : WalkSpeed;
		maxSpeed = curSpeed;

		Vector2 input = new Vector2(Input.GetAxis ("Horizontal"+player.Number), Input.GetAxis("Vertical"+player.Number));

		/*rigidbody2D.velocity = new Vector2(Mathf.Lerp(0, input.x * curSpeed, SpeedUpTime),
		                                   Mathf.Lerp(0, input.y * curSpeed, SpeedUpTime));*/
		rigidbody2D.AddForce(input * curSpeed, ForceMode2D.Force);

		if (isGrabbed) // pull toward your grabber
		{
			Vector3 direction = this.transform.position - grabber.position;
			if (direction.magnitude < 0.40f)
				direction = Vector3.zero;
			this.rigidbody2D.AddForce(new Vector2(direction.x, direction.y) * LaunchSpeed/-7.0f);
		}
	}

	// launch in the direction
	public void Kicked(int player, Vector3 direction)
	{
		isGrabbed = false;
		rigidbody2D.AddForce(direction.normalized * LaunchSpeed);
		kicksTaken++;
		AudioManager.Instance.PlayKickPlayer();
	}

	public void Tagged(int player)
	{
		//TODO: THIS
		StartCoroutine("Stunned");
		AudioManager.Instance.PlayTagPlayer();
	}

	public void Grabbed(int player)
	{
		AudioManager.Instance.PlayGrabPlayer();
		isGrabbed = true;
		GameObject grabberGO = Camera.main.GetComponent<RuleManager>().players[player-1];
		grabberGO.GetComponent<PlayerBehaviour>().kicksTaken = 0;
		grabber = grabberGO.transform;
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

	public void Dash(Vector2 input)
	{
		print ("dash");
		this.rigidbody2D.AddForce (input.normalized * LaunchSpeed/2.0f);
	}

	public bool hasTakenEnoughKicks()
	{
		return kicksTaken >= KicksToFree;
	}
}