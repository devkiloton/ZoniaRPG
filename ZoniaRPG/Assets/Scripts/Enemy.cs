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
    [SerializeField]
    private Vector3 target;
    [SerializeField]
    private float fov;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private Vector3 direction;

    private void Start()
    {
        RayCast = 1 << LayerMask.NameToLayer("Player");
        RigidBodyEnemy = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
        target = initialPosition;
        //PlayerRay();

    }
    private void Update()
    {
        if (direction.x != 0 || direction.y != 0)
        {
            anim.SetFloat("X", direction.x);
            anim.SetFloat("Y", direction.y);

        }
        PlayerRay();
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
            Vector3 temp = transform.TransformDirection(play.transform.position - 
                                                        transform.position);
            Debug.DrawRay(transform.position, temp, Color.cyan);

            if(!hit.collider.CompareTag(null) && hit.collider.CompareTag("Player"))
            {
                target = play.transform.position;
            }
            else
            {
                target = initialPosition;
            }
            float distTemp = Vector3.Distance(target, transform.position);
            direction = (target - transform.position).normalized;

            if(target!= initialPosition && distTemp < RayAttack)
            {

            }
            else
            {
                RigidBodyEnemy.MovePosition(transform.position + direction * Velocity * Time.deltaTime);
            }
            if(target== initialPosition && distTemp <= 0.02f)
            {
                transform.position = initialPosition;
            }
            //PLAYER.Remove(play);
        }
    }
}
