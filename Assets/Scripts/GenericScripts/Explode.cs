using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using Random = UnityEngine.Random;

namespace Assets
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(Collider))]
    public class Explode : MonoBehaviour
    {
        public GameObject ExplodingObject;
        public GameObject Explotion;
        public AudioClip ExplosionSound;
        public AudioClip TimerTickBeepSound;
        public bool ExplodeOnGaze = false;
        public bool ExplodeOnTap = false;
        public List<string> ExplodeOnKeyword;
        public TextMesh Timer;
        public List<GameObject> Debris;
        public int NumberOfDebris;

        private string _formattedTimeLeft;
        private float _timer;
        private int _hoursLeft;
        private int _minutesLeft;
        private int _secondsLeft;
        private bool _timerHasStarted = false;

        private AudioSource _audio;
        private bool _hasBeenPlayed;
        private bool _hasBeenExploded;
        private GameObject _explotion;
        private KeywordRecognizer _keywordRecognizer;
        private Dictionary<string, KeywordAction> _keywordCollection;
        private List<GameObject> _debris;


        delegate void KeywordAction(PhraseRecognizedEventArgs args);

        void Start()
        {
            _timer = 10f;
            _debris = new List<GameObject>();
            SetTimer(_timer);
            _hasBeenExploded = false;
            _keywordCollection = new Dictionary<string, KeywordAction>();
            foreach (string keyWord in ExplodeOnKeyword)
            {
                _keywordCollection.Add(keyWord, OnKeywordRecognized);
            }
            _audio = GetComponent<AudioSource>();
            _audio.clip = TimerTickBeepSound;
            _hasBeenPlayed = false;
            _explotion = (GameObject)Instantiate(Explotion, gameObject.transform.position, Quaternion.identity);

            //foreach (GameObject d in Debris)
            //{
            //    _debris.Add((GameObject)Instantiate(d, gameObject.transform.position, Quaternion.identity));
            //}

        }

        void Update()
        {
            //If the tomer has not yet been started, we don't need to do anything
            if (!_timerHasStarted) return;

            _timer -= Time.deltaTime;
            SetTimer(_timer);
            Timer.text = _formattedTimeLeft;
            if (_timer <= 0 && !_hasBeenExploded)
            {
                TriggerExplotion();
                Timer.text = "00:00:00";
                _timer = 10;
            }
        }

        // Called by GazeGestureManager when the user performs a Select gesture
        void OnGazeEnter()
        {
            if (ExplodeOnGaze && !_hasBeenPlayed)
            {
                StartCoroutine(WaitAndBleep());
                _timerHasStarted = true;
            }
        }

        void OnSelect()
        {
            if (ExplodeOnTap && !_hasBeenPlayed)
            {
                StartCoroutine(WaitAndBleep());
                _timerHasStarted = true;

            }
        }

        private void OnKeywordRecognized(PhraseRecognizedEventArgs args)
        {
            if (ExplodeOnTap && !_hasBeenPlayed)
            {
                StartCoroutine(WaitAndBleep());
                _timerHasStarted = true;
            }
        }

        private void TriggerExplotion()
        {
            _hasBeenExploded = true;
            StopCoroutine(WaitAndBleep());
            MakeDebrisFly();
            GetComponent<Collider>().enabled = false;
            StartCoroutine(PlaySoundAndDestroy());
            Destroy(ExplodingObject);
            _explotion.GetComponent<ParticleSystem>().Play();
            _timerHasStarted = false;
            _hasBeenPlayed = true;
        }

        private void MakeDebrisFly()
        {

            //Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
            //foreach (Collider hit in colliders)
            //{
            //    Rigidbody rb = hit.GetComponent<Rigidbody>();

            //    if (rb != null)
            //        rb.AddExplosionForce(power, explosionPos, radius, 3.0F);

            //}
            foreach (GameObject d in Debris)
            {
                for (int i = 0; i < NumberOfDebris; i++)
                {
                    Debug.Log(d.gameObject.name);
                    Vector3 position = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
                    _debris.Add((GameObject)Instantiate(d, gameObject.transform.position + position, Quaternion.identity));
                }

            }
            foreach (GameObject d in _debris)
            {
                d.GetComponent<Rigidbody>().AddExplosionForce(600, gameObject.transform.position, 10, 20.0f);
            }
        }

        private IEnumerator WaitAndBleep()
        {
            while (true)
            {
                if (_hasBeenExploded) break;

                float waitTime = 1.0f;
                if (_secondsLeft <= 7)
                {
                    waitTime = 0.3f;
                }
                if (_secondsLeft <= 5)
                {
                    waitTime = 0.2f;
                }
                if (_secondsLeft <= 2)
                {
                    waitTime = 0.1f;
                }
                _audio.Play();
                yield return new WaitForSeconds(waitTime);
            }
        }

        void SetTimer(float timer)
        {
            _minutesLeft = Mathf.FloorToInt(timer / 60F);
            _secondsLeft = Mathf.FloorToInt(timer - _minutesLeft * 60);

            _hoursLeft = 00;
            _formattedTimeLeft = String.Format("{0:00}:{1:00}:{2:00}", _hoursLeft, _minutesLeft, _secondsLeft);
        }

        private IEnumerator PlaySoundAndDestroy()
        {
            _audio.Stop();
            _audio.clip = ExplosionSound;
            _audio.Play();
            yield return new WaitForSeconds(ExplosionSound.length);
            Destroy(gameObject);
            Destroy(_explotion);
        }
    }
}