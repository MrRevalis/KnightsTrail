using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleBridgeLever : MonoBehaviour
{
    public GameObject bridge;
    public GameObject spikes;
    public Sprite offSprite;
    public Sprite onSprite;
    public bool leverEnebled = false;
    private bool playerInRange = false;
    private bool effectShown = false;
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
        Vector3 vector3 = new Vector3(-1, 0, 0);
        if (leverEnebled)
        {
            leverEnebled = false;
            transform.GetComponent<SpriteRenderer>().sprite = offSprite;
            spikes.SetActive(true);
            vector3 = new Vector3(1, 0, 0);
        }
        else
        {
            leverEnebled = true;
            spikes.SetActive(false);
            transform.GetComponent<SpriteRenderer>().sprite = onSprite;
            if (!effectShown)
            {
                transform.GetComponent<ShowEffect>().Show();
                effectShown = true;
            }
        }

        StartCoroutine(RotateObject(vector3));
    }

    IEnumerator RotateObject(Vector3 move)
    {
        float timer = 0f;
        while (timer <= 8)
        {
            bridge.transform.position += move * Time.deltaTime;
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
