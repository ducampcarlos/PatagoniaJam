using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Day1Events : MonoBehaviour
{
    public static Day1Events Instance { get; private set; }

    [SerializeField] List<Collider> drawerColliders = new List<Collider>();

    [SerializeField] GameObject deactivateSoundsTrigger;

    [SerializeField] List<GameObject> sleepingGas = new List<GameObject>();

    [SerializeField] GameObject player;
    [SerializeField] GameObject pause;
    [SerializeField] GameObject reticle;

    [SerializeField] Volume volume;
    Vignette vignette;

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

    public void ActivateDrawers()
    {
        foreach(var ani in drawerColliders)
        {
            ani.enabled = true;
        }
    }

    public void ActivateH2Sound()
    {
        //Activar sonidos de la puerta H2
        deactivateSoundsTrigger.SetActive(true);
    }

    public void DeactivateH2Sound()
    {
        StartCoroutine(StartSleepingGas());
    }

    IEnumerator StartSleepingGas()
    {
        yield return new WaitForSeconds(2.5f);
        foreach(var obj in sleepingGas)
        {
            obj.SetActive(true);
        }
        yield return new WaitForSeconds(5f);

        ActivatePlayerControl(false);

        if (volume.profile.TryGet<Vignette>(out vignette))
        {
            vignette.active = true;
            vignette.intensity.value = 0.1f;
            float currentFadeTime = 0;
            float fadeDuration = 2;
            while(currentFadeTime < fadeDuration)
            {
                // Increase the fade time
                currentFadeTime += Time.deltaTime;

                // Calculate the current intensity based on the fade time
                float intensity = Mathf.Lerp(0f, 1, currentFadeTime / fadeDuration);

                // Apply the intensity to the vignette effect
                vignette.intensity.value = intensity;
                yield return null;
            }
        }
    }

    void ActivatePlayerControl(bool state)
    {
        player.GetComponent<MouseMovement>().enabled = state;
        player.GetComponent<PlayerMovement>().enabled = state;
        player.GetComponent<ObjectInspection>().enabled = state;
        pause.SetActive(state);
        reticle.SetActive(state);
    }
}
