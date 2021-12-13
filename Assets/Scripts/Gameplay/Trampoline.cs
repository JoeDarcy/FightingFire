using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Trampoline : MonoBehaviour
{
	public static int points = 0;
	public TextMeshPro pointsText = null;

	private void Update() 
	{
		Debug.Log("Points: " + points);

		if (pointsText) pointsText.text = points.ToString();
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
