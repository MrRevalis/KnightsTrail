using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowInfoText : MonoBehaviour
{
    public string textToShow;
    public bool customText = false;

    private void Update()
    {
        Transform text = transform.Find("TextContainer");
        text.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) ShowText();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) HideText();
    }

    private void ShowText()
    {
        Transform text = transform.Find("TextContainer");
        if(customText) text.GetComponent<TextMeshPro>().text = $"{textToShow}";
        text.gameObject.SetActive(true);
    }

    private void HideText()
    {
        Transform text = transform.Find("TextContainer");
        text.GetComponent<Animator>().SetBool("Hide", true);
    }
}
