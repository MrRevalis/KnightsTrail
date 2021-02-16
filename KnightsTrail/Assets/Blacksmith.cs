using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blacksmith : MonoBehaviour
{
    private bool canInteract = false;
    public GameObject playerObject;
    private PlayerCombat playerCombat;
    private Bow playerBow;

    public GameObject shop;


    private void Awake()
    {
        playerCombat = playerObject.GetComponent<PlayerCombat>();
        playerBow = playerObject.GetComponent<Bow>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract)
            StartInteraction();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
            canInteract = true;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.gameObject.name.Equals("Player"))
        {
            StopInteracting();
            canInteract = false;
        }

    }

    private void StartInteraction()
    {
        shop.SetActive(true);
        playerBow.InputDisabled = true;
        playerCombat.InputDisabled = true;

    }

    public void StopInteracting()
    {
        shop.SetActive(false);
        playerBow.InputDisabled = false;
        playerCombat.InputDisabled = false;
    }
}
