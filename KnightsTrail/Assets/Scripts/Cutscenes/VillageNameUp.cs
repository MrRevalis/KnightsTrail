using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VillageNameUp : MonoBehaviour
{
    public GameObject VillageName;
    public GameObject playerObject;
    private TMP_Text text;
    private Rigidbody2D playerRB;

    public void Awake()
    {
        playerRB = playerObject.GetComponent<Rigidbody2D>();
        text = VillageName.GetComponent<TMP_Text>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("weszlo: " + collider.name);

        if (collider.CompareTag("Player"))
        {
            VillageName.SetActive(true);

            if (playerRB.velocity.x > 0)
            {
                text.SetText("...The Village");
            }
            else
            {
                text.SetText("...Forest");
            }

            Invoke(nameof(SetNotActive), 1.5f);
        }
    }

    void SetNotActive()
    {
        VillageName.SetActive(false);
    }
}
