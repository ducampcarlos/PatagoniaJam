using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    [HideInInspector]
    public string textContent;
    public NoteContent noteContent;

    private void Awake()
    {
        textContent = noteContent.bodyText;
    }
}
