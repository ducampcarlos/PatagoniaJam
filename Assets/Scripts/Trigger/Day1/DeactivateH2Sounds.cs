using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateH2Sounds : MonoBehaviour, IActivable
{
    public void Activate()
    {
        Day1Events.Instance.DeactivateH2Sound();
        gameObject.tag = "Untagged";
        Destroy(this);
    }
}
