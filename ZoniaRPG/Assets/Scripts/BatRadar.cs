using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatRadar : MonoBehaviour
{
    [SerializeField]
    private Enemy batEnemy;
    private void Start()
    {
        GetComponent<CircleCollider2D>().radius = batEnemy.rayVisionStatic;
    }
    private void Update()
    {
        transform.position = batEnemy.transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            batEnemy.player = collision.gameObject;
        }
    }

}
