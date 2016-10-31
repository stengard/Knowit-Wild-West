using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameScripts.Gameplay.Items.Radio
{
    [RequireComponent(typeof(AudioSource))]
    public class Radio : MonoBehaviour
    {

        public List<AudioClip> RadioSongs;



        private AudioSource _audioSource;
        private bool _isPlaying = true;
        private int _currentlyPlayingIndex;
        // Use this for initialization
        void Start()
        {
            if (RadioSongs.Count < 1)
            {
                Debug.LogError("You need to have at least one song in the RadioSongs List");
                return;
            }

            _currentlyPlayingIndex = 0;
            _audioSource = GetComponent<AudioSource>();
            _audioSource.clip = RadioSongs[_currentlyPlayingIndex];
            _audioSource.Play();
            _isPlaying = _audioSource.isPlaying;
        }

        void OnSelect()
        {
            //_isPlaying = !_isPlaying;
            //PlayOrPause();
            PlayNext();

        }

        public void OnHoldStarted()
        {

        }



        public void OnHoldCompleted()
        {
            //PlayNext();
        }


        private void PlayNext()
        {
            if (_currentlyPlayingIndex == RadioSongs.Count - 1)
            {
                _currentlyPlayingIndex = 0;
            }
            else
            {
                _currentlyPlayingIndex++;
            }

            _audioSource.clip = RadioSongs[_currentlyPlayingIndex];
            _audioSource.Play();
        }

        void PlayOrPause()
        {
            _audioSource.mute = !_isPlaying;
        }


    }
}
