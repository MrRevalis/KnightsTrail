using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaterJar : MonoBehaviour
{
    public int water = 0;
    public bool blockDropping = false;

    private bool playerInRange = false;
    private bool playerIsHolding = false;
    private Transform player;
    private Transform originalParent;
    private Transform scale = null;

    private bool timer = false;
    private float timerStart;

    // Start is called before the first frame update
    void Start()
    {
        originalParent = transform.parent;
        water = Random.Range(0, 5);
    }

    public void DropJar()
    {
        //player.GetComponent<PlayerMovement>().carryingStuff = false;
        if (scale == null)
        {
            transform.parent = originalParent;
        }
        else
        {
            var jarHolder = scale.Find("JarHolder");
            transform.parent = jarHolder;
            transform.position = jarHolder.position;
            scale.GetComponent<WaterScale>().SetJar(transform);
        }
        playerIsHolding = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer)
        {
            if(Time.time - timerStart > 0.3)
            {
                timer = false;
                HideText();
            }
        }

        if (playerIsHolding)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            if (Input.GetKeyUp(KeyCode.E) && !blockDropping) //drop item
            {
                DropJar();
            }
        }
        if (playerInRange && !playerIsHolding)
        {
            if (Input.GetKeyUp(KeyCode.E)) //drag item
            {
                player.GetComponent<PlayerMovement>().carryingStuff = true;
                transform.parent = player;
                playerIsHolding = true;
                if(scale != null)
                {
                    transform.localPosition = new Vector3(transform.localRotation.x, 0.005154928f, transform.localPosition.z);
                    scale.GetComponent<WaterScale>().SetJar(null); 
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            player = collision.transform;
            ShowText();
        }
        if (collision.CompareTag("WaterScale"))
        {
            scale = collision.transform;
            if (scale.GetComponent<WaterScale>().jar != null) scale = null;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            HideText();
        }
        if (collision.CompareTag("WaterScale"))
        {
            scale = null;
        }
    }

    private void ShowText()
    {
        Transform text = transform.Find("TextContainer");
        text.GetComponent<TextMeshPro>().text = $"Water {water}l";
        text.gameObject.SetActive(true);
    }

    private void HideText()
    {
        Transform text = transform.Find("TextContainer");
        text.GetComponent<Animator>().SetBool("Hide", true);
    }

    public void AddWater()
    {
        if (water < 9) water++;
        timer = true;
        ShowText();
        timerStart = Time.time;
    }

    public void DeleteWater()
    {
        if (water > 0) water--;
        timer = true;
        ShowText();
        timerStart = Time.time;
    }
}
