using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H2RoomTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Day2Events.Instance.ActivateH2Room();
            Destroy(gameObject);
        }
    }
}
