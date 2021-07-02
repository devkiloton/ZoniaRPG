using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D RigidBodyPlayer { get; set; }
    public Animator Anim { get; set; }
    public AnimationsController MyMovements { get; set; }
    [SerializeField]
    private float speed;
    public Vector2 Direction { get; private set; }

    private void Start()
    {
        RigidBodyPlayer = GetComponent<Rigidbody2D>();
        MyMovements = GetComponent<AnimationsController>();
    }
    public void Update()
    {
        input();
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
