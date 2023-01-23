using System;
using DG.Tweening;
using UnityEngine;
using Utility;

namespace Sound
{
    public class AudioManager : Instancable<AudioManager>
    {
        #region Private Properties
        [SerializeField] private Sound[] musicSounds;
        [SerializeField] private AudioSource musicSource, sfxSource;
        [SerializeField] private float delayAmount = 0.1f;
        #endregion
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            PlayMusic("Theme");
        }

        public void PlayMusic(string name)
        {
            Sound sound = Array.Find(musicSounds, x => x.name == name);

            if (sound == null)
            {
                Debug.Log("Sound Not Found");
            }

            else
            {
                musicSource.clip = sound.clip;
                musicSource.Play();
            }
        }

        public void PlaySFX(string name)
        {
            Sound sound = Array.Find(musicSounds, x => x.name == name);

            if (sound == null)
            {
                Debug.Log("Sound Not Found");
            }

            else
            {
                sfxSource.PlayOneShot(sound.clip);
            }
        }
        
        public void PlayDelayedSFX(string name)
        {
            DOVirtual.DelayedCall(delayAmount, () => PlaySFX(name));
        }
    }
}
