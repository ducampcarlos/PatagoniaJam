using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyGet : MonoBehaviour, IActivable
{
    public void Activate()
    {
        Day1Events.Instance.ActivateDrawers();
        Destroy(gameObject);
    }
}
