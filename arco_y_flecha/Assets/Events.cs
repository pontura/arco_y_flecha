using UnityEngine;
using YaguarLib.Audio;

public static class Events
{

    //Audio
    public static System.Action<int, Vector2> AddScore = delegate { };
    public static System.Action<string, Vector2> AddParticle = delegate { };
}