using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenMirror : MonoBehaviour
{
    PlayerMovement player;
    SpriteRenderer mySpriteRenderer;

    void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        if (player.GetLettersRead() == 2)
        {
            mySpriteRenderer.sortingOrder = 1;
        }
    }
}
