using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    private bool playerInRange;
    private bool playerHolding;

    private Transform playerPosition;
    void Start()
    {
        playerInRange = false;
        playerHolding = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHolding)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.parent = null;
                Debug.Log("KONIEC TRZYMANA");
                playerHolding = false;
                playerInRange = false;
            }
        }
        if (playerInRange && !playerHolding)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                Debug.Log("TRZYMANA");
                transform.parent = playerPosition;
                playerHolding = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            ShowText();
            playerInRange = true;
            playerPosition = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            HideText();
            playerInRange = false;
        }
    }

    private void ShowText()
    {
        Debug.Log("pokazd testk");
        Transform text = transform.Find("TextContainer");
        text.gameObject.SetActive(true);
    }

    private void HideText()
    {
        Debug.Log("SUPER JEST");
        Transform text = transform.Find("TextContainer");
        text.GetComponent<Animator>().SetBool("Hide", true);
    }
}
