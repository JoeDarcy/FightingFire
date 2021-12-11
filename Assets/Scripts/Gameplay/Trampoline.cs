using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
	public static int points = 0;

	private void Update() 
	{
		Debug.Log("Points: " + points);
	}

	private void OnTriggerEnter(Collider other) 
	{
		if (other.CompareTag("Old_Woman"))
		{
			points += 200;
		}
        else if (other.CompareTag("Baby"))
		{
			points += 500;
        }
	}
}
