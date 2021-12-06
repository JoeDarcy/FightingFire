using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    [SerializeField] GameObject midgroundSpreading = null;

    private List<GameObject> objFires = new List<GameObject>();


    public void AddFire(GameObject _fireInstance)
    {
        objFires.Add(_fireInstance);
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
}
