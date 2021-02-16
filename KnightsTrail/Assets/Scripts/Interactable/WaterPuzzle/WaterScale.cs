using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaterScale : MonoBehaviour
{
    public int scaleID = 0;
    private WaterPuzzleController manager;
    private bool playerInRange = false;
    private bool playerHasJar = false;
    private Transform player;
    public Transform jar;
    private ParticleSystem lamp;

    private void Start()
    {
        manager = transform.parent.GetComponent<WaterPuzzleController>();
        lamp = transform.Find("Lamp").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            player = collision.transform;
            for (int i = 0; i < player.childCount; i++)
            {
                if (player.GetChild(i).CompareTag("WaterJar"))
                {
                    playerHasJar = true;
                    break;
                }
            }
            ShowText();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            playerHasJar = false;
            HideText();
        }
    }

    private void ShowText()
    {
        Transform text = transform.Find("TextContainer");
        string textContent = "";
        if (playerHasJar)
        {
            textContent = "put";
        } else if(jar != null)
        {
            textContent = "take";
        }
        text.GetComponent<TextMeshPro>().text = $"E - {textContent}";
        if (textContent != "")  text.gameObject.SetActive(true);
    }

    private void HideText()
    {
        Transform text = transform.Find("TextContainer");
        text.GetComponent<Animator>().SetBool("Hide", true);
    }

    public void SetJar(Transform obj)
    {
        jar = obj;
        if(jar == null)
        {
            manager.userCode[scaleID] = -1;
        } else
        {
            manager.userCode[scaleID] = jar.GetComponent<WaterJar>().water;
        }
        if(manager.userCode[scaleID] == manager.code[scaleID])
        {
            lamp.startColor = Color.green;
        }
        else
        {
            lamp.startColor = new Color(105, 80, 0, 255);
        }
    }
}
