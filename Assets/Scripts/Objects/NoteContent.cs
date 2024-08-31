using UnityEngine;

[CreateAssetMenu(fileName = "NoteContent", menuName = "Notes/NoteContent", order = 1)]
public class NoteContent : ScriptableObject
{
    [TextAreaAttribute]
    public string bodyText;
}
