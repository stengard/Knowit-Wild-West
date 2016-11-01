using System;
using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;
using Random = UnityEngine.Random;

namespace Assets.Scripts.GenericScripts
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
        public bool ExplodeOnKeyword = false;

        public Text TimerText;
        public List<GameObject> Debris;
        public int NumberOfDebris;
        public float SecondsToExplode;
        public float ExplosionRadius;
        public float ExplosionForceNewton;
        public float DebrisSpawnRadius;


        private string _formattedTimeLeft;
        private int _hoursLeft;
        private int _minutesLeft;
        private int _secondsLeft;
        private bool _timerHasStarted = false;

        private AudioSource _audio;
        private bool _hasBeenPlayed;
        private bool _hasBeenExploded;
        private GameObject _explotion;

        private List<GameObject> _debris;


        void Start()
        {
            _debris = new List<GameObject>();
            TimerText.text = GetFormattedTimer(SecondsToExplode);
            _hasBeenExploded = false;
            _audio = GetComponent<AudioSource>();
            _audio.clip = TimerTickBeepSound;
            _hasBeenPlayed = false;
            _explotion = (GameObject)Instantiate(Explotion, gameObject.transform.position, Quaternion.identity);
            _explotion.transform.parent = gameObject.transform;
        }

        void Update()
        {
 
            //If the tomer has not yet been started, we don't need to do anything
            if (!_timerHasStarted) return;

            SecondsToExplode -= Time.deltaTime;

            TimerText.text = GetFormattedTimer(SecondsToExplode); 

            if (!(SecondsToExplode <= 0) || _hasBeenExploded) return;
            TriggerExplotion();
            TimerText.text = "00:00:00";
            SecondsToExplode = 10;
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

        public void OnBlowUpRecognized()
        {
            var focusObject = GestureManager.Instance.FocusedObject;

            if (focusObject == gameObject && ExplodeOnKeyword && !_hasBeenPlayed)
            {
                StartCoroutine(WaitAndBleep());
                _timerHasStarted = true;
            }
        }

        private void RemoveVertices(IEnumerable<GameObject> boundingObjects)
        {
            RemoveSurfaceVertices removeVerts = RemoveSurfaceVertices.Instance;
            if (removeVerts != null && removeVerts.enabled)
            {
                removeVerts.RemoveSurfaceVerticesWithinBounds(boundingObjects);
            }
        }

        private void TriggerExplotion()
        {
            List<GameObject> test = new List<GameObject> { gameObject };
            RemoveVertices(test);
            _hasBeenExploded = true;
            StopCoroutine(WaitAndBleep());
            MakeDebrisFly();
            TimerText.enabled = false;
            GetComponent<Collider>().enabled = false;
            StartCoroutine(PlaySoundAndDestroy());
            Destroy(ExplodingObject);
            _explotion.GetComponent<ParticleSystem>().Play();
            _timerHasStarted = false;
            _hasBeenPlayed = true;
        }

        private void MakeDebrisFly()
        {
            //Spawn Debris

            for (int i = 0; i < NumberOfDebris; i++)
            {
                Vector3 position = new Vector3(Random.Range(-DebrisSpawnRadius, DebrisSpawnRadius), Random.Range(0, DebrisSpawnRadius*2), Random.Range(-DebrisSpawnRadius, DebrisSpawnRadius));
                _debris.Add((GameObject)Instantiate(Debris[Random.Range(0, Debris.Count)], gameObject.transform.position + position, Quaternion.identity));
            }
            

            Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, ExplosionRadius);

            foreach (Collider c in colliders)
            {        
                Rigidbody rb = c.GetComponent<Rigidbody>();
                //if (rb == null) c.gameObject.AddComponent<Rigidbody>();
                if (rb != null)
                    rb.AddExplosionForce(ExplosionForceNewton, gameObject.transform.position, ExplosionRadius, 2.0F);
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

        private string  GetFormattedTimer(float timer)
        {
            _minutesLeft = Mathf.FloorToInt(timer / 60F);
            _secondsLeft = Mathf.FloorToInt(timer - _minutesLeft * 60);

            _hoursLeft = 00;
            return String.Format("{0:00}:{1:00}:{2:00}", _hoursLeft, _minutesLeft, _secondsLeft);
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