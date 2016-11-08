using UnityEngine;
using System.Collections;

public class Killable : MonoBehaviour
{


    public AudioClip EnemyHitSound;
    public AudioClip SecondHitSound;
    public GameObject BloodSplash;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public IEnumerator KillEnemyAndPlaySound(GameObject enemyGameObject)
    {
        if (enemyGameObject.gameObject.GetComponent<AudioSource>() == null)
        {
            enemyGameObject.gameObject.AddComponent<AudioSource>();
        }
        AudioSource objectAudioSource = enemyGameObject.gameObject.GetComponent<AudioSource>();
        float delay = 0.04f;
        objectAudioSource.spatialize = true;
        objectAudioSource.PlayOneShot(EnemyHitSound);

        GameObject blood = (GameObject)Instantiate(BloodSplash, transform.position, transform.rotation);
        foreach (var childRenderer in enemyGameObject.GetComponentsInChildren<Renderer>())
        {
            childRenderer.enabled = false;
        }
        yield return new WaitForSeconds(EnemyHitSound.length + delay);
        Destroy(enemyGameObject);
        yield return new WaitForSeconds(5.0f);
        Destroy(blood);
    }
}
