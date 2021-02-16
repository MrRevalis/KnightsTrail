using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianHealthBar : MonoBehaviour
{
    Vector3 localScale;
    float max;
    private Guardian parent;

    void Start()
    {
        parent = gameObject.GetComponentInParent<Guardian>();
        max = parent.lives;
        localScale = transform.localScale;
    }

    void Update()
    {
        localScale.x = parent.lives / max;
        transform.localScale = localScale;
    }
}
