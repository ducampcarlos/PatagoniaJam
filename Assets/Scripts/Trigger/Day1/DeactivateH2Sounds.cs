using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateH2Sounds : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Day1Events.Instance.DeactivateH2Sound();
        Destroy(gameObject);
    }
}
