using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeverScript : MonoBehaviour
{
    public Animator animator;
    public GameObject gameObject;
    public AudioSource activated;

    bool canWork = false;
    bool isAnimationWorking = false;
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //bool isOpen = animator.GetBool("isOpen");
        if (canWork)
        {
            GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
            float distanceBetween = Vector3.Distance(player.transform.position, this.transform.position);
            if (distanceBetween < 1 && Input.GetKeyDown("e"))
            {
                if (isAnimationWorking == false)
                {
                    activated.Play();
                    bool leverStatus = animator.GetBool("isUp");
                    animator.SetBool("isUp", !leverStatus);
                    Vector3 vector3 = new Vector3(-1, 0, 0);

                    if (animator.GetBool("isUp") == false)
                    {
                        vector3 = new Vector3(1, 0, 0);
                    }

                    StartCoroutine(RotateObject(vector3));
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            ShowText();
            Debug.Log("Kolizja dzwignia");
            canWork = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
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

    IEnumerator RotateObject(Vector3 move)
    {
        float timer = 0f;
        isAnimationWorking = true;
        while (timer <= 4)
        {
            gameObject.transform.position += move * Time.deltaTime;
            timer += Time.deltaTime;
            yield return null;
        }
        isAnimationWorking = false;
    }
}
