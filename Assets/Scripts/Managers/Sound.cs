using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3)]
    public float pitch;

    public bool loop;

    [Header("BGM/SFX/UI")]
    public AudioType type;

    [HideInInspector]
    public AudioSource source;
}
