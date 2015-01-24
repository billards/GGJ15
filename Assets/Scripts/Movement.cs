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

	private bool isDribbling = false;

	public float WalkSpeed = 5.0f;
	public float DribbleSpeed = 4.0f;
	public float SpeedUpTime = 1.0f;
	private Player player;
	
	void Start()
	{
		player = GetComponent<Player>();
		print (player.Number);
	}
	
	void FixedUpdate()
	{
		curSpeed = isDribbling ? DribbleSpeed : WalkSpeed;
		maxSpeed = curSpeed;

		Vector2 input = new Vector2(Input.GetAxis ("Horizontal"+player.Number), Input.GetAxis("Vertical"+player.Number));
		//print(input.magnitude);
		print (player.Number);

		/*rigidbody2D.velocity = new Vector2(Mathf.Lerp(0, input.x * curSpeed, SpeedUpTime),
		                                   Mathf.Lerp(0, input.y * curSpeed, SpeedUpTime));*/
		rigidbody2D.AddForce(input * curSpeed, ForceMode2D.Force);
	}

	public void ToggleDribbling()
	{
		this.isDribbling = !this.isDribbling;
	}
}