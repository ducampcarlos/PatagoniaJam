using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorPassed : MonoBehaviour
{
    [SerializeField] bool passedDoor;
    [SerializeField] ObjectOnScreenDetector obj;
    [SerializeField] GameObject otherTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            obj.EnableDoorDisappear(passedDoor);
            otherTrigger.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
