 using System;
 using UnityEngine;

public class PosetliKadinAnimation : MonoBehaviour
{
    private Animator animator;
    [SerializeField]float acceleration = 1.0f;
    [SerializeField] float deceleration = 1.0f;
    private float Velocity = 0.0f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {
     bool forwardPressed = Input.GetKey(KeyCode.W);
     bool runPressed = Input.GetKey(KeyCode.LeftShift);

     if (forwardPressed && Velocity < 1.0f) ;
     {
         Velocity += acceleration * Time.deltaTime;
     }

     if (!forwardPressed && Velocity > 0.0f)
     {
         Velocity = 0.0f;
     }
     
     animator.SetFloat("Velocity", Velocity);
    }
}
