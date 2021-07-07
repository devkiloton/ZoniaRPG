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
    [SyncVar]
    public int Life; //{ get; set; } = 100;
    [Header("Components")]
    public NavMeshAgent Agent;
    public AnimationsController MyMovements { get; private set; }
    private Canvas canvas;
    public KeyCode Shot = KeyCode.Space;
    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        Life = 100;
        canvas = GetComponentInChildren<Canvas>();
        if (!isLocalPlayer)
        {
            canvas.gameObject.SetActive(false);
        }
        RigidBodyPlayer = GetComponent<Rigidbody2D>();
        MyMovements = GetComponent<AnimationsController>();
    }
    public void Update()
    {
        if (!isLocalPlayer) return;
        input();
        if (Input.GetKeyDown(Shot))
        {
            cmdFire();
        }
        DestroyPlayerNetwork();
    }
    public void FixedUpdate()
    {
        RigidBodyPlayer.MovePosition(RigidBodyPlayer.position +
                                     speed * Time.fixedDeltaTime * Direction);
    }
    [Command, ClientRpc]
    void cmdFire()
    {

        GameObject skill = Instantiate(Skill, transform.position , transform.rotation);
        Physics2D.IgnoreCollision(skill.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        skill.GetComponent<PurpleSkill>().DirectionPlayer = this;
        skill.GetComponent<PurpleSkill>().idleRotationReference = this.MyMovements;
        NetworkServer.Spawn(skill);
    }

    public void input()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Direction = new Vector2(x, y);
        if (x != 0 || y != 0)
        {
            Direction = new Vector2(x, y);
            MyMovements.MovementPlayer(Direction);
        }
    }
    [Command]
    void DestroyPlayerNetwork()
    {
        if (Life <= 0)
        {
            NetworkServer.Destroy(gameObject);
            //SceneManager.LoadScene("GameOver");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //collision = GameObject.FindGameObjectWithTag("SkillBat")
        if (collision.tag == "PlayerSkills")
        {
            if (isServer)
            {
                Life -= 20;
            }
            
        }
    }
}
