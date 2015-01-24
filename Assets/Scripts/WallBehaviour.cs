using UnityEngine;
using System.Collections;

/**
 * Ensures the walls never move
 * 
 */
public class WallBehaviour : MonoBehaviour 
{
	private Vector3 originalPosition;
	// Use this for initialization
	void Start () 
	{
		originalPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position = originalPosition;
	}
}
