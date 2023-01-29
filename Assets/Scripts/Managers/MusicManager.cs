using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class MusicManager : MonoBehaviour
    {
        [SerializeField]
        private AudioSource _audio_source;
        [SerializeField]
        private List<AudioClip> _clips;

        private void Awake()
        {
            _audio_source = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _audio_source.clip = _clips[0];
            _audio_source.Play();
            StartCoroutine(RaiseVolumeCoroutine());
        }

        public void DecreaseVolume()
        {
            StartCoroutine(DecreaseVolumeCoroutine());
        }

        public void SwitchClip(int index)
        {
            StartCoroutine(SwitchClipCoroutine(index));
        }

        private IEnumerator SwitchClipCoroutine(int index)
        {
            yield return StartCoroutine(DecreaseVolumeCoroutine());
            _audio_source.Stop();
            _audio_source.clip = _clips[index];
            _audio_source.Play();
            yield return StartCoroutine(RaiseVolumeCoroutine());
        }

        private IEnumerator RaiseVolumeCoroutine()
        {
            float step = 0.1f;
            for (int i = 0; i <= 10f; i++)
            {
                _audio_source.volume += step;
                yield return new WaitForSeconds(step);
            }
        }

        private IEnumerator DecreaseVolumeCoroutine()
        {
            float step = 0.1f;
            for (int i = 0; i <= 10f; i++)
            {
                _audio_source.volume -= step;
                yield return new WaitForSeconds(step);
            }
        }
    }
}