using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.GetComponent<BallBehaviour>() != null)
		{
			other.transform.rigidbody2D.velocity *= -10;
		}
	}
}
