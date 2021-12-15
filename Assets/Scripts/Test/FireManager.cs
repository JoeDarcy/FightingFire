using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    [SerializeField] GameObject midgroundSpreading = null;

    private List<GameObject> objFires = new List<GameObject>();

    private int totalFires = 0;
    private int activeFires = 0;
    private int extinguishedFires = 0;

	private void Update()
	{
		//Debug.Log("Active fires: " + activeFires);
		//Debug.Log("Total fires: " + totalFires);
    }

	public void AddFire(GameObject _fireInstance)
    {
        objFires.Add(_fireInstance);
        totalFires += 1;
        activeFires += 1;
    }

    public void UpdateMidground()
    {
        if (objFires.Count > 0)
        {
            SendMidgroundUpdate();
        }
    }

    private void SendMidgroundUpdate()
    {
        midgroundSpreading.GetComponent<MidgroundSpreading>().UpdateFires(objFires);
    }

    public void FireExtinguished() //call this when fire is put out *******************************************************************************************************
    {
        activeFires -= 1;
        extinguishedFires += 1;
    }

    public int GetTotalFires()
    {
        return totalFires;
    }

    public int GetActiveFires()
    {
        return activeFires;
    }

    public int GetExtinguishedFires()
    {
        return extinguishedFires;
    }
}
