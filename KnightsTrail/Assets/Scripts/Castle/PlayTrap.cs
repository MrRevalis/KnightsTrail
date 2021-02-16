using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTrap : MonoBehaviour
{
    public GameObject trapWall;
    public GameObject trapFloor;
    public CinemachineVirtualCamera camera;
    public NoiseSettings profile;
    private CinemachineBasicMultiChannelPerlin shake;

    private void Start()
    {
        camera.AddCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        shake = camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.GetComponent<PlayerMovement>().CanMove = false;
            collision.transform.GetComponent<Animator>().SetFloat("PlayerSpeed", 0);
            StartCoroutine(Play());
        }
    }

    IEnumerator Play()
    {
        float timer = 0f;
        ShakeCamera();
        while (timer <= 2)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        StopShakeCamera();
        trapWall.SetActive(true);
        trapFloor.SetActive(false);
    }

    void ShakeCamera()
    {
        shake.m_NoiseProfile = profile;
        shake.m_AmplitudeGain = 6f;
        shake.m_FrequencyGain = 1f;
    }

    void StopShakeCamera()
    {
        shake.m_NoiseProfile = null;
    }
}
