using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAnimationTrigger : MonoBehaviour
{
    public void ResetTrigger()
    {
        GetComponent<Animator>().ResetTrigger("Move");
    }
}
