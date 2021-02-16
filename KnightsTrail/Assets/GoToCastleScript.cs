using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToCastleScript : MonoBehaviour
{
    private bool playerInRange = false;
    public GameObject saveManager;

    private void Update()
    {
        if(playerInRange == true && Input.GetKeyDown(KeyCode.E))
        {
            saveManager.GetComponent<SaveManager>().Save();
            SceneManager.LoadScene("Castle");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ShowText();
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HideText();
            playerInRange = false;
        }
    }

    private void ShowText()
    {
        Transform text = transform.Find("TextContainer");
        text.GetComponent<TextMeshPro>().text = "E - enter";
        text.gameObject.SetActive(true);
    }

    private void HideText()
    {
        Transform text = transform.Find("TextContainer");
        text.GetComponent<Animator>().SetBool("Hide", true);
    }
}
