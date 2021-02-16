using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarSlime2 : MonoBehaviour
{
    Vector3 localScale;
    float max;
    private SlimeMovement parent;

    void Start()
    {
        //Debug.Log(FrogMovement.lives);
        parent = gameObject.GetComponentInParent<SlimeMovement>();
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
