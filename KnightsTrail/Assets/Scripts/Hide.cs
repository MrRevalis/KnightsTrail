using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    void HideElement()
    {
        transform.gameObject.SetActive(false);
    }
}
