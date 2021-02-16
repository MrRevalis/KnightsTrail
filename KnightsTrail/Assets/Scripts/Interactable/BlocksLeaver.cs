using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksLeaver : MonoBehaviour
{
    public Sprite offSprite;
    public Sprite onSprite;
    public bool leverEnebled = false;
    private bool playerInRange = false;
    private bool effectShown = false;

    public DoorController[] doorsToOpen;


    // Update is called once per frame
    void Update()
    {
        if (playerInRange)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                ChangeLeverPos();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void ChangeLeverPos()
    {
        if (leverEnebled)
        {
            leverEnebled = false;
            transform.GetComponent<SpriteRenderer>().sprite = offSprite;
            foreach(var door in doorsToOpen)
            {
                door.locked = true;
            }
        }
        else
        {
            leverEnebled = true;
            transform.GetComponent<SpriteRenderer>().sprite = onSprite;
            foreach (var door in doorsToOpen)
            {
                door.locked = false;
            }
            if (!effectShown)
            {
                transform.GetComponent<ShowEffect>().Show();
                effectShown = true;
            }
        }
    }
}
