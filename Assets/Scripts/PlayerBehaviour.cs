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
	private bool isStunned = false;
	private float stunnedTimer = 0.0f;

	public Sprite StunnedSprite;
	private Sprite normalSprite;
	private SpriteRenderer spriteRenderer;
	private Animator animator;

	public GameObject[] StunAnimations;

	void Start()
	{
		player = GetComponent<Player>();
		actionCollider = this.GetComponentInChildren<OrbitingAction>();
		timer = GlobalCooldown;
		spriteRenderer = this.GetComponent<SpriteRenderer>();
		normalSprite = spriteRenderer.sprite;
		animator = this.GetComponent<Animator>();
	}

	void Update()
	{ 
		if (!isStunned) // can't do anything
		{
			// check if we're on the global cooldown - to prevent spamming
			if ((timer += Time.deltaTime) >= GlobalCooldown)
			{
				if (Input.GetButtonDown("Tag"+player.Number) && this.transform.childCount < 2)
				{
					actionCollider.PerformAction(Verbtype.Tag, true);
					timer = 0;
				}
				else if (Input.GetButton("Kick"+player.Number))
				{
					actionCollider.PerformAction(Verbtype.Kick, true);
					timer = 0;
				}
				else if (Input.GetButtonDown("Grab"+player.Number) && this.transform.childCount < 2 && !isGrabbed)
				{
					actionCollider.PerformAction(Verbtype.Grab, true);
					timer = 0;
				}
				else if (Input.GetButtonDown ("Dash"+player.Number) && !isGrabbed)
				{
					Dash(new Vector2(Input.GetAxis ("Horizontal"+player.Number), Input.GetAxis("Vertical"+player.Number)));
					timer = 0;
				}
			}
		}
		else if ((stunnedTimer += Time.deltaTime) > StunnedTime)
		{
			isStunned = false;
			stunnedTimer = 0;
			UseStunnedSprite(false);
		}
		// release the tag hand
		if ((Input.GetButtonUp("Tag"+player.Number) || Input.GetButtonUp("Grab"+player.Number)) && this.transform.childCount < 2)
		{
			actionCollider.PerformAction(Verbtype.Tag, false);
		}
		if (this.transform.childCount > 1) // we're holding onto something..
			isGrabbing = true;
		else
			isGrabbing = false;

		// if our grabber has taken enough kicks, free ourselves
		if (grabber != null && grabber.gameObject.GetComponent<PlayerBehaviour>().hasTakenEnoughKicks())
		{
			isGrabbed = false;
			UseStunnedSprite(false);
			grabber.gameObject.GetComponent<PlayerBehaviour>().Tagged(player.Number);
			grabber = null;
		}
	}

	void FixedUpdate()
	{
		curSpeed = isGrabbing ? GrabSpeed : WalkSpeed;
		maxSpeed = curSpeed;

		Vector2 input = new Vector2(Input.GetAxis ("Horizontal"+player.Number), Input.GetAxis("Vertical"+player.Number));

		/*rigidbody2D.velocity = new Vector2(Mathf.Lerp(0, input.x * curSpeed, SpeedUpTime),
		                                   Mathf.Lerp(0, input.y * curSpeed, SpeedUpTime));*/
		if (!isStunned)
			rigidbody2D.AddForce(input * curSpeed, ForceMode2D.Force);

		if (isGrabbed) // pull toward your grabber
		{
			Vector3 direction = this.transform.position - grabber.position;
			if (direction.magnitude < 0.45f)
				direction = Vector3.zero;
			this.rigidbody2D.AddForce(new Vector2(direction.x, direction.y) * LaunchSpeed/-7.0f);
		}
	}

	private void UseStunnedSprite(bool stunned)
	{
		// if we're grabbed or stunned, always set to false
		if (isStunned || isGrabbed)
		{
			spriteRenderer.sprite = StunnedSprite;
			animator.enabled = false;
		}
		else
		{
			if (stunned)
				spriteRenderer.sprite = StunnedSprite;
			else
				spriteRenderer.sprite = normalSprite;
			// need to stop the animator or it will overwrite any changes
			animator.enabled = !stunned;
		}
	}

	// launch in the direction
    public AdjectiveType Adjective
    {
        get
        {
            switch (player.Number)
            {
                case 1:
                    return AdjectiveType.One;

                case 2:
                    return AdjectiveType.Two;
                    
                case 3:
                    return AdjectiveType.Three;

                case 4:
                    return AdjectiveType.Four;

                default:
                    return AdjectiveType.None;
            }
        }

        set { }
    }

	public IEnumerator HurtAnimation()
	{
		print ("hurt anim");
		animator.SetBool("isHurt", true);
		yield return new WaitForSeconds(1);
		animator.SetBool("isHurt", false);
		animator.SetInteger("state", 1)
	}

	public void Kicked(int player, Vector3 direction)
	{
		isGrabbed = false;
		UseStunnedSprite(false);
		rigidbody2D.AddForce(direction * LaunchSpeed);
		kicksTaken++;
		AudioManager.Instance.PlayKickPlayer();
		StartCoroutine("HurtAnimation");

        RuleManager.instance.CheckRule(player, Verbtype.Kick, NounType.Player, Adjective);
	}

	public void Tagged(int player)
	{
		if (!isStunned && !isGrabbed) // don't allow chain stuns
		{
			AudioManager.Instance.PlayTagPlayer();
			isStunned = true;
			stunnedTimer = 0;
			UseStunnedSprite(true);
			int rand = Random.Range(0, StunAnimations.Length);
			GameObject stunnedAnimation = Instantiate(StunAnimations[rand], new Vector3(transform.position.x, transform.position.y + 0.125f, 0), Quaternion.identity) as GameObject;
			stunnedAnimation.transform.parent = this.transform;
			stunnedAnimation.GetComponent<SelfDestruct>().SelfDestructTime = StunnedTime;
			// drop any ball(s?) you are carrying
			BallBehaviour[] comps = this.transform.GetComponentsInChildren<BallBehaviour>();
			if (comps != null)
			{
				foreach (BallBehaviour component in comps)
				{
					Vector3 direction = new Vector3(Input.GetAxis ("Horizontal"+this.player.Number), Input.GetAxis("Vertical"+this.player.Number), 0);
					direction = direction.normalized / 2.0f;
					component.Kicked (this.player.Number, direction);
				}
			}
			kicksTaken = KicksToFree+1; // +1 just to be sure, should release anything we're holding

		}

        RuleManager.instance.CheckRule(player, Verbtype.Tag, NounType.Player, Adjective);
	}

	public void Grabbed(int player)
	{
		AudioManager.Instance.PlayGrabPlayer();
		isGrabbed = true;
		GameObject grabberGO = Camera.main.GetComponent<RuleManager>().players[player-1];
		grabberGO.GetComponent<PlayerBehaviour>().kicksTaken = 0;
		grabber = grabberGO.transform;
		UseStunnedSprite(true);

        RuleManager.instance.CheckRule(player, Verbtype.Grab, NounType.Player, Adjective);
	}

	public void Dash(Vector2 input)
	{
		//AudioManager.Instance.PlayDash();
		this.rigidbody2D.AddForce (input.normalized * LaunchSpeed * 0.5f);
	}

	public bool hasTakenEnoughKicks()
	{
		return kicksTaken >= KicksToFree;
	}

	public void OnCollisionEnter2D(Collision2D other)
	{
		BallBehaviour bbComponent;
		if ((bbComponent = other.gameObject.GetComponent<BallBehaviour>())!= null)
		{
			// if the ball wasnt kicked by us and was going pretty fast, we'll get stunned
			if (other.transform.rigidbody2D.velocity.magnitude > 3.5f)
				Tagged(bbComponent.KickedBy);
		}
	}
}