using UnityEngine;
using System.Collections;

[RequireComponent((typeof(Collider2D)))]
public class OrbitingAction : MonoBehaviour 
{
	public Sprite Hand;
	public Sprite Foot;

	private RuleManager.Verbtype verb;
	private int player;


	// Use this for initialization
	void Start () 
	{
		// turn off the visual
		this.renderer.enabled = false;
		// turn off the 
		this.collider2D.enabled = false;
		this.player = this.transform.GetComponentInParent<Player>().Number;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// get it down to a smaller number
		Vector3 parentVelocity = this.transform.parent.rigidbody2D.velocity.normalized;
		// we don't want the ball to return to the centre when the player is not moving
		if (parentVelocity.magnitude != 0)
			transform.localPosition = new Vector3(parentVelocity.x/4, parentVelocity.y/4, 0);
	}

	public void PerformAction(RuleManager.Verbtype verb, bool enable)
	{
		// turn off the visual
		this.renderer.enabled = enable;
		// turn off the collider
		this.collider2D.enabled = enable;
		// change the state
		this.verb = verb;
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "CanInteract")
		{
			Noun noun = null;
			// if we get a collision with something, do the appropriate action on the parent
			foreach (Component component in GetComponents (typeof(Component)))
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
					case RuleManager.Verbtype.Grab :
						noun.Grabbed(player);
						break;
					case RuleManager.Verbtype.Kick :
						noun.Kicked(player, this.transform.localPosition.normalized);
						break;
					case RuleManager.Verbtype.Tag :
						noun.Tagged (player);
						break;
					default :
						break;
				}
			}
		}
	}
}
