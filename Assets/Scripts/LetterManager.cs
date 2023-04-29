using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LetterManager : MonoBehaviour
{
    [SerializeField] Image                  BGLetter;
    [SerializeField] TextMeshProUGUI        textLetter;
    LetterSO                                myLetterSO;

    public bool SetLetterSO(LetterSO newLetterSO, bool canRead)
    {
        if (canRead)
        {
            myLetterSO = newLetterSO;
            ReadLetter();
            return true;
        }
        return false;
    }

    public void ReadLetter()
    {
        BGLetter.sprite = myLetterSO.BGLetter;
        textLetter.text = myLetterSO.textLetter;
        gameObject.SetActive(true);
    }
}
