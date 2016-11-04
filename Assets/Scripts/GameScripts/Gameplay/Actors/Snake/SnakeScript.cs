using UnityEngine;
using System.Collections;
using Assets.Classes;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class SnakeScript : MonoBehaviour
{

    public AudioClip AttackSound;
    public AudioClip HissSound;

    private Animator _animator;                         
    private bool _attackHasPlayed;
    private bool _hissHasPlayed;
    private AudioSource _audioSource;

    void Start () {                                                                                                                       
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    void OnSelect()
    {
        _animator.SetTrigger("Attack");
        //_animator.CrossFade("Idle");
    }


    // Update is called once per frame
    void Update ()
    {
        if (_animator.GetCurrentAnimatorClipInfo(0).Length == 0) return;

        if (_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Rattler_Coil_strike" && !_attackHasPlayed)
        {
            SetAndPlaySoundWithDelay(_audioSource, AttackSound, 1.0f);
            _attackHasPlayed = true;
        }
        else if(_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Rattler_hiss" && !_hissHasPlayed)
        {
            SetAndPlaySoundWithDelay(_audioSource, HissSound, 0.0f);
            _hissHasPlayed = true;
        }
    }

    void SetAndPlaySoundWithDelay(AudioSource source, AudioClip clip, float delay)
    {
        _audioSource.clip = clip;
        _audioSource.PlayDelayed(delay);
    }
}
