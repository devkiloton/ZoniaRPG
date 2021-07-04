using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float /*RayVision,*/ rayVisionStatic, rayVisionMoving, RayAttack, rayLongRangeAttack, Velocity;//rayvision, rayVisionStatic, rayVisionMoving
    private float RayVision;
    [SerializeField]
    private LayerMask playerMask;
    [SerializeField]
    private Rigidbody2D SkillBat;
    private Animator animo;
    private GameObject[] player;
    private Rigidbody2D rigidBodyEnemy;
    public static Enemy Instance { get; private set; }
    //public List<GameObject> PLAYER;
    //public LayerMask RayCast;
    // TU PAROU AQUI!!!!!!!!!
    private Vector3 initialPosition;
    public Vector3 Target;
    //[SerializeField]
    //private float fov;
    public Vector3 direction;
    private Vector3 temp; //que porra é esse temp?
    private float clock;
    private float timeToInstantiate = 5;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {   
        animo = GetComponent<Animator>();
        //RayCast = 1 << LayerMask.NameToLayer("Player");
        rigidBodyEnemy = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
        //Target = initialPosition;
        //PlayerRay();
        RayVision = rayVisionStatic;

    }
    private void Update()
    {
        initialPosition = transform.position;
        clock = Time.time;
        
        PlayerRay();
        if (clock > timeToInstantiate)
        {
            GameObject.Instantiate(SkillBat, transform.position, transform.rotation);
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
        Gizmos.DrawWireSphere(initialPosition, RayVision);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(initialPosition, RayAttack);
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
                                                 RayVision,
                                                 playerMask);
            temp = transform.TransformDirection(play.transform.position - 
                                                        transform.position);
            Debug.DrawRay(transform.position, temp, Color.cyan);

            if(/*!hit.collider.CompareTag(null) &&*/ hit.collider.CompareTag("Player"))
            {
                RayVision = rayVisionMoving;
                Target = play.transform.position;
            }
            if(temp.magnitude > RayVision)
            {
                Destroy(GameObject.FindGameObjectWithTag("SkillBat"));
                RayVision = rayVisionStatic;
                Target = initialPosition;
            }
            float distTemp = Vector3.Distance(Target, transform.position);
            direction = (Target - transform.position).normalized;

            if(Target!= initialPosition && distTemp < RayAttack)
            {

            }
            else
            {
                rigidBodyEnemy.MovePosition(transform.position + direction.normalized * Velocity * Time.deltaTime);
            }
            if(Target== initialPosition && distTemp <= 0.02f)
            {
                transform.position = initialPosition;
            }
            //PLAYER.Remove(play);
        }
    }
}
