using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MannequinLastLook : MonoBehaviour, IActivable
{
    public void Activate()
    {
        Day2Events.Instance.LastWhispers();
    }
}
