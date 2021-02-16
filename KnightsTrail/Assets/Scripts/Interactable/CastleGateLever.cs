using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleGateLever : MonoBehaviour
{
    public Transform gate;
    public Sprite offSprite;
    public Sprite onSprite;
    private bool playerInRange = false;
    private bool leverEnabled = false;
    public bool fliped = false;

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

    public void OpenTheGate()
    {
        if (!leverEnabled)
        {
            ChangeLeverPos();
        }
    }

    private void ChangeLeverPos()
    {
        int multiplayer = fliped ? -1 : 1;
        Vector3 vector3 = new Vector3(0, multiplayer*1, 0);
        if (leverEnabled)
        {
            leverEnabled = false;
            transform.GetComponent<SpriteRenderer>().sprite = offSprite;
            vector3 = new Vector3(0, multiplayer*(-1), 0);
        }
        else
        {
            leverEnabled = true;
            transform.GetComponent<SpriteRenderer>().sprite = onSprite;
        }

        StartCoroutine(RotateObject(vector3));
    }

    IEnumerator RotateObject(Vector3 move)
    {
        float timer = 0f;
        float until = fliped ? 4f : 3.5f;
        while (timer <= until)
        {
            gate.position += move * Time.deltaTime;
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
