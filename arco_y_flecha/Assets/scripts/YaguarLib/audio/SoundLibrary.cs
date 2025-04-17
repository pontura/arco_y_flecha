using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YaguarLib.Audio
{

    [CreateAssetMenu(fileName = "SoundLibrary", menuName = "YaguarLib/SoundLibrary")]
    public class SoundLibrary : ScriptableObject {
        [Header("Audio Clips")]
        public List<ClipData> audioClips;

        [Header("Win Game")]
        public List<ClipData> vo_win;
        int winID = 0;
        public AudioClip GetRandomVOWin()
        {
            if (vo_win == null || vo_win.Count == 0)
                Debug.LogError("No vo win");
            else
            {
                winID++;
                if (winID >= vo_win.Count) winID = 0;
                return vo_win[winID].clip;
            }
            return null;
        }

        [Header("Correct Clips")]
        public List<ClipData> vo_correct;

        int correctID;
        public AudioClip GetRandomVOCorrect()
        {
            if (vo_correct == null || vo_correct.Count == 0)
                Debug.LogError("No vo win");
            else
            {
                correctID++;
                if (correctID >= vo_correct.Count) correctID = 0;
                return vo_correct[correctID].clip;
            }
            return null;
        }

        [Header("Wrong Clips")]
        public List<ClipData> vo_wrong;

        int wrongID;
        public AudioClip GetRandomVOWrong()
        {
            if (vo_wrong == null || vo_wrong.Count == 0)
                Debug.LogError("No vo win");
            else {
                wrongID++;
                if (wrongID >= vo_wrong.Count) wrongID = 0;
                return vo_wrong[wrongID].clip;
            }
            return null;
        }

        [Header("Audio Clip Groups")]
        public List<ClipGroupData> audioClipGroups;

        public ClipData GetClip(string key)
        {
            return audioClips.Find(x => x.key == key);
        }

        public ClipData GetRandomClipFromGroup(string key) {
            return audioClips.Find(x => x.key == key);
        }
    }
}