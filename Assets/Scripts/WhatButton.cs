using UnityEngine;
using System.Collections;

public class WhatButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetAxis("Fire1") != 0)
		{
			print ("yes");

		}
	}
}
