using UnityEngine;
using System.Collections;

[RequireComponent((typeof(Collider2D)))]
public class OrbitingAction : MonoBehaviour 
{
	public Sprite Hand;
	public Sprite Foot;
	public float KickDuration = 0.25f;

	private Verbtype verb;
	private int player;
	private float timer = 0;
	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () 
	{
		// turn off the visual
		this.renderer.enabled = false;
		// turn off the 
		this.collider2D.enabled = false;
		this.player = this.transform.GetComponentInParent<Player>().Number;
		this.spriteRenderer = this.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// get it down to a smaller number
		Vector3 parentVelocity = this.transform.parent.rigidbody2D.velocity.normalized;
		// we don't want the ball to return to the centre when the player is not moving
		if (parentVelocity.magnitude != 0)
			transform.localPosition = new Vector3(parentVelocity.x/6, parentVelocity.y/6, 0);
		if (timer > 0)
		{
			timer -= Time.deltaTime;
		}
		else
		{
			timer = 0;
			if (spriteRenderer.sprite == this.Foot)
				toggle(false);
		}
	}

	public void PerformAction(Verbtype verb, bool enable)
	{
		toggle (enable);
		// change the state
		this.verb = verb;
		switch (verb)
		{
			case Verbtype.Grab :
			case Verbtype.Tag :
				spriteRenderer.sprite = this.Hand;
				break;
			case Verbtype.Kick:
				spriteRenderer.sprite = this.Foot;
				this.timer = KickDuration;
				break;
			default :
				break;
		}
	}

	private void toggle(bool value)
	{
		// turn off the visual
		this.renderer.enabled = value;
		// turn off the collider
		this.collider2D.enabled = value;
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "CanInteract")
		{
			Noun noun = null;
			// if we get a collision with something, do the appropriate action on the parent
			foreach (Component component in other.GetComponents (typeof(Component)))
			{
				if (component is Noun)
				{
					noun = component as Noun;
					break;
				}
			}
			if (noun != null)
			{
				switch (verb)
				{
					case Verbtype.Grab :
						noun.Grabbed(player);
						break;
					case Verbtype.Kick :
						noun.Kicked(player, this.transform.localPosition.normalized);
						break;
					case Verbtype.Tag :
						noun.Tagged (player);
						break;
					default :
						break;
				}
				toggle(false);
			}
		}
	}
}
