using UnityEngine;
using System.Collections;

/**
 * The ball may or may not be attached to a player at any given time. 
 * If it is attached, we want the ball to spin to the position that the player is heading in.
 */
public class BallBehaviour : MonoBehaviour, Noun
{
	public AudioClip KickedNoise;
	public AudioClip TaggedNoise;
	public AudioClip GrabbedNoise;
	public float Cooldown = 1.0f;
	public float LaunchSpeed = 50.0f;
	private float cooldownTimer = 0.0f;
	private GameObject[] players;
	public int KickedBy { get; private set; }
	// Use this for initialization
	void Start () 
	{
		players = Camera.main.GetComponent<RuleManager>().players;
	}
	
	// Update is called once per frame
	void Update () 
	{	
		if (this.transform.parent != null)
		{
			// get it down to a smaller number
			Vector3 parentVelocity = this.transform.parent.rigidbody2D.velocity.normalized;
			// we don't want the ball to return to the centre when the player is not moving
			if (parentVelocity.magnitude != 0)
				transform.localPosition = new Vector3(parentVelocity.x/3, parentVelocity.y/3, 0);
		}
		cooldownTimer += Time.deltaTime;
	}

    public AdjectiveType adjectiveType = AdjectiveType.None;
    public AdjectiveType Adjective
    {
        get { return adjectiveType;  }
        set { }
    }

	public void Grabbed(int player)
	{
		// start the cooldown timer and change who the parent is, also reset the localposition
		this.transform.parent = players[player-1].transform;
		this.transform.localPosition = new Vector3(0, 0.25f, 0);
		this.cooldownTimer = 0;
		this.rigidbody2D.velocity = Vector2.zero;
		AudioManager.Instance.PlayGrabBall();

        RuleManager.instance.CheckRule(player, Verbtype.Grab, NounType.Ball, Adjective);
	}

	public void Kicked(int player, Vector3 direction)
	{
		// remove any parenting
		this.transform.parent = null;
		KickedBy = player;
		this.rigidbody2D.AddForce(direction.normalized * LaunchSpeed);
		AudioManager.Instance.PlayKickBall();

        RuleManager.instance.CheckRule(player, Verbtype.Kick, NounType.Ball, Adjective);
	}

	public void Tagged(int player)
	{
		// do nothing?
	}

	public bool IsOffCooldown()
	{
		return cooldownTimer > Cooldown;
	}
}
