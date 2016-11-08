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
    public Text SizeTextObject;
    private float _currentSize;
    private Vector3 _originalSize;
	void Start ()
	{
	    
        _spawnerButton = gameObject.GetComponent<Button>();
        _spawnedQuickShowGameObject = (GameObject)Instantiate(ObjectToSpawn, QuickShowGameObject.transform.position + QuickShowGameObject.transform.up * 0.15f, QuickShowGameObject.transform.rotation);
        _originalSize = ObjectToSpawn.transform.localScale;
        SetSize(_spawnedQuickShowGameObject);
        _spawnedQuickShowGameObject.transform.parent = QuickShowGameObject.transform;
        SetSize(_spawnedQuickShowGameObject);

        //If we have a Placebla script attached to the quick show object. We Need to disable the script. Otherwise it screw up the rotation.... (for some reason.) Quick fix.
        if (_spawnedQuickShowGameObject.GetComponent<Placeable>()) _spawnedQuickShowGameObject.GetComponent<Placeable>().enabled = false;

        _spawnedQuickShowGameObject.SetActive(false);
    }
	

    void OnGazeEnter()
    {
        _spawnerButton.Select();
        _spawnedQuickShowGameObject.SetActive(true);
        SetSize(_spawnedQuickShowGameObject);
    }

    void OnGazeLeave()
    {
        _spawnedQuickShowGameObject.SetActive(false);
    }

    void OnSelected()
    {
        _spawnedQuickShowGameObject.SetActive(false);
        SpawnObject();
    }


    private void QuickShow()
    {
        
    }

    void SpawnObject()
    {

        
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + 0.15f, transform.position.z + -0.2f);

        if (SpawnLocation) spawnPosition = SpawnLocation.transform.position + SpawnLocation.transform.up*0.15f;

        _spawnedGameObject = (GameObject)Instantiate(ObjectToSpawn, spawnPosition, transform.rotation);
        SetSize(_spawnedGameObject);
        _spawnedGameObject.transform.parent = Parent.transform;

    }

    void SetSize(GameObject go)
    {
        float.TryParse(SizeTextObject.text, out _currentSize);
        go.transform.localScale = _originalSize * _currentSize;
    }
}
