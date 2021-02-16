using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignScript : MonoBehaviour
{
    public DialogueTrigger dialogueTrigger;
    public DialogueMenager dialogueMenager;

    private bool playerInRange = false;
    private bool dialogueStarted = false;

    void Update()
    {
        if(playerInRange == true)
        {
            GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
            float distanceBetween = Vector3.Distance(player.transform.position, this.transform.position);

            if (distanceBetween < 1.1)
            {
                //Debug.Log("GRACZ W ZASIEGU");
                if (Input.GetKeyDown("e") && dialogueStarted != true)
                {
                    dialogueStarted = true;
                    dialogueTrigger.TriggerDialogue();
                }

                if(dialogueStarted == true && Input.GetKeyDown("r"))
                {
                    dialogueMenager.DisplayNextSentence();
                }
            }
            else
            {
                //Debug.Log("GRACZ POZA ZASIEGIEM");
                dialogueMenager.EndDialog();
                playerInRange = false;
                dialogueStarted = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            //Debug.Log("Kolidacja z graczem");
            playerInRange = true;
            ShowText();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            dialogueMenager.EndDialog();
            HideText();
        }
    }

    private void ShowText()
    {
        Transform text = transform.Find("TextContainer");
        text.gameObject.SetActive(true);
    }

    private void HideText()
    {
        Transform text = transform.Find("TextContainer");
        text.GetComponent<Animator>().SetBool("Hide", true);
    }
}
