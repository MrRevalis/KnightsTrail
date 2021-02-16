using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonHealthBar : MonoBehaviour
{
    Vector3 localScale;
    float max;
    private ArcherController parent;

    void Start()
    {
        parent = gameObject.GetComponentInParent<ArcherController>();
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