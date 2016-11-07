using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SpawnerButton : MonoBehaviour
{


    public GameObject ObjectToSpawn;
    public GameObject Parent;
    public GameObject QuickShowGameObject;
    public GameObject SpawnLocation;
	// Use this for initialization

    private GameObject _spawnedGameObject;
    private GameObject _spawnedQuickShowGameObject;
    private Button _spawnerButton;

    private float _quickShowDelay = 0.3f;

	void Start ()
	{
	    _spawnerButton = gameObject.GetComponent<Button>();
     //   _spawnedQuickShowGameObject = (GameObject)Instantiate(ObjectToSpawn, QuickShowGameObject.transform.position, QuickShowGameObject.transform.rotation);
	    //_spawnedQuickShowGameObject.transform.parent = QuickShowGameObject.transform;
     //   _spawnedQuickShowGameObject.SetActive(false);
    }
	

    void OnGazeEnter()
    {
        _spawnerButton.Select();
        //_spawnedQuickShowGameObject.SetActive(true);


    }

    void OnGazeLeave()
    {
        //_spawnedQuickShowGameObject.SetActive(false);

    }

    void OnSelected()
    {
        SpawnObject();
    }


    private void QuickShow()
    {
        
    }

    void SpawnObject()
    {

        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + -0.2f);

        if (SpawnLocation) spawnPosition = SpawnLocation.transform.position;

        _spawnedGameObject = (GameObject)Instantiate(ObjectToSpawn, spawnPosition, transform.rotation);

        _spawnedGameObject.transform.parent = Parent.transform;

    }
}
