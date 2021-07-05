using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Slider LifeSlider;
    [SerializeField]
    private Player player;
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }
    public void Update()
    {
        LifeBarClock();
    }
    public void LifeBarClock()
    {
        LifeSlider.value = (player.Life);
    }
}
