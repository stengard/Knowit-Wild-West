using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

namespace Assets
{
    [RequireComponent(typeof(AudioSource))]
    public class Explode : MonoBehaviour
    {
        public GameObject ExplodingObject;
        public GameObject Explotion;
        public AudioClip ExplosionSound;
        public bool ExplodeOnGaze = false;
        public bool ExplodeOnTap = false;
        public List<string> ExplodeOnKeyword;

        private AudioSource _audio;
        private bool _hasBeenPlayed;
        private  GameObject _explotion;
        private KeywordRecognizer _keywordRecognizer;
        private Dictionary<string, KeywordAction> _keywordCollection;


        delegate void KeywordAction(PhraseRecognizedEventArgs args);

        void Start()
        {
            _keywordCollection = new Dictionary<string, KeywordAction>();
            foreach (string keyWord in ExplodeOnKeyword) 
            {
                _keywordCollection.Add(keyWord, OnKeywordRecognized);
            }
            _audio = GetComponent<AudioSource>();
            _audio.clip = ExplosionSound;
            _hasBeenPlayed = false;
            _explotion = (GameObject)Instantiate(Explotion, gameObject.transform.position, Quaternion.identity);
        }

        // Called by GazeGestureManager when the user performs a Select gesture
        void OnGazeEnter()
        {
            if (ExplodeOnGaze && !_hasBeenPlayed)
            {
                TriggerExplotion();
            }
        }

        void OnSelect()
        {
            if (ExplodeOnTap && !_hasBeenPlayed)
            {
                TriggerExplotion();
            }
        }

        private void OnKeywordRecognized(PhraseRecognizedEventArgs args)
        {
            if (ExplodeOnTap && !_hasBeenPlayed)
            {
                TriggerExplotion();
            }
        }

        private void TriggerExplotion()
        {
            StartCoroutine(PlaySoundAndDestroy());
            Destroy(ExplodingObject);
            _explotion.GetComponent<ParticleSystem>().Play();
            _hasBeenPlayed = true;
        }

        private IEnumerator PlaySoundAndDestroy()
        {
            _audio = GetComponent<AudioSource>();
            _audio.clip = ExplosionSound;
            _audio.Play();
            yield return new WaitForSeconds(_audio.clip.length);
            Destroy(gameObject);
            Destroy(_explotion);
        }
    }
}