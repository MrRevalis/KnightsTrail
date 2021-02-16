using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpen : MonoBehaviour
{

    public Animator animator;
    public GameObject myPrefab;
    public AudioSource openChest;

    private bool canOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        Transform text = transform.Find("TextContainer");
        text.rotation = Quaternion.Euler(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        bool isOpen = animator.GetBool("isOpen");
        if (canOpen && isOpen == false)
        {
            GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
            float distanceBetween = Vector3.Distance(player.transform.position, this.transform.position);
            if (distanceBetween < 1 && Input.GetKeyDown("e"))
            {
                openChest.Play();
                HideText();
                animator.SetBool("isOpen", true);
                Instantiate(myPrefab, new Vector2(this.transform.position.x - 1, this.transform.position.y), Quaternion.identity);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            Debug.Log("Kolizja skrzynka");
            canOpen = true;
            if(!animator.GetBool("isOpen")) ShowText();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) HideText();
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
