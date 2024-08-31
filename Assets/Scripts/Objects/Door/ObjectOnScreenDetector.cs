using UnityEngine;
using System.Collections;

public class ObjectOnScreenDetector : MonoBehaviour
{
    bool visible;

    public bool triggerDoorDisappear = false;
    private bool executed = false;

    [SerializeField] GameObject disappearDoor;
    [SerializeField] GameObject appearWall;

    void OnBecameVisible()
    {
        visible = true;
    }

    private void OnBecameInvisible()
    {
        visible = false;
    }

    void Update()
    {
        if (triggerDoorDisappear && !executed)
        {
            if (!visible)
            {
                executed = true;
                disappearDoor.SetActive(false);
                appearWall.SetActive(true);
            }
        }
    }

    public void EnableDoorDisappear(bool state)
    {
        triggerDoorDisappear = state;
    }
}
