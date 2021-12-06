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

    [SerializeField] GameObject fireManager = null;

    private List<GameObject> objProcessedFires = new List<GameObject>();
    private List<GameObject> objNewFires = new List<GameObject>();

    private List<Tile> tiles = new List<Tile>();

    [SerializeField] private float rangeBurnt1 = 4.0f;
    [SerializeField] private float spreadTimeBurnt1 = 4.5f;
    [SerializeField] private Material burntMaterial1 = null;
    [SerializeField] private float rangeBurnt2 = 2.0f;
    [SerializeField] private float spreadTimeBurnt2 = 3.0f;
    [SerializeField] private Material burntMaterial2 = null;
    [SerializeField] private float rangeBurnt3 = 0.75f;
    [SerializeField] private float spreadTimeBurnt3 = 1.0f;
    [SerializeField] private Material burntMaterial3 = null;

    private string midgroundTag = "Midground";

    private float midgroundUpdateTotalTime = 2.0f;
    private float midgroundUpdateTimer = 0.0f;

    private float materialUpdateTotalTime = 0.1f;
    private float materialUpdateTimer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (midgroundUpdateTimer >= midgroundUpdateTotalTime)
        {
            fireManager.GetComponent<FireManager>().UpdateMidground();
            midgroundUpdateTimer = 0.0f;
        }
        else
        {
            midgroundUpdateTimer += Time.deltaTime;
        }


        if (materialUpdateTimer >= materialUpdateTotalTime)
        {
            ApplyUpdatedTiles();
            materialUpdateTimer = 0.0f;
        }
        else
        {
            materialUpdateTimer += Time.deltaTime;
        }
    }

    public void UpdateFires(List<GameObject> _objFires)
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
        }
    }

    private void ProcessNewFires()
    {
        foreach (GameObject obj in objNewFires)
        {
            List<GameObject> sourceTiles = new List<GameObject>();

            Vector3 v3ObjBoxExtents = obj.GetComponent<Collider>().bounds.extents;
            Vector3 v3SearchBoxExtents = new Vector3(v3ObjBoxExtents.x + rangeBurnt3, v3ObjBoxExtents.y, v3ObjBoxExtents.z + rangeBurnt3);
            Vector3 v3SearchBoxOrigin = new Vector3(obj.transform.position.x, obj.transform.position.y - (v3ObjBoxExtents.y / 2.0f), obj.transform.position.z);
            
            Collider[] objBoxHitColliders = Physics.OverlapBox(v3SearchBoxOrigin, v3SearchBoxExtents / 2);

            for (int i = 0; i < objBoxHitColliders.Length; ++i)
            {
                GameObject obj2 = objBoxHitColliders[i].gameObject;
                if (obj2.CompareTag(midgroundTag))
                {
                    List<Tile> currentTiles = tiles;
                    bool exists = false;
                    foreach (Tile tile in currentTiles.ToArray())
                    {
                        if (!exists)
                        {
                            if (tile.gameObject == obj2)
                            {
                                exists = true;
                                if (tile.material != burntMaterial3)
                                {
                                    if (tile.material == burntMaterial1 || tile.material == burntMaterial2)
                                    {
                                        tiles.Remove(tile);
                                    }
                                    AddTile(obj2, burntMaterial3, spreadTimeBurnt3, 0.0f);
                                    sourceTiles.Add(obj2);
                                }
                            }
                        }
                    }
                    if (!exists)
                    {
                        AddTile(obj2, burntMaterial3, spreadTimeBurnt3, 0.0f);
                        sourceTiles.Add(obj2);
                    }
                }
            }

            foreach (GameObject objTile in sourceTiles)
            {
                Vector3 v3TileBoxExtents = objTile.GetComponent<Collider>().bounds.extents;
                Vector3 v3SearchTileExtents = new Vector3(v3TileBoxExtents.x + rangeBurnt2, v3TileBoxExtents.y + rangeBurnt2, v3TileBoxExtents.z + rangeBurnt2);
                Vector3 v3SearchTileOrigin = objTile.transform.position;

                Collider[] objTileHhitColliders = Physics.OverlapBox(v3SearchTileOrigin, v3SearchTileExtents / 2);

                for (int i = 0; i < objTileHhitColliders.Length; ++i)
                {
                    if (objTileHhitColliders[i].gameObject.CompareTag(midgroundTag))
                    {
                        List<Tile> currentTiles = tiles;
                        bool exists = false;
                        foreach (Tile tile in currentTiles.ToArray())
                        {
                            if (!exists)
                            {
                                if (tile.gameObject == objTileHhitColliders[i].gameObject)
                                {
                                    exists = true;
                                    if (tile.material != burntMaterial3)
                                    {
                                        if (tile.material == burntMaterial1)
                                        {
                                            tiles.Remove(tile);
                                        }
                                        AddTile(objTileHhitColliders[i].gameObject, burntMaterial2, spreadTimeBurnt2, 0.0f);
                                    }

                                }
                            }
                        }
                        if (!exists)
                        {
                            AddTile(objTileHhitColliders[i].gameObject, burntMaterial2, spreadTimeBurnt2, 0.0f);
                        }
                    }
                }
            }


            foreach (GameObject objTile in sourceTiles)
            {
                Vector3 v3TileBoxExtents = objTile.GetComponent<Collider>().bounds.extents;
                Vector3 v3SearchTileExtents = new Vector3(v3TileBoxExtents.x + rangeBurnt1, v3TileBoxExtents.y + rangeBurnt1, v3TileBoxExtents.z + rangeBurnt1);
                Vector3 v3SearchTileOrigin = objTile.transform.position;

                Collider[] objTileHhitColliders = Physics.OverlapBox(v3SearchTileOrigin, v3SearchTileExtents / 2);

                for (int i = 0; i < objTileHhitColliders.Length; ++i)
                {
                    if (objTileHhitColliders[i].gameObject.CompareTag(midgroundTag))
                    {
                        List<Tile> currentTiles = tiles;
                        bool exists = false;
                        foreach (Tile tile in currentTiles.ToArray())
                        {
                            if (!exists)
                            {
                                if (tile.gameObject == objTileHhitColliders[i].gameObject)
                                {
                                    exists = true;
                                    if (tile.material != burntMaterial2 && tile.material != burntMaterial3)
                                    {
                                        AddTile(objTileHhitColliders[i].gameObject, burntMaterial1, spreadTimeBurnt1, 0.0f);
                                    }
                                }
                            }
                        }
                        if (!exists)
                        {
                            AddTile(objTileHhitColliders[i].gameObject, burntMaterial1, spreadTimeBurnt1, 0.0f);
                        }
                    }
                }
            }
            objProcessedFires.Add(obj);
        }
        objNewFires.Clear();
    }

    private void ApplyUpdatedTiles()
    {
        foreach(Tile tile in tiles)
        {
            if (tile.spreadTimer >= tile.spreadTotalTime && tile.gameObject.GetComponent<Renderer>().material != tile.material)
            {
                tile.gameObject.GetComponent<Renderer>().material = tile.material;
            }
            else if (tile.spreadTimer < tile.spreadTotalTime)
            {
                tile.spreadTimer += Time.deltaTime + materialUpdateTimer;
            }
        }
    }

    private void AddTile(GameObject _obj, Material _mat, float _totalTime, float _timer)
    {
        tiles.Add(new Tile { gameObject = _obj, material = _mat, spreadTotalTime = _totalTime, spreadTimer = _timer });
    }
}

public class Tile
{
    public GameObject gameObject { get; set; }
    public Material material { get; set; }
    public float spreadTotalTime { get; set; }
    public float spreadTimer { get; set; }
}
