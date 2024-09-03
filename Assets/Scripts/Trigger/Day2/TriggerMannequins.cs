using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMannequins : MonoBehaviour
{
    public float speed = 5f;
    public bool shouldMove = false;
    private GameObject triggerZone;
    public GameObject skin;

    void Start()
    {
        triggerZone = transform.GetChild(0).gameObject;
        // Puedes subscribirte al evento en el Start o directamente en el Inspector si usas Event
    }

    void Update()
    {
        if (shouldMove)
        {
            transform.position += Vector3.back * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("Player"))
        {
            SoundManager.main.Stop("Cracking");
            skin.SetActive(true);
            shouldMove = true;
            triggerZone.SetActive(false);
        }

        if (other.gameObject.CompareTag("Deactivable"))
        {
            Day2Events.Instance.ShutMannequinDoor(true);
            gameObject.SetActive(false);
            other.gameObject.SetActive(false);

        }
    }

}
