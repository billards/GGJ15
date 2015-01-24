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

	void OnTriggerEnter2D(Collider2D other)
	{
		// if the other collider is a ball and can be stolen (not on cooldown), do so.
		if (other.GetComponent<Ball>() != null)
		{
			Debug.Log(other.name);
			Ball ball = other.GetComponent<Ball>();
			if (ball.IsOffCooldown())
			{
				ball.Steal(this.transform);
			}
		}
	}
}
