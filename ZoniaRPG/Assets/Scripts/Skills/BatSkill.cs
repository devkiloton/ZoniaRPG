using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BatSkill : MonoBehaviour
{
    [SerializeField]
    private Enemy enemyScript;
    [SerializeField]
    private float velocity;
    private Rigidbody2D RigidBodySkillBat;
    private Vector3 direction;
    private GameObject[] targets;
    
    void Start()
    {
        enemyScript = FindObjectOfType<Enemy>();
        RigidBodySkillBat = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(enemyScript.CompareTag("BatTag"))
        {
            targets = GameObject.FindGameObjectsWithTag("Player");
            foreach (var target in from GameObject play in targets
                                   let target = Enemy.Instance.Target
                                   select target)
            {
                direction = (target - transform.position).normalized;
                RigidBodySkillBat.velocity = direction * velocity;
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
