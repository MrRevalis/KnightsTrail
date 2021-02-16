using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealtBarControllerBandit : MonoBehaviour
{
    Vector3 localScale;
    float max;
    private BanditLightController parent;

    void Start()
    {
        //Debug.Log(FrogMovement.lives);
        parent = gameObject.GetComponentInParent<BanditLightController>();
        max = parent.lives;
        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        localScale.x = parent.lives / max;
        transform.localScale = localScale;
    }
}
