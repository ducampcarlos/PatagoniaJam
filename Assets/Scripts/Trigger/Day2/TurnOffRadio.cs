using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffRadio : MonoBehaviour, IActivable
{
    public AudioSource _source;
    public void Activate()
    {
        //Apagar radio
        _source.enabled = false;
        Debug.Log("Apagar Radio");
        SoundManager.main.Play("OffRadio");
        tag = "Untagged";
        Day2Events.Instance.StartMannequinSounds();
        SoundManager.main.Play("Cracking");
    }
}
