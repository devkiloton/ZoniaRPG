using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mirror;

public class PurpleSkill : NetworkBehaviour
{
    [SerializeField]
    private Player DirectionPlayer;
    [SerializeField]
    private AnimationsController idleRotationReference;
    [SerializeField]
    private float Velocity = 40;
    private Rigidbody2D RigidBody;
    [SerializeField]
    private Animator anim;
    private Vector3 directionPlayer;
    private Vector2 directionIdle;
    public float DestroyAfter = 5;
    //private GameObject BatCollider;

    void Start()
    {
        anim = FindObjectOfType<Animator>();
        DirectionPlayer = FindObjectOfType<Player>();
        idleRotationReference = FindObjectOfType<AnimationsController>();
        directionIdle = new Vector2(idleRotationReference.HorizontalIdle(), idleRotationReference.VerticalIdle());
        directionPlayer = DirectionPlayer.Direction;
        RigidBody = GetComponent<Rigidbody2D>();
        //BatCollider = GameObject.FindGameObjectWithTag("BatTag");
    }
    void Update()
    {
        if (anim.tag == "PlayerSkills") 
        {
            anim.SetFloat("X", DirectionPlayer.Direction.x);
            anim.SetFloat("Y", DirectionPlayer.Direction.y);
            RigidBody.MovePosition(transform.position +
            Time.deltaTime * Velocity * directionPlayer.normalized);
            if (directionPlayer.magnitude < 0.1)
            {

                anim.SetFloat("XIdle", idleRotationReference.HorizontalIdle());
                anim.SetFloat("YIdle", idleRotationReference.VerticalIdle());
                anim.SetFloat("Velocity", idleRotationReference.Velocity());
                RigidBody.MovePosition((Vector2)(transform.position) +
                                       Time.deltaTime * Velocity * directionIdle.normalized);

            }
        }
    }
    /*private void OnTriggerStay2D(Collider2D collision)
    {
        collision = BatCollider.GetComponent<BoxCollider2D>();
        if(collision.CompareTag("BatTag"))
        {
            Enemy.Instance.life -= 100;
        }
    }*/
    [Server]
    void DestroySelf()
    {
        NetworkServer.Destroy(gameObject);
    }
    public override void OnStartServer()
    {
        Invoke(nameof(DestroySelf), DestroyAfter);
    }
}