using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using Mirror;

public class Player : NetworkBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private GameObject Skill;
    public Rigidbody2D RigidBodyPlayer { get; private set; }
    
    public Vector2 Direction { get; private set; }
    public static Player Instance;
    public int Life = 100;
    [Header("Components")]
    public NavMeshAgent Agent;
    public AnimationsController MyMovements { get; private set; }
    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        RigidBodyPlayer = GetComponent<Rigidbody2D>();
        MyMovements = GetComponent<AnimationsController>();
    }
    public void Update()
    {
        if (!isLocalPlayer) return;
        input();
        if (Input.GetKeyUp(KeyCode.Space))
        {
            GameObject skill = Instantiate(Skill, transform.position, transform.rotation);
        }
        if (Life <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("GameOver");
        }
    }
    public void FixedUpdate()
    {
        RigidBodyPlayer.MovePosition(RigidBodyPlayer.position +
                                     speed * Time.fixedDeltaTime * Direction);
    }

    public void input()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Direction = new Vector2(x,y);
        if (x != 0 || y != 0)
        {
            Direction = new Vector2(x, y);
            MyMovements.MovementPlayer(Direction);
        }
    }
}
