using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftController : MonoBehaviour
{
	[SerializeField] bool levelComplete = false;
	private bool completeDone = false;
	private Animator liftAnimator = null;

    // Start is called before the first frame update
    void Start()
    {
        liftAnimator = GetComponent<Animator>();
    }

	private void Update() 
	{
		// If the level is complete open the lift doors again
		if (levelComplete == true && completeDone == false)
		{
			liftAnimator.SetBool("levelComplete", true);
			liftAnimator.SetBool("playerEnterLift", false);
			liftAnimator.SetBool("playerExitLift", false);
			completeDone = true;
		}
	}

	private void OnTriggerEnter(Collider other) 
	{
        liftAnimator.SetBool("playerEnterLift", true);
	}

	private void OnTriggerExit(Collider other) 
	{
		liftAnimator.SetBool("playerExitLift", true);
    }
}
