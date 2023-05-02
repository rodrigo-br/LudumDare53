using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : Singleton<PlayerMovement>
{
    public delegate void    ReadLetter();
    public event ReadLetter OnReadLetter;
    [SerializeField] float  moveSpeed = 3f;
    [SerializeField] int    numberOfLettersinGame = 10;
    Animator                myAnimator;
    int[]                   lettersReadArray;
    int                     lettersRead;
    Rigidbody2D             myRigidBody;
    Vector2                 moveInput;
    float                   defaultMoveSpeed;
    float                   defaultAnimationSpeed;
    [SerializeField] Interactible interactible;
    bool                    isReadingLetter = false;
    bool                    isFlipped = false;
    bool                    isOnStair = false;
    AreaExit                stairExit;

    protected override void Awake()
    {
        base.Awake();
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    void Start()
    {
        lettersReadArray = new int[numberOfLettersinGame];
        lettersRead = 0;
        defaultMoveSpeed = moveSpeed;
        defaultAnimationSpeed = myAnimator.speed;
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        myRigidBody.velocity = moveInput * moveSpeed;
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        if (moveInput.x > 0 && isFlipped || moveInput.x < 0 && !isFlipped)
        {
            isFlipped = !isFlipped;
            transform.localScale = new Vector2(-transform.localScale.x, 1f);
        }
        myAnimator.SetBool("isWalking", Mathf.Abs(moveInput.x) > Mathf.Epsilon);
    }

    void OnRun(InputValue value)
    {
        if (value.isPressed)
        {
            moveSpeed *= 2;
            myAnimator.speed *= 1.5f;
        }
        else
        {
            moveSpeed = defaultMoveSpeed;
            myAnimator.speed = defaultAnimationSpeed;
        }
    }

    void OnInteract(InputValue value)
    {
        if (isReadingLetter)
        {
            interactible.StopReading();
            isReadingLetter = false;
        }
        else if (myRigidBody.IsTouchingLayers(LayerMask.GetMask("Interactible")) && !isReadingLetter)
        {
            if (interactible.OnInteract(lettersRead))
            {
                isReadingLetter = true;
                if (lettersReadArray[interactible.GetIndex()]++ == 0)
                {
                    lettersRead++;
                    OnReadLetter();
                    TestFinalScene();
                }
            }
        }
        if (isOnStair)
        {
            stairExit.Transition();
        }
    }

    public int GetLettersRead()
    {
        return lettersRead;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Interactible")
        {
            interactible = other.GetComponent<Interactible>();
        }
        else if (other.tag == "Stair")
        {
            isOnStair = true;
            stairExit = other.GetComponent<AreaExit>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Stair")
        {
            isOnStair = false;
        }
        else if (other.tag == "Interactible")
        {
            if (isReadingLetter)
            {
                interactible.StopReading();
                isReadingLetter = false;
            }
            interactible = null;
        }
    }

    void TestFinalScene()
    {
        if (lettersRead == 6)
        {
            FadeScreen.Instance.FadeToBlack();
            StartCoroutine(LoadSceneRoutine());
        }
    }

    IEnumerator LoadSceneRoutine()
    {
        yield return new WaitForSecondsRealtime(2f / FadeScreen.Instance.GetFadeSpeed());
        SceneManager.LoadScene("Scene 7");
    }

    public bool GetIsReadingLetter()
    {
        return isReadingLetter;
    }
}
