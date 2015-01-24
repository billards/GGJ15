using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour 
{
	public static AudioManager Instance;

	public AudioClip KickBall;
	public AudioClip KickPlayer;
	public AudioClip TagBall;
	public AudioClip TagPlayer;
	public AudioClip GrabBall;
	public AudioClip GrabPlayer;
	public AudioClip ActivateSwitch;
	public AudioClip BallHitsPlayer;
	public AudioClip Dash;

	private AudioSource audioSource;
	// Use this for initialization
	void Start () 
	{
		Instance = this;
		audioSource = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void PlayKickBall()
	{
		audioSource.PlayOneShot(KickBall);
	}

	public void PlayKickPlayer()
	{
		audioSource.PlayOneShot(KickPlayer);
	}
	
	public void PlayTagBall()
	{
		audioSource.PlayOneShot(TagBall);
	}
	
	public void PlayTagPlayer()
	{
		audioSource.PlayOneShot(TagPlayer);
	}
	
	public void PlayGrabBall()
	{
		audioSource.PlayOneShot(GrabBall);
	}
	
	public void PlayActivateSwitch()
	{
		audioSource.PlayOneShot(ActivateSwitch);
	}
	
	public void PlayBallHitsPlayer()
	{
		audioSource.PlayOneShot(BallHitsPlayer);
	}

	public void PlayGrabPlayer()
	{
		audioSource.PlayOneShot(GrabPlayer);
	}

	
	public void PlayDash()
	{
		audioSource.PlayOneShot(Dash);
	}
}
