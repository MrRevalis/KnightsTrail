using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallScript : MonoBehaviour
{
    public Transform SpawnPoint;
    public Transform Player;
    public AudioSource respawn;
    public GameObject gameOver;
    public AudioSource background;
    public AudioSource backgroundDead;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            respawn.Play();

            Player.transform.position = new Vector3(SpawnPoint.transform.position.x, SpawnPoint.transform.position.y, 0f);
        }
    }
    public void Respawn()
    {
        respawn.Play();
        //FindObjectOfType<HealthController>().currentHealth = FindObjectOfType<HealthController>().MaxHealth;
        Player.transform.position = new Vector3(SpawnPoint.transform.position.x, SpawnPoint.transform.position.y, 0f);
        /*gameOver.SetActive(false);
        FindObjectOfType<GameController>().StartGame();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().CanMove = true;*/
        FindObjectOfType<HealthController>().Resurrect();
        background.Play();
        backgroundDead.Stop();
    }
}
