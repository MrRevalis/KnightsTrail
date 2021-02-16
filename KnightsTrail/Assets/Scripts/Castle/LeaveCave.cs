using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveCave : MonoBehaviour
{
    public GameObject savemanager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            savemanager.GetComponent<SaveManager>().Save();
            SceneManager.LoadScene("Swamp");
        }
    }
}
