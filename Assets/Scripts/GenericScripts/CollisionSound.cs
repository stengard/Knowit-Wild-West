using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class CollisionSound : MonoBehaviour
{

    public List<AudioClip> CollisionSounds;

    private AudioSource _audioSource;
	// Use this for initialization
	void Awake ()
	{
	    if (!ShouldRun())
	    {
	        Debug.LogError("You need to specify at least one collision sounn in the 'CollisionSounds' List");
	        return;
	    }
	    _audioSource = GetComponent<AudioSource>();
	}
	
    void OnCollisionEnter(Collision col)
    {
        if (!ShouldRun()) return;
        _audioSource.PlayOneShot(GetRandomSound());
    }

    private AudioClip GetRandomSound()
    {
        return CollisionSounds[Random.Range(0,CollisionSounds.Count-1)];
    }

    private bool ShouldRun()
    {
        return (CollisionSounds.Count > 0);
    }
}
