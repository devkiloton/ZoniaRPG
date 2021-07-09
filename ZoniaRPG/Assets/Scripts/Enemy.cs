using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
//using System;

public class Enemy : NetworkBehaviour
{
    [SerializeField]
    private LayerMask playerMask;
    [SerializeField]
    private Rigidbody2D skillBat;
    [SerializeField]
        private float rayVisionMoving, rayAttack, rayLongRangeAttack, velocity;
        public float rayVisionStatic;
        private float rayVision;
    private Animator animo;
    public GameObject Player;
    private Rigidbody2D rigidBodyEnemy;
    public static Enemy Instance { get; private set; }
    private Vector3 initialPosition;
    public Vector3 Target { get; private set; }
    private Vector3 direction;
    private Vector3 temp;
    private float clock;
    private float timeToInstantiate = 5;
    public float Life { get; set; } = 100;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        animo = GetComponent<Animator>();
        rigidBodyEnemy = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
        rayVision = rayVisionStatic;
    }
    private void Update()
    {
        CheckDeath();
        AttackIfPlayerIsOnRadar();
        DestroyAttackIfSkillIsOutRadar();
        initialPosition = transform.position;
        clock = Time.time;
        animo.SetFloat("X", 0);
        animo.SetFloat("Y", 0);

    }
    private void AttackIfPlayerIsOnRadar()
    {
        CheckOnRadarToAttackPlayer();
        FollowingPlayerOnRadar();
        FinishAttackIfPlayerIsOutRadar();
    }
    
    private void CheckOnRadarToAttackPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position,
                                             Player.transform.position - transform.position,
                                             rayVision, playerMask);

        //Debug.DrawRay(transform.position, temp, Color.cyan);

        if (hit.collider.CompareTag("Player"))
        {
            if (clock > timeToInstantiate)
            {
                GameObject.Instantiate(skillBat, transform.position, transform.rotation);
                timeToInstantiate = clock + 5;
            }
            rayVision = rayVisionMoving;
            Target = Player.transform.position;
        }
    }
    private void FinishAttackIfPlayerIsOutRadar()
    {
        temp = transform.TransformDirection(Player.transform.position -
                                                    transform.position);

        if (temp.magnitude > rayVision)
        {
            Destroy(GameObject.FindGameObjectWithTag("SkillBat"));
            rayVision = rayVisionStatic;
            Target = initialPosition;
        }
    }
    private void FollowingPlayerOnRadar()
    {
        float distTemp = Vector3.Distance(Target, transform.position);
        direction = (Target - transform.position).normalized;

        if (!(Target != initialPosition && distTemp < rayAttack))
        {
            rigidBodyEnemy.MovePosition(transform.position +
                                        direction.normalized * velocity * Time.deltaTime);
        }

        if (Target == initialPosition && distTemp <= 0.02f)
        {
            transform.position = initialPosition;
        }
    }
    private void CheckDeath()
    {
        if (Life <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void DestroyAttackIfSkillIsOutRadar()
    {
        if ((GameObject.FindGameObjectWithTag("SkillBat").transform.position - transform.position).magnitude >
                                                          rayLongRangeAttack || (direction.magnitude < 0.1))
        {
            Destroy(GameObject.FindGameObjectWithTag("SkillBat"));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerSkills")
        {
            Life = 0;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(initialPosition, rayVision);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(initialPosition, rayAttack);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(initialPosition, rayLongRangeAttack);
    }
}
