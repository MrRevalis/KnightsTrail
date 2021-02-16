using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarControl : MonoBehaviour
{
    Vector3 localScale;
    public FrogMovement frog;
    float max;
    private FrogMovement parent;

    void Start()
    {
        //Debug.Log(FrogMovement.lives);
        parent = gameObject.GetComponentInParent<FrogMovement>();
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
