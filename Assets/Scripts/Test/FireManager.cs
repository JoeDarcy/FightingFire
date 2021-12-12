using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    [SerializeField] GameObject midgroundSpreading = null;

    private List<GameObject> objFires = new List<GameObject>();

    public int totalFires = 0;
    public int activeFires = 0;
    public int extinguishedFires = 0;

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
}
