using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D RigidBodyEnemy;
    public float RayVision, RayAttack, Velocity;
    public LayerMask PlayerMask;

    public GameObject[] Player;
    public List<GameObject> PLAYER;
    public LayerMask RayCast;

    [SerializeField]
    private Vector3 initialPosition;
    public Vector3 Target;
    [SerializeField]
    private float fov;
    [SerializeField]
    private Animator animo;
    public Vector3 direction;
    private Vector3 temp;
    [SerializeField]
    private Rigidbody2D SkillBat;
    private float clock;
    private float timeToInstantiate = 5;

    private void Start()
    {
        
        animo = GetComponent<Animator>();
        RayCast = 1 << LayerMask.NameToLayer("Player");
        RigidBodyEnemy = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
        Target = initialPosition;
        //PlayerRay();

    }
    private void Update()
    {
        clock = Time.timeSinceLevelLoad;
        
        PlayerRay();
        if ((temp.magnitude < 5))
        {
            animo.SetFloat("X", direction.x);
            animo.SetFloat("Y", direction.y);
            if (clock >= timeToInstantiate)
            {
                GameObject.Instantiate(SkillBat, transform.position, transform.rotation);
                timeToInstantiate += clock;
            }

        }
        else
        {
            animo.SetFloat("X", 0);
            animo.SetFloat("Y", 0);
        }
        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(initialPosition, RayVision);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(initialPosition, RayAttack);
    }
    public void PlayerRay()
    {
        Player = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject play in Player)
        {
            //PLAYER.Add(play);
            RaycastHit2D hit = Physics2D.Raycast(transform.position,
                                                 play.transform.position - transform.position,
                                                 RayVision,
                                                 PlayerMask);
            temp = transform.TransformDirection(play.transform.position - 
                                                        transform.position);
            Debug.DrawRay(transform.position, temp, Color.cyan);

            if(/*!hit.collider.CompareTag(null) &&*/ hit.collider.CompareTag("Player"))
            {
                Target = play.transform.position;
            }
            else
            {
                Target = initialPosition;
            }
            float distTemp = Vector3.Distance(Target, transform.position);
            direction = (Target - transform.position).normalized;

            if(Target!= initialPosition && distTemp < RayAttack)
            {

            }
            else
            {
                RigidBodyEnemy.MovePosition(transform.position + direction.normalized * Velocity * Time.deltaTime);
            }
            if(Target== initialPosition && distTemp <= 0.02f)
            {
                transform.position = initialPosition;
            }
            //PLAYER.Remove(play);
        }
    }
}
