using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWarriorHealthBar : MonoBehaviour
{
    Vector3 localScale;
    float max;
    private GoblinController parent;

    void Start()
    {
        parent = gameObject.GetComponentInParent<GoblinController>();
        max = parent.lives;
        localScale = transform.localScale;
    }

    void Update()
    {
        localScale.x = parent.lives / max;
        transform.localScale = localScale;
    }
}
