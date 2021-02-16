using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainElevetor : MonoBehaviour
{
    private bool elevatorOn = false;
    private void Update()
    {
        if (GetComponent<PlatformMovementVertical>().enabled) elevatorOn = true;
        else elevatorOn = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if(!elevatorOn) ShowText();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
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
