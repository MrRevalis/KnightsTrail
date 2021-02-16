using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCatch : MonoBehaviour
{
    private Transform player;
    public float originalSpeed = 40f;
    private bool inRange = false;
    private bool canMove = true;
    private bool isCatched = false;
    private float xDiff = 0f;

    private PlayerMovement pmove;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (inRange && Input.GetKeyUp(KeyCode.E))
        {        
            if (isCatched)
            {
                player.GetComponent<PlayerMovement>().carryingStuff = false;
                pmove.movementSpeed = originalSpeed;
                pmove.canDash = true;
                isCatched = false;
            }
            else
            {
                player.GetComponent<PlayerMovement>().carryingStuff = true;
                pmove.movementSpeed = 0.5f * originalSpeed;
                pmove.canDash = false;
                if (!isCatched) xDiff = Mathf.Abs(player.position.x) - Mathf.Abs(transform.position.x);
                isCatched = true;
                HideText();
            }
        }
        if (isCatched && !inRange)
        {
            pmove.movementSpeed = originalSpeed;
            pmove.canDash = true;
            isCatched = false;
        }
    }

    private void FixedUpdate()
    {
        if (isCatched && canMove && inRange)
        {
            float mp = 1.1f;
            transform.position = new Vector3(player.position.x - (xDiff*mp), transform.position.y, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.transform;
            pmove = player.GetComponent<PlayerMovement>();
            inRange = true;
            ShowText();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.GetComponent<PlayerMovement>().carryingStuff = false;
            inRange = false;
            HideText();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Ground"))
        {
            canMove = false;
            float mp = 0.9f;
            if(player != null) transform.position = new Vector3(player.position.x - (xDiff * mp), transform.position.y, 0f);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) canMove = true;
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
