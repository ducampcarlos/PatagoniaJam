using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager main;

    void Awake()
    {
        if (main == null)
            main = this;
        else
            Destroy(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }

       Play("Loop");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
            return;

        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
            return;

        s.source.Stop();
    }

    public void Mute(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
            return;
        s.source.mute = !s.source.mute;
    }

    public void Volume(string name, float volume)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
            return;
        s.source.volume = volume;
    }

    public void VolumeByType(AudioType type, float volume)
    {
        Sound[] s = Array.FindAll(sounds, sound => sound.type == type);

        if (s == null || s.Length == 0)
            return;

        foreach (var x in s)
            x.source.volume = volume;
    }
}
