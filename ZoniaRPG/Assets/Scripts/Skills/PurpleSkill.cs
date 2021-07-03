using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PurpleSkill : MonoBehaviour
{
    [SerializeField]
    private Player DirectionPlayer;
    [SerializeField]
    private AnimationsController idleRotationReference;
    [SerializeField]
    private float Velocity = 20;
    private Rigidbody2D RigidBody;
    [SerializeField]
    private Animator anim;
    private Vector3 directionPlayer;
    private Vector2 directionIdle;

    void Start()
    {
        DirectionPlayer = FindObjectOfType<Player>();
        idleRotationReference = FindObjectOfType<AnimationsController>();
        directionIdle = new Vector2(idleRotationReference.HorizontalIdle(), idleRotationReference.VerticalIdle());
        directionPlayer = DirectionPlayer.Direction;
        RigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        anim.SetFloat("X", DirectionPlayer.Direction.x);
        anim.SetFloat("Y", DirectionPlayer.Direction.y);
        RigidBody.MovePosition(transform.position +
        Time.deltaTime * Velocity * directionPlayer.normalized);
        if (directionPlayer.sqrMagnitude == 0)
        {
            
            anim.SetFloat("XIdle", idleRotationReference.HorizontalIdle());
            anim.SetFloat("YIdle", idleRotationReference.VerticalIdle());
            anim.SetFloat("Velocity", idleRotationReference.Velocity());
            RigidBody.MovePosition((Vector2)(transform.position) +
                                   Time.deltaTime * Velocity * directionIdle.normalized);
            
        }
        Debug.Log(directionPlayer.x);
    }
}