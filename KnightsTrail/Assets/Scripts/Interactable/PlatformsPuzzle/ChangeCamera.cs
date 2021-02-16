using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    public GameObject cameraCtrl;
    private CinemachineVirtualCamera manager;

    public float size = 3;
    public float height = 0.5f;

    private float defSize;
    private float defHeight;
    public bool resetToDefault = false;

    // Start is called before the first frame update
    void Start()
    {
        manager = cameraCtrl.GetComponent<CinemachineVirtualCamera>();
        defSize = manager.m_Lens.OrthographicSize;
        defHeight = manager.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            manager.m_Lens.OrthographicSize = resetToDefault ? defSize : size;
            manager.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY = resetToDefault ? defHeight : height;
        }
    }
}
