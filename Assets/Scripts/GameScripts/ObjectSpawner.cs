using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine.EventSystems;

public class ObjectSpawner : MonoBehaviour
{

    public List<GameObject> ObjectsToSpawn;
    public GameObject ParentToSpawnOn;
    public bool SpawnOnGaze = false;
    public bool SpawnOnTap = false;
    public bool SpawnOnKeyword = false;
    public float SpawnOffset;
    public float MinSizeAdd = 0;
    public float MaxSizeAdd = 0;


    private GameObject _spawnedGameObject;

    // Use this for initialization
    void Start () {
        if (ObjectsToSpawn.Count < 1)
        {
            Debug.LogError("You need to have a game object attached to the scipt so what we can spawn it for you");
        }

        if (ParentToSpawnOn == null)
        {
            ParentToSpawnOn = GameObject.Find("Items");
        }
    }


    void OnGazeEnter()
    {
        if (SpawnOnGaze)
        {
            SpawnObject();
        }
    }

    void OnSelected()
    {
        if (SpawnOnTap)
        {
            SpawnObject();
        }
    }

    void SpawnObject()
    {
        var spawnerSize = transform.GetComponent<Collider>().bounds.size.y;
        double offset = (double)spawnerSize/2 + (double)SpawnOffset;
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + (float)offset, transform.position.z);
        RemoveNonMovedChildren();

        _spawnedGameObject = (GameObject)Instantiate(ObjectsToSpawn[Random.Range(0, ObjectsToSpawn.Count)], spawnPosition, transform.rotation);

        float sizeToAdd = Random.Range(MinSizeAdd, MaxSizeAdd);
        _spawnedGameObject.transform.localScale += new Vector3(sizeToAdd, sizeToAdd, sizeToAdd);
        _spawnedGameObject.transform.parent = ParentToSpawnOn.transform;
    }


    private void RemoveNonMovedChildren()
    {
        foreach (Transform child in transform.parent)
        {
            if (!child.CompareTag(TagHelper.MOVED_TAG) && child != gameObject.transform)
                Destroy(child.gameObject);
                
        }
    }
}
