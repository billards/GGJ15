using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour 
{
	// Normal Movements Variables
	private float walkSpeed;
	private float curSpeed;
	private float maxSpeed;

	public float WalkSpeed = 5.0f;
	public float SpeedUpTime = 1.0f;
	
	void Start()
	{
	}
	
	void FixedUpdate()
	{
		curSpeed = WalkSpeed;
		maxSpeed = curSpeed;

		// Move senteces
		rigidbody2D.velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal") * curSpeed, SpeedUpTime),
		                                   Mathf.Lerp(0, Input.GetAxis("Vertical") * curSpeed, SpeedUpTime));
	}
}