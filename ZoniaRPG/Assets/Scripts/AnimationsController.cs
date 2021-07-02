using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsController : MonoBehaviour
{
    private Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void MovementPlayer(Vector2 direction)
    {
        anim.SetFloat("Horizontal", direction.x);
        anim.SetFloat("Vertical", direction.y);
        anim.SetFloat("Velocity", direction.sqrMagnitude);
        if (direction != Vector2.zero)
        {
            anim.SetFloat("HorizontalIdle", direction.x);
            anim.SetFloat("VerticalIdle", direction.y);
        }
    }
}
