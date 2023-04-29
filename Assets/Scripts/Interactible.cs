using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    [SerializeField] LetterSO   letter;
    [SerializeField] int        index = 0;
    LetterManager               letterManager;

    void Awake()
    {
        letterManager = FindObjectOfType<LetterManager>(true);
    }

    public bool OnInteract(int lettersRead)
    {
        if (letterManager.SetLetterSO(letter, lettersRead >= index))
        {
            return true;
        }
        return false;
    }

    public int GetIndex()
    {
        return index;
    }

    public void StopReading()
    {
        letterManager.gameObject.SetActive(false);
    }
}
