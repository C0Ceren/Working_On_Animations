using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class characterMovemeny : MonoBehaviour
{
    private Animator animator;
    private PlayerInput input;

    private void Awake()
    {
        input = new PlayerInput();

       // input.CharacterControls.Movement.performed += ctx => Debug.Log(ctx.ReadValueAsObject());
        
        
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void handleMovement()
    {
        bool isRunning = animator.GetBool("isRunning");
        bool isWalking = animator.GetBool("isWalking");
    }

    void OnEnable()
    {
       // input.CharacterControls.Enable();
    }
    
    void OnDisable()
    {
       // input.CharacterControls.Disable();
    }
}
