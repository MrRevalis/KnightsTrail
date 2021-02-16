using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SproutHealthBar : MonoBehaviour
{
    Vector3 localScale;
    float max;
    private Sprout parent;

    void Start()
    {
        parent = gameObject.GetComponentInParent<Sprout>();
        max = parent.lives;
        localScale = transform.localScale;
    }

    void Update()
    {
        localScale.x = parent.lives / max;
        transform.localScale = localScale;
    }
}
