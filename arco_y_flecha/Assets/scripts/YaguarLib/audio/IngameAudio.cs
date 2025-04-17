using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Networking;
using System.Collections.Generic;

namespace YaguarLib.Audio
{
    public class IngameAudio : MonoBehaviour
    {
        [SerializeField] SoundLibrary soundLibrary;
        [SerializeField] AudioSource source;
        
        public void Play(string key) {
            ClipData cp = soundLibrary.GetClip(key);
            if (cp == null)
                return;
            AudioManager.Instance.PlaySound(source, cp.clip, cp.vol);
        }

        public void PlayOneShot(string key) {
            ClipData cp = soundLibrary.GetClip(key);
            if (cp == null)
                return;
            AudioManager.Instance.PlaySoundOneShot(source, cp.clip, cp.vol);
        }

        public void Play(string key, AudioManager.channels channel, bool loop=false, Action onClipDone=null, bool noRepeat = false) {
            Debug.Log("Play " + key);
            ClipData cp = soundLibrary.GetClip(key);
            if (cp == null) {
                Debug.Log("#NULL");
                return;
            }
            //AudioManager.Instance.PlaySound(cp.clip, sourceName:sourceKey, volume:cp.vol);

            //TO-DO
            AudioManager.Instance.PlaySound(cp.clip, channel, volume: cp.vol, loop:loop, noRepeat: noRepeat);
            if (onClipDone != null)
                StartCoroutine(OnClipDone(cp.clip.length,onClipDone));
        }

        public void PlayMusic(string key) {
            Play(key, AudioManager.channels.MUSIC, true);
        }

        IEnumerator OnClipDone(float clipLength, Action onDone) {
            yield return new WaitForSeconds(clipLength);
            onDone();
        }

        public void CancelAllOnClipDone() {
            StopAllCoroutines();
        }
    }
}
