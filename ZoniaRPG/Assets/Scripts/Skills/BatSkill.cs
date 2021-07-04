using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSkill : MonoBehaviour
{
    [SerializeField]
    private Enemy enemyScript;
    public Rigidbody2D RigidBodyBat;
    private Rigidbody2D RigidBodySkillBat;
    private Vector3 direction;
    public GameObject[] Target;
    public float Velocity;
    void Start()
    {
        enemyScript = FindObjectOfType<Enemy>();
        RigidBodyBat = enemyScript.RigidBodyEnemy;
        RigidBodySkillBat = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(enemyScript.tag == "BatTag")
        {
            Target = GameObject.FindGameObjectsWithTag("Player");
            Velocity = 5;
            foreach (GameObject play in Target)
            {
                Vector3 targetp = Enemy.Instance.Target;
                direction = targetp - transform.position;
                RigidBodySkillBat.velocity = direction.normalized * Velocity;
               // Vector3 destiny = play.transform.position;
               // RigidBodySkillBat.MovePosition((play.transform.position - this.transform.position).normalized * Velocity * Time.deltaTime);
            }
        }
        
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }   
    }
}
