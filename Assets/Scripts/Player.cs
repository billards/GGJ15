using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public int Number = 0;
	public string Name;
	// Use this for initialization
	void Start () 
	{
		if (Number == 0)
			throw new UnityException("A player's number is still set to 0");
		else if (Number > 4 || Number < 0)
			throw new UnityException("A player's number is not with the range of 1-4");

	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
