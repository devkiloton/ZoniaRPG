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
    public int Life { get; set; } = 100;
    [Header("Components")]
    public NavMeshAgent Agent;
    public AnimationsController MyMovements { get; private set; }
    private Canvas canvas;
    private Vector2 a;

    public KeyCode Shot = KeyCode.Space;
    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
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
        a = new Vector2(MyMovements.HorizontalIdle(), MyMovements.VerticalIdle());

        GameObject skill = Instantiate(Skill, transform.position + (Vector3)a.normalized * 2f, transform.rotation);
        skill.GetComponent<PurpleSkill>().DirectionPlayer = this;
        skill.GetComponent<PurpleSkill>().idleRotationReference = this.MyMovements;
        NetworkServer.Spawn(skill);
        //GameObject skill = Instantiate(Skill, transform.position + (Vector3)a.normalized * 1.3f, transform.rotation);
        //NetworkServer.Spawn(skill);
        //clientFire();
    }
    /*[ClientRpc]
    void clientFire()
    {
        GameObject skill = Instantiate(Skill, transform.position + (Vector3)a.normalized * 1.3f, transform.rotation);
        skill.GetComponent<PurpleSkill>().DirectionPlayer = this;
        skill.GetComponent<PurpleSkill>().idleRotationReference = this.MyMovements;
        NetworkServer.Spawn(skill);
    }*/
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
            Life -= 20;
        }
    }
}
