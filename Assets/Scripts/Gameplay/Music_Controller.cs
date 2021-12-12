using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_Controller : MonoBehaviour
{
    [SerializeField] private GameObject liftMusic = null;
    [SerializeField] private GameObject themeMusic = null;

	private void OnTriggerExit(Collider other) 
	{
		liftMusic.SetActive(false);
		themeMusic.SetActive(true);
	}

	private void OnTriggerEnter(Collider other) 
	{
		liftMusic.SetActive(true);
		themeMusic.SetActive(false);
	}
}
