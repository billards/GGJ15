using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Player))]
public class Movement : MonoBehaviour 
{
	// Normal Movements Variables
	private float walkSpeed;
	private float curSpeed;
	private float maxSpeed;

	public float WalkSpeed = 5.0f;
	public float SpeedUpTime = 1.0f;
	private Player player;
	
	void Start()
	{
		player = GetComponent<Player>();
	}
	
	void FixedUpdate()
	{
		curSpeed = WalkSpeed;
		maxSpeed = curSpeed;

		Vector2 input = new Vector2(Input.GetAxis ("Horizontal"+player.Number), Input.GetAxis("Vertical"+player.Number));
		print(input.magnitude);
		//if (input.magnitude > 0.25f)
		{
			rigidbody2D.velocity = new Vector2(Mathf.Lerp(0, input.x * curSpeed, SpeedUpTime),
			                                   Mathf.Lerp(0, input.y * curSpeed, SpeedUpTime));
		}
	}
}