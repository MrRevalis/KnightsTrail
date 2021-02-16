using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBucket : MonoBehaviour
{
    public AudioSource fillBucket;
    public AudioSource pourBucket;
    private GameObject jar;
    private bool playerWithJar = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerWithJar)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                jar.GetComponent<WaterJar>().AddWater();
                fillBucket.Play();
            } else if (Input.GetKeyUp(KeyCode.R))
            {
                jar.GetComponent<WaterJar>().DeleteWater();
                pourBucket.Play();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            for(int i = 0; i<collision.transform.childCount; i++)
            {
                if (collision.transform.GetChild(i).CompareTag("WaterJar"))
                {
                    jar = collision.transform.GetChild(i).gameObject;
                    jar.GetComponent<WaterJar>().blockDropping = true;
                    playerWithJar = true;
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
            if(jar != null) jar.GetComponent<WaterJar>().blockDropping = false;
            playerWithJar = false;
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
