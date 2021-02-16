using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPController : MonoBehaviour
{
    public int leverId = 0;
    public Sprite offSprite;
    public Sprite onSprite;
    public bool leverEnebled = false;
    public bool playerInRange = false;

    private LeverPuzzleManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = transform.parent.GetComponent<LeverPuzzleManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                ChangeLeverPos();
                manager.ChangeState(leverId);
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
        } else
        {
            leverEnebled = true;
            transform.GetComponent<SpriteRenderer>().sprite = onSprite;
        }
    }
}
