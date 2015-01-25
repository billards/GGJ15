using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour 
{
	public GameObject TeamBoolHolder;
	public bool IsTeam = false;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void OnClick()
	{
		TeamBoolHolder.GetComponent<TeamBoolHolder>().IsTeamGame = IsTeam;
		Application.LoadLevel(1);
	}

}
