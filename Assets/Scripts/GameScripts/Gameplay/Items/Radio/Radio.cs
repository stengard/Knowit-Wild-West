using UnityEngine;

namespace Assets.Scripts.GameScripts.Gameplay.Items.Radio
{
    public class Radio : MonoBehaviour {

        private AudioSource _audio;
        private bool _isPlaying = true;
        // Use this for initialization
        void Start () {
            _audio = GetComponent<AudioSource>();
        }

        void OnSelect()
        {
            _isPlaying = !_isPlaying;
            PlayOrPause();
        }


        void PlayOrPause()
        {
            _audio.mute = _isPlaying;
        }


    }
}
