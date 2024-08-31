using UnityEngine;

[CreateAssetMenu(fileName = "TextContent", menuName = "IntroText/TextContent", order = 1)]
public class TextContent : ScriptableObject
{
    [TextAreaAttribute]
    public string text1;
    [TextAreaAttribute]
    public string text2;
}
