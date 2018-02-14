using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeditationManager : MonoBehaviour {

    public Rigidbody2D playerBody;

    public CursorBehavior cursorBehavior;

    public Camera cam;

    public Transform startingTransform;
    public Transform stoppingTransform;

    public Animator playerAnimator;
    public Animator lanternAnim;

    public float playerSpeed;
    public float gameStartingDelay;

    public RiddleManager riddleManager;

    [Header("Dialogue Settings")]
    public DialogueTrigger lanternDialogue;
    public DialogueManager dialogueManager;
    public GameObject dialogueBox;
    private bool sentenceFullyDisplayed;

    private bool isWalking;
    public bool isLit = false;

	void Start () {

        //check amount of souls possessed
        Debug.Log("Souls Possessed : " + GameManager.soulsPossessed);
        //disable dialogue box
        dialogueBox.SetActive(false);

        playerBody.transform.position = startingTransform.position;
        StartWalking();
        playerAnimator.SetBool("sat", false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //detect if sentence has finished writing
            if(dialogueManager.dialogueText.text == dialogueManager.sentence)
            {
                sentenceFullyDisplayed = true;
            }
            else
            {
                sentenceFullyDisplayed = false;
            }

            //finish sentence, or go to next sentence
            if(sentenceFullyDisplayed)
            {
                dialogueManager.DisplayNextSentence();
            }
            else
            {
                dialogueManager.dialogueText.text = dialogueManager.sentence;
                dialogueManager.StopAllCoroutines();
            }
        }
    }

    private void FixedUpdate()
    {
        //exit
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }

        //stop if at stop position
        if (playerBody.transform.position.y >= stoppingTransform.position.y && isWalking)
        {
            StopWalking();

            //light lantern after lanternLightDelay
            StartCoroutine("StartGame");

            //sit down
            playerAnimator.SetTrigger("sitting");
            playerAnimator.SetBool("sat", true);
        }

    }


    void StartWalking()
    {
        isWalking = true;
        playerBody.velocity = new Vector3(0, playerSpeed, 0);
    }


    void StopWalking()
    {
        isWalking = false;
        playerBody.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }


    private IEnumerator StartGame()
    {
<<<<<<< HEAD
        yield return new WaitForSeconds(gameStartingDelay);
=======
        yield return new WaitForSeconds(lanternLightDelay);
>>>>>>> Martin
        riddleManager.state = RiddleState.active;
        dialogueBox.SetActive(true);
        lanternDialogue.TriggerDialogue();
        cursorBehavior.Appear();
    }
}