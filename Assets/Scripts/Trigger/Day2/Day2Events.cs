using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day2Events : MonoBehaviour
{
    public static Day2Events Instance;
    public GameObject mannequinDoor;
    [SerializeField] List<GameObject> mannequinTriggers = new List<GameObject>();
    [SerializeField] GameObject H2Trigger;
    [SerializeField] GameObject H2Light;
    [SerializeField] GameObject H2Mannequin;

    [SerializeField] GameObject player;
    [SerializeField] GameObject pause;
    [SerializeField] GameObject reticle;

    [SerializeField] CameraTransition camt;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartMannequinSounds()
    {
        //Sonidos de Maniqui

        mannequinDoor.GetComponent<Animator>().SetTrigger("QuickOpen");

        StartCoroutine(ActivateMannequinCrawl());
    }

    IEnumerator ActivateMannequinCrawl()
    {
        yield return new WaitForSeconds(1.2f);
        foreach(GameObject obj in mannequinTriggers)
        {
            obj.SetActive(true);
        }
    }

    public void ShutMannequinDoor(bool setSecondTrigger)
    {
        mannequinDoor.GetComponent<Animator>().SetTrigger("Shut");
        mannequinDoor.tag = "Door";

        if (setSecondTrigger)
            H2Trigger.SetActive(true);
    }

    public void ActivateH2Room()
    {
        ShutMannequinDoor(false);
        mannequinDoor.tag = "Untagged";
        H2Light.SetActive(true);
        H2Mannequin.SetActive(true);
    }

    public void LastWhispers()
    {
        ActivatePlayerControl(false);
        camt.enabled = true;
    }

    void ActivatePlayerControl(bool state)
    {
        if (!state)
            player.GetComponent<ObjectInspection>().StopInspectionExternally();
        player.GetComponent<MouseMovement>().enabled = state;
        player.GetComponent<PlayerMovement>().enabled = state;
        player.GetComponent<ObjectInspection>().enabled = state;
        pause.SetActive(state);
        reticle.SetActive(state);
    }
}
