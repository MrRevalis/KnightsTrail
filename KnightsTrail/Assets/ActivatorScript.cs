using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorScript : MonoBehaviour
{
    public GameObject gate;
    public AudioSource open;
    private Vector3 movingDirection;
    private bool isTriggered;
    private bool isDown = false;
    private bool isUp = false;
    private bool boxCollide = false;
    void Start()
    {
        movingDirection = new Vector3(0, 20, 0);
        isTriggered = false;
    }

    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        if (!isTriggered && !isDown && collision.name != "Guardian")
        {
            StartCoroutine(RotateObjectDown());
            if (collision.tag.Equals("Box"))
            {
                boxCollide = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Box"))
        {
            boxCollide = false;
        }

        if (!isTriggered && !isUp && !boxCollide)
        {
            StartCoroutine(RotateObjectUp());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isTriggered && !isDown && collision.name != "Guardian")
        {
            //Debug.Log("JEST DALEJ TRIGGER:");
            StartCoroutine(RotateObjectDown());
        }
    }

    IEnumerator RotateObjectDown()
    {
        open.Play();
        //Debug.Log("ELOSZKA PRZESUWNAIE w DOL");
        isTriggered = true;
        float timer = 0f;
        while (timer <= 0.2)
        {
            gate.transform.position += movingDirection * Time.deltaTime;
            timer += Time.deltaTime;
            yield return null;
        }
        isTriggered = false;
        isDown = true;
        isUp = false;
    }

    IEnumerator RotateObjectUp()
    {
        open.Play();
        isTriggered = true;
        //Debug.Log("ELOSZKA PRZESUWNAIE w GORE");
        float timer = 0f;
        while (timer <= 0.2)
        {
            gate.transform.position += -movingDirection * Time.deltaTime;
            timer += Time.deltaTime;
            yield return null;
        }
        isTriggered = false;
        isDown = false;
        isUp = true;
    }
}
