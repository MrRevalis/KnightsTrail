using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchemistScript : MonoBehaviour
{
    public GameObject Shop;
    public GameObject playerObject;

    private PlayerCombat playerCombat;
    private Bow playerBow;
    private bool canInteract = false;
    private void Awake()
    {
        playerCombat = playerObject.GetComponent<PlayerCombat>();
        playerBow = playerObject.GetComponent<Bow>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            Debug.Log("Elosk");
            StartInteraction();
        }
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
        Shop.SetActive(true);
        playerBow.InputDisabled = true;
        playerCombat.InputDisabled = true;

    }

    public void StopInteracting()
    {
        Shop.SetActive(false);
        playerBow.InputDisabled = false;
        playerCombat.InputDisabled = false;
    }
}
