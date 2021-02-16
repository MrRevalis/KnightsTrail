using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Video;

public class LeverPlatPuz : MonoBehaviour
{
    public GameObject platform;
    public Sprite offSprite;
    public Sprite onSpriteLeft;
    public Sprite onSpriteRight;
    public int leverState = 0;
    public bool lastLeft = false;
    private bool playerInRange = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                if(leverState == 0)
                {
                    if (lastLeft)
                    {
                        leverState = 1;
                        lastLeft = false;
                    } else
                    {
                        leverState = -1;
                        lastLeft = true;
                    }
                } else if(leverState == -1)
                {
                    leverState++;
                    lastLeft = true;
                } else
                {
                    leverState--;
                    lastLeft = false;
                }
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
        Vector3 vector3 = new Vector3(0, leverState, 0);

        switch (leverState)
        {
            case -1:
                transform.GetComponent<SpriteRenderer>().sprite = onSpriteLeft;
                break;
            case 0:
                transform.GetComponent<SpriteRenderer>().sprite = offSprite;
                break;
            case 1:
                transform.GetComponent<SpriteRenderer>().sprite = onSpriteRight;
                break;
        }

        StartCoroutine(RotateObject(vector3));
    }

    IEnumerator RotateObject(Vector3 move)
    {
        while (leverState != 0)
        {
            platform.transform.position += move * Time.deltaTime;
            yield return null;
        }
    }

    public void TurnOff()
    {
        leverState = 0;
        ChangeLeverPos();
    }
}
