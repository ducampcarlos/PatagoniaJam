using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffRadio : MonoBehaviour, IActivable
{
    public void Activate()
    {
        //Apagar radio
        Debug.Log("Apagar Radio");
        tag = "Untagged";
        Day2Events.Instance.StartMannequinSounds();
    }
}
