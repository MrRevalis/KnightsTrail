using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowEffect : MonoBehaviour
{
    public CinemachineVirtualCamera camera;
    public GameObject canvas;
    public Transform player;
    public Transform lookAt;

    private CinemachineVirtualCamera manager;

    private float size = 3;
    private float height = 0.5f;

    private float defSize;
    private float defHeight;

    public void Show()
    {
        manager = camera.GetComponent<CinemachineVirtualCamera>();
        defSize = manager.m_Lens.OrthographicSize;
        defHeight = manager.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY;

        manager.m_Lens.OrthographicSize = size;
        manager.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY = height;

        player.GetComponent<PlayerMovement>().CanMove = false;
        canvas.SetActive(false);
        camera.Follow = lookAt;
        StartCoroutine(Coroutine());
    }

    private void StopShow()
    {
        manager.m_Lens.OrthographicSize = defSize;
        manager.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY = defHeight;

        player.GetComponent<PlayerMovement>().CanMove = true;
        canvas.SetActive(true);
        camera.Follow = player;
    }

    IEnumerator Coroutine()
    {
        yield return new WaitForSeconds(2);
        StopShow();
    }
}
