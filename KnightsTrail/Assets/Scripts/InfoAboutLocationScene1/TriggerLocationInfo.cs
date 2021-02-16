using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLocationInfo : MonoBehaviour
{
    public GameObject Info1;
    public GameObject Info2;

    public AudioSource BackgroundMusic;
    public AudioClip[] AudioClips;

    public Rigidbody2D playerRb;
    public Transform SpawnPoint;

    public GameObject player;
    private float time;

    private void Start()
    {
        time = Time.time;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpawnPoint.position = transform.position;

        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(HideText());
        }
        IEnumerator HideText()
        {
            Show();
            yield return new WaitForSeconds(1.5f);
            Info1.SetActive(false);
            Info2.SetActive(false);
        }
    }

    void Show()
    {
        if (playerRb.velocity.x > 0)
        {
            Info1.SetActive(true);
            Info2.SetActive(false);

            player.GetComponent<CharacterController2D>().ChangeMusicSource(Info1.name);

            if(Info1.name == "ForestInfo")
            {
                BackgroundMusic.clip = AudioClips[0];
                BackgroundMusic.Play();
            }
            else if(Info1.name == "CastleWayInfo")
            {
                BackgroundMusic.clip = AudioClips[2];
                BackgroundMusic.Play();
            }
        }
        else if (playerRb.velocity.x < 0)
        {
            Info1.SetActive(false);
            Info2.SetActive(true);
            player.GetComponent<CharacterController2D>().ChangeMusicSource(Info2.name);

            if (Info2.name == "MineInfo")
            {
                BackgroundMusic.clip = AudioClips[1];
                BackgroundMusic.Play();
            }
            else if(Info2.name == "VillageInfo")
            {
                BackgroundMusic.clip = AudioClips[0];
                BackgroundMusic.Play();
            }
        }
    }
}
