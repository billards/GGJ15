using UnityEngine;
using System.Collections;

public class TeamBoolHolder : MonoBehaviour 
{
	public bool IsTeamGame;
	// Use this for initialization
	void Start () 
	{
		GameObject.DontDestroyOnLoad(this.gameObject);
		GameObject.DontDestroyOnLoad(GameObject.Find("BGMusic"));
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
