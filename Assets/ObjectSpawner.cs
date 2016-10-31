using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets;

public class ObjectSpawner : MonoBehaviour
{

    public List<GameObject> ObjectsToSpawn;
    public GameObject ParentToSpawnOn;
    public bool SpawnOnGaze = false;
    public bool SpawnOnTap = false;
    public bool SpawnOnKeyword = false;
    public float SpawnOffset;


    private GameObject _spawnedGameObject;

    // Use this for initialization
    void Start () {
        if (ObjectsToSpawn.Count < 1)
        {
            Debug.LogError("You need to have a game object attached to the scipt so what we can spawn it for you");
        }
    }


    void OnGazeEnter()
    {
        if (SpawnOnGaze)
        {
            SpawnObject();
        }
    }

    void OnSelect()
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
        _spawnedGameObject.transform.parent = ParentToSpawnOn.transform;
        //float spawnerSize = transform.GetComponent<Collider>().bounds.size.y;
        //float objectSize = spawnedObject.GetComponent<Collider>().bounds.size.y;
        //Debug.Log(spawnerSize + " " + objectSize);
        //float offset = objectSize*2 + (spawnerSize/2);
        //Debug.Log(offset);

        //spawnedObject.GetComponent<Renderer>().enabled = false;
        ////spawnedObject.GetComponent<Collider>().enabled = false;
        //spawnedObject.GetComponent<Collider>().enabled = true;

        //Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
        //spawnedObject.GetComponent<Renderer>().enabled = true;
        //Debug.Log(spawnPosition);

        //spawnedObject.transform.position = spawnPosition;

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
