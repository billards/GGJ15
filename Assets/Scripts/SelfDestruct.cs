using UnityEngine;
using System.Collections;

public class SelfDestruct : MonoBehaviour 
{
	private float timer = 0;
	public float SelfDestructTime = 2.0f; // default value, override it just in case
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if ((timer += Time.deltaTime) > SelfDestructTime)
			Destroy (this.gameObject);
	}

}
