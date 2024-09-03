using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateH2Sounds : MonoBehaviour, IActivable
{
    private void OnTriggerEnter(Collider other)
    {
        SoundManager.main.Stop("Blows");
    }
    public void Activate()
    {
        SoundManager.main.Play("Alarm");
        Day1Events.Instance.DeactivateH2Sound();
        gameObject.tag = "Untagged";
        Destroy(this);
    }
}
