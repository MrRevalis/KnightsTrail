using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingController : MonoBehaviour
{

    public CinemachineVirtualCamera vcam;
    public DialogueTrigger dialogueTrigger;
    public DialogueMenager dialogueMenager;

    private float cameraSize = 0;
    private bool canInteract;
    private Vector3 kingPosition;

    void Start()
    {
        canInteract = false;
        kingPosition = GameObject.FindGameObjectWithTag("King").transform.position;
        cameraSize = vcam.m_Lens.OrthographicSize;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        canInteract = true;
        Debug.Log("Rozpoczecie triggera");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("Jest dalej trigger");
        if(canInteract && collision.gameObject.tag.Equals("Player"))
        {
            /*var playerPosition = collision.gameObject.transform.position;
            float distance = Vector3.Distance(playerPosition, kingPosition);*/
            if (Input.GetKeyDown("e"))
            {
                Debug.Log("Konwersacja");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canInteract = false;
        Debug.Log("Koniec triggera");
    }

}
