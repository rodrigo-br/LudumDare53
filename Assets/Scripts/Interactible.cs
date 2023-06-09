using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactible : MonoBehaviour
{
    [SerializeField] Sprite     objectToRender;
    [SerializeField] LetterSO   letter;
    [SerializeField] int        index = 0;
    PlayerMovement              player;
    LetterManager               letterManager;
    SpriteRenderer              mySpriteRender;

    void Awake()
    {
        letterManager = FindObjectOfType<LetterManager>(true);
        player = FindObjectOfType<PlayerMovement>();
        mySpriteRender = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        FindIndex();
    }

    void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name == "Scene 1" && player.GetLettersRead() >= 5 && index != 5)
        {
            FindObjectOfType<Interactible>(true).gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        player.OnReadLetter += FindIndex;
    }

    void FindIndex()
    {
        if (player.GetLettersRead() == index)
        {
            if (objectToRender != null && mySpriteRender != null)
            {
                mySpriteRender.sprite = objectToRender;
            }
        }
        else
        {
            if (mySpriteRender != null)
            {
                mySpriteRender.sprite = null;
            }
        }
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
        if (letterManager && letterManager.gameObject)
        {
            letterManager.gameObject.SetActive(false);
        }
    }
}
