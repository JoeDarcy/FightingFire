using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    //fire prefab
    [SerializeField] private GameObject fire = null;

    //object basic properties
    [SerializeField] public bool flammable = false;
    [SerializeField] public bool canFireSpread = false; //sometime later, change to false and change the texture to a burnt variation
    private bool isSpreading = false;
    private bool burntOut = false;
    private SphereCollider normalSpreadColliderHorizontal = null;
    private SphereCollider normalSpreadColliderVertical = null;
    private SphereCollider closeSpreadCollider = null;

    //normal fire spreading properties
    [SerializeField] private float spreadRangeHorizontal = 5.0f;
    [SerializeField] private float timeToSpreadHorizontal = 5.0f;
    [SerializeField] private float spreadRangeVertical = 5.0f;
    [SerializeField] private float timeToSpreadVertical = 5.0f;

    //close fire spreading properties
    [SerializeField] private bool closeSpreading = false;
    [SerializeField] private float closeSpreadRange = 5.0f;
    [SerializeField] private float timeToCloseSpread = 5.0f;

    private float timerStart = 0.0f;
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (flammable)
        {
            gameObject.tag = "Flammable";
            normalSpreadColliderHorizontal = gameObject.AddComponent<SphereCollider>();
            normalSpreadColliderHorizontal.radius = spreadRangeHorizontal;
            normalSpreadColliderVertical = gameObject.AddComponent<SphereCollider>();
            normalSpreadColliderVertical.radius = spreadRangeVertical;
            closeSpreadCollider = gameObject.AddComponent<SphereCollider>();
            closeSpreadCollider.radius = closeSpreadRange;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
