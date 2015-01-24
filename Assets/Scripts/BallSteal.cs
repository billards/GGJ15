using UnityEngine;
using System.Collections;

public class BallSteal : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	/*void OnTriggerEnter2D(Collider2D other)
	{
		// if the other collider is a ball and can be stolen (not on cooldown), do so.
		if (other.GetComponent<Ball>() != null)
		{
			Debug.Log(other.name);
			Ball ball = other.GetComponent<Ball>();
			if (ball.IsOffCooldown())
			{
				if (ball.transform.parent != null)
					ball.transform.parent.GetComponent<Movement>().ToggleDribbling();
				ball.Steal(this.transform);
				this.GetComponent<Movement>().ToggleDribbling();
			}
		}
	}*/

	/*void OnCollisionEnter2D(Collision2D other)
	{
		// if the other collider is a ball and can be stolen (not on cooldown), do so.
		if (other.gameObject.GetComponent<Ball>() != null)
		{
			Debug.Log(other.gameObject.name);
			Ball ball = other.gameObject.GetComponent<Ball>();
			if (ball.IsOffCooldown())
			{
				if (ball.transform.parent != null)
					ball.transform.parent.GetComponent<Movement>().ToggleDribbling();
				ball.Steal(this.transform);
				this.GetComponent<Movement>().ToggleDribbling();
			}
		}
	}*/
}
