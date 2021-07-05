using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float rayVisionStatic, rayVisionMoving, rayAttack, rayLongRangeAttack, velocity;
    private float rayVision;
    [SerializeField]
    private LayerMask playerMask;
    [SerializeField]
    private Rigidbody2D skillBat;
    private Animator animo;
    private GameObject[] player;
    private Rigidbody2D rigidBodyEnemy;
    public static Enemy Instance { get; private set; }
    private Vector3 initialPosition;
    public Vector3 Target { get; private set; }
    private Vector3 direction;
    private Vector3 temp;
    private float clock;
    private float timeToInstantiate = 5;
    public float life { get; set; } = 100;

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
        if(life <= 0)
        {
            Destroy(gameObject);
        }
        initialPosition = transform.position;
        clock = Time.time;
        
        PlayerRay();
        if (clock > timeToInstantiate)
        {
            GameObject.Instantiate(skillBat, transform.position, transform.rotation);
            timeToInstantiate += 5;
        }
        Debug.Log(clock);
        Debug.Log(timeToInstantiate);
        animo.SetFloat("X", 0);
        animo.SetFloat("Y", 0);
        if((GameObject.FindGameObjectWithTag("SkillBat").transform.position - transform.position).magnitude > rayLongRangeAttack)
        {
            Destroy(GameObject.FindGameObjectWithTag("SkillBat"));
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
    public void PlayerRay()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject play in player)
        {
            //PLAYER.Add(play);
            RaycastHit2D hit = Physics2D.Raycast(transform.position,
                                                 play.transform.position - transform.position,
                                                 rayVision,
                                                 playerMask);
            temp = transform.TransformDirection(play.transform.position - 
                                                        transform.position);
            Debug.DrawRay(transform.position, temp, Color.cyan);

            if(/*!hit.collider.CompareTag(null) &&*/ hit.collider.CompareTag("Player"))
            {
                rayVision = rayVisionMoving;
                Target = play.transform.position;
            }
            if(temp.magnitude > rayVision)
            {
                Destroy(GameObject.FindGameObjectWithTag("SkillBat"));
                rayVision = rayVisionStatic;
                Target = initialPosition;
            }
            float distTemp = Vector3.Distance(Target, transform.position);
            direction = (Target - transform.position).normalized;

            if(Target!= initialPosition && distTemp < rayAttack)
            {

            }
            else
            {
                rigidBodyEnemy.MovePosition(transform.position + direction.normalized * velocity * Time.deltaTime);
            }
            if(Target== initialPosition && distTemp <= 0.02f)
            {
                transform.position = initialPosition;
            }
            Debug.DrawRay(transform.position, play.transform.position - transform.position, Color.blue);
            Debug.DrawRay(transform.position, Target - transform.position, Color.green);
            Debug.DrawRay(transform.position, transform.position + direction.normalized * velocity * Time.deltaTime, Color.gray);
            Debug.DrawRay(transform.position, initialPosition, Color.green);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //collision = GameObject.FindGameObjectWithTag("SkillBat")
        if (collision.tag == "PlayerSkills")
        {
            life = 0;
        }
    }
}
