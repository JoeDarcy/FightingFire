using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_Controller : MonoBehaviour
{
    [SerializeField] private GameObject liftMusic = null;
    [SerializeField] private GameObject themeMusic = null;

	private void OnTriggerExit(Collider other) 
	{
		if (other.CompareTag("Player"))
		{
			liftMusic.SetActive(false);
			themeMusic.SetActive(true);
		}
	}

	private void OnTriggerEnter(Collider other) 
	{
		if (other.CompareTag("Player"))
		{
			liftMusic.SetActive(true);
			themeMusic.SetActive(false);
		}
	}
}
