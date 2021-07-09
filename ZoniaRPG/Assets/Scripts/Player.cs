using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using Mirror;

public class Player : NetworkBehaviour
{
    [SyncVar]
    public int Life;
    [SerializeField]
    private float speed;
    [SerializeField]
    private GameObject Skill;
    public Rigidbody2D RigidBodyPlayer { get; private set; }
    public Vector2 Direction { get; private set; }
    public static Player Instance;
    public AnimationsController MyMovements { get; private set; }
    private Canvas canvas;
    private GameObject skill;
    public Vector3 InitialDirection;
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
        InitialDirection = transform.position;
    }
    private void Update()
    {
        if (!isLocalPlayer) return;
        Input();
        if (UnityEngine.Input.GetKeyDown(Shot))
        {
            CmdFire();
        }
        DestroyPlayerNetwork();
    }
    private void FixedUpdate()
    {
        RigidBodyPlayer.MovePosition(RigidBodyPlayer.position +
                                     speed * Time.fixedDeltaTime * Direction);
    }
    [Command]
    private void CmdFire()
    {
        skill = Instantiate(Skill, transform.position, transform.rotation);
        Physics2D.IgnoreCollision(skill.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        skill.GetComponent<PurpleSkill>().DirectionPlayer = this;
        NetworkServer.Spawn(skill);
        RpOnFire();
    }
    [ClientRpc]
    private void RpOnFire()
    {
        skill.GetComponent<PurpleSkill>().idleRotationReference = this.MyMovements;
    }
    private void Input()
    {
        float x = UnityEngine.Input.GetAxis("Horizontal");
        float y = UnityEngine.Input.GetAxis("Vertical");
        Direction = new Vector2(x, y);
        if (x != 0 || y != 0)
        {
            Direction = new Vector2(x, y);
            MyMovements.MovementPlayer(Direction);
        }
    }
    [Command]
    private void DestroyPlayerNetwork()
    {
        if (Life <= 0)
        {
            NetworkServer.Destroy(gameObject);
            //SceneManager.LoadScene("GameOver");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerSkills"))
        {
            if (isServer)
            {
                Life -= 20;
            }
        }
    }
}
