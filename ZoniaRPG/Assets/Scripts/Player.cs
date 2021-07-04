using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed;
    public Rigidbody2D RigidBodyPlayer { get; set; }
    public Animator Anim { get; set; }
    public AnimationsController MyMovements { get; set; }
    public Vector2 Direction { get; private set; }
    public GameObject Skill;

    private void Start()
    {
        RigidBodyPlayer = GetComponent<Rigidbody2D>();
        MyMovements = GetComponent<AnimationsController>();
    }
    public void Update()
    {
        input();
        if (Input.GetKeyUp(KeyCode.Space))
        {
            GameObject skill = Instantiate(Skill, transform.position, transform.rotation);
        }
    }
    public void FixedUpdate()
    {
        RigidBodyPlayer.MovePosition(RigidBodyPlayer.position +
                                     Direction * 
                                     speed * 
                                     Time.fixedDeltaTime);
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
