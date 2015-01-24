using UnityEngine;
using System.Collections;

public interface Noun
{
    AdjectiveType Adjective {get; set;}
	void Kicked(int player, Vector3 direction);
	void Tagged(int player);
	void Grabbed(int player);
}
