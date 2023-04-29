using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "new LetterSO", menuName = "LetterSO")]
public class LetterSO : ScriptableObject
{
    public Sprite BGLetter;
    [TextArea(minLines:3, maxLines:10)]public string textLetter;
}
