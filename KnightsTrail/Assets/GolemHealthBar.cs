using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemHealthBar : MonoBehaviour
{
    Vector3 localScale;
    float max;
    private Golem parent;

    void Start()
    {
        parent = gameObject.GetComponentInParent<Golem>();
        max = parent.lives;
        localScale = transform.localScale;
    }

    void Update()
    {
        localScale.x = parent.lives / max;
        transform.localScale = localScale;
    }
}
