using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.ParticleSystemJobs;

public class Yuki : MonoBehaviour
{
    [Range(0.01f, 0.1f)][SerializeField] float moveSpeed = 0.05f;
    [SerializeField] bool isChasing = false;
    PlayerMovement      player;
    Animator            myAnimator;
    Rigidbody2D         myRigidBody;
    int                 numberOfLetters = 5;
    SpriteRenderer      mySpriteRenderer;
    ParticleSystem      myParticleSystem;

    void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
        myAnimator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myParticleSystem = GetComponentInChildren<ParticleSystem>();
    }

    void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Scene 4" || sceneName == "Scene 1")
        {
            myRigidBody.gravityScale = 0;
            if (player.GetLettersRead() >= numberOfLetters)
            {
                mySpriteRenderer.enabled = true;
                myAnimator.SetBool("isJumping", true);
                myAnimator.SetTrigger("ON");
                myParticleSystem.Play();
            }
        }
        else
        {
            CheckIndex();
        }
    }

    void FixedUpdate()
    {
        if (isChasing)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed);
            if (Vector2.Distance(transform.position, player.transform.position) < 2)
            {
                SceneManager.LoadScene("Scene 6");
            }
        }
    }

    public void SetChasing()
    {
        isChasing = !isChasing;
    }

    public void SetGravity()
    {
        myRigidBody.gravityScale = 1;
    }

    void OnEnable()
    {
        player.OnReadLetter += CheckIndex;
    }

    void OnDisable()
    {
        player.OnReadLetter -= CheckIndex;
    }

    void CheckIndex()
    {
        if (player.GetLettersRead() >= numberOfLetters)
        {
            TookTheLastLetter();
        }
    }

    void TookTheLastLetter()
    {
        myParticleSystem.Play();
        if (mySpriteRenderer && mySpriteRenderer.enabled == false)
        {
            mySpriteRenderer.enabled = true;
        }
        if (myAnimator)
        {
            myAnimator.SetTrigger("ON");
        }
        float delay = 1f;
        while (delay > 0)
        {
            delay -= Time.deltaTime;
        }
        isChasing = true;
    }
}
