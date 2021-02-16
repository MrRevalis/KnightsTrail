using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingHealthBar : MonoBehaviour
{
    Vector3 localScale;
    float max;
    private EyeFollowPlayer parent;

    void Start()
    {
        parent = gameObject.GetComponentInParent<EyeFollowPlayer>();
        max = parent.lives;
        localScale = transform.localScale;
    }

    void Update()
    {
        localScale.x = parent.lives / max;
        transform.localScale = localScale;
    }
}
