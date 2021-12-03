using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidgroundSpreading : MonoBehaviour
{
    //place this on 'floor' empty object, when a new fire is instantiated,
    // add a reference of it to another script on an empty object e.g.
    // "FireManager.cs" on an empty object called "Fire Manager", each
    // InteractiveObject.cs and MidgroundSpreading.cs script will need a
    // reference of the Fire Manager object, and tell it to add whatever
    // (new fire, etc) to some list or similar, MidgroundSpreading.cs will
    // be on a timer which runs a check on the FireManager script on its
    // object for new fires, then calculate which midground objetcs to set
    // to which material, over whatever time

    private List<GameObject> objProcessedFires = new List<GameObject>();
    private List<GameObject> objNewFires = new List<GameObject>();

    private List<Tile> tiles = new List<Tile>();

    [SerializeField] private float rangeBurnt1 = 3.0f;
    [SerializeField] private float spreadTimeBurnt1 = 1.0f;
    [SerializeField] private Material burntMaterial1 = null;
    [SerializeField] private float rangeBurnt2 = 2.0f;
    [SerializeField] private float spreadTimeBurnt2 = 1.0f;
    [SerializeField] private Material burntMaterial2 = null;
    [SerializeField] private float rangeBurnt3 = 0.75f;
    [SerializeField] private float spreadTimeBurnt3 = 1.0f;
    [SerializeField] private Material burntMaterial3 = null;

    private string midgroundTag = "Midground";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //timer to call a get function "updateMe" or whatever on FireManager,
        //which then calls UpdateFires here passing in their objFires list to
        //to process then update our processdfires list

        //call it every 2 seconds


        //another foreach for everything in tiles list, check if
        //timer >= totaltime, if not then increment timer by total time,
        //if its now >= total time then set the material

        //each tile can be in the list multiple times, check for material3, then
        //material1, then material2
    }

    public void UpdateFires(List<GameObject> _objFires) //called from 
    {
        foreach (GameObject obj in _objFires)
        {
            if (!objProcessedFires.Contains(obj))
            {
                objNewFires.Add(obj);
            }
        }

        if (objNewFires.Count > 0)
        {
            ProcessNewFires();
            ApplyUpdatedTiles();
        }
    }

    private void ProcessNewFires()
    {
        foreach (GameObject obj in objNewFires)
        {
            List<GameObject> sourceTiles = new List<GameObject>();

            Vector3 v3ObjBoxExtents = obj.GetComponent<Collider>().bounds.extents;
            Vector3 v3SearchBoxExtents = new Vector3(v3ObjBoxExtents.x + rangeBurnt1, v3ObjBoxExtents.y, v3ObjBoxExtents.z + rangeBurnt1);
            Vector3 v3SearchBoxOrigin = new Vector3(obj.transform.position.x, obj.transform.position.y - (v3ObjBoxExtents.y / 2.0f), obj.transform.position.z);
            
            Collider[] boxHitColliders = Physics.OverlapBox(v3SearchBoxOrigin, v3SearchBoxExtents / 2);

            for (int i = 0; i < boxHitColliders.Length; ++i)
            {
                GameObject obj2 = boxHitColliders[i].gameObject;
                if (obj2.CompareTag(midgroundTag))
                {
                    //some sort of check like the other two, where if its already material3 then dont add it
                    tiles.Add(new Tile { gameObject = obj2, material = burntMaterial3, spreadTime = spreadTimeBurnt3, spreadTotalTime = 0.0f });
                    sourceTiles.Add(obj2);
                }
            }


            foreach (GameObject obj2 in sourceTiles)
            {
                Collider[] sphereHhitColliders = Physics.OverlapSphere(obj2.transform.position, rangeBurnt2 / 2);

                for (int i = 0; i < sphereHhitColliders.Length; ++i)
                {
                    if (sphereHhitColliders[i].gameObject.CompareTag(midgroundTag))
                    {
                        bool exists = false;
                        foreach (Tile tile in tiles)
                        {
                            if (!exists)
                            {
                                if (tile.gameObject == obj2)
                                {
                                    if (tile.material != burntMaterial3)
                                    {
                                        exists = true;
                                        if (tile.material == burntMaterial1)
                                        {
                                            tiles.Remove(tile);
                                        }
                                    }
                                }
                            }
                        }
                        if (!exists)
                        {
                            tiles.Add(new Tile { gameObject = obj2, material = burntMaterial2, spreadTime = spreadTimeBurnt2, spreadTotalTime = 0.0f });
                        }
                        //if (!tiles.Contains(new Tile { gameObject = obj2 }))
                        //{
                        //    tiles.Add(new Tile { gameObject = obj2, material = burntMaterial2, spreadTime = spreadTimeBurnt2, spreadTotalTime = 0.0f });
                        //}
                    }
                }
            }


            foreach (GameObject obj2 in sourceTiles)
            {
                Collider[] sphereHhitColliders = Physics.OverlapSphere(obj2.transform.position, rangeBurnt1 / 2);

                for (int i = 0; i < sphereHhitColliders.Length; ++i)
                {
                    if (sphereHhitColliders[i].gameObject.CompareTag(midgroundTag))
                    {
                        bool exists = false;
                        foreach (Tile tile in tiles)
                        {
                            if (!exists)
                            {
                                if (tile.gameObject == obj2)
                                {
                                    if (tile.material != burntMaterial2 || tile.material != burntMaterial3)
                                    {
                                        exists = true;
                                    }
                                }
                            }
                        }
                        if (!exists)
                        {
                            tiles.Add(new Tile { gameObject = obj2, material = burntMaterial1, spreadTime = spreadTimeBurnt1, spreadTotalTime = 0.0f });
                        }
                        //if (!tiles.Contains(new Tile { gameObject = obj2 }))
                        //{
                        //   tiles.Add(new Tile { gameObject = obj2, material = burntMaterial1, spreadTime = spreadTimeBurnt1, spreadTotalTime = 0.0f });
                        //}
                    }
                }
            }
            objProcessedFires.Add(obj);
        }
        objNewFires.Clear();
    }

    public void ApplyUpdatedTiles()
    {
        foreach(Tile tile in tiles)
        {
            if (tile.spreadTime >= tile.spreadTotalTime && tile.gameObject.GetComponent<Renderer>().material != tile.material)
            {
                tile.gameObject.GetComponent<Renderer>().material = tile.material;
            }
            else if (tile.spreadTime < tile.spreadTotalTime)
            {
                tile.spreadTotalTime += Time.deltaTime;
            }
        }
    }
}

public class Tile
{
    public GameObject gameObject { get; set; }
    public Material material { get; set; }
    public float spreadTotalTime { get; set; }
    public float spreadTime { get; set; }
}
