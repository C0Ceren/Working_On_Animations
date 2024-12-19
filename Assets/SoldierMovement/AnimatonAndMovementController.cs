using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimatonAndMovementController : MonoBehaviour
{
    private PlayerInput playerInput;
    private CharacterController characterController;
    private Animator animator;

 
     
    //Oyuncunun input değerlerini tutacak değişkenler
    private Vector2 currentMovementInput;
    private Vector3 currentMovement;
    private Vector3 currentRunMovement;
    private bool isMovementPressed;
    private bool isRunPressed;

    [SerializeField] float rotationFactorPerFrame = 1.0f;
    [SerializeField] float runSpeed = 3.0f;
    [SerializeField] float walkSpeed = 1.0f;

    private void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        
        //Yürüme callbackleri
        playerInput.CharacterControls.Move.started += context => onMovementInput(context);/*CharacterControls action mapimizdeki move actionu başlatıldığında ne yapılacağını söyler*/
        playerInput.CharacterControls.Move.canceled += context => onMovementInput(context);//Action  durdurulduğunda ne yapacağını söyler.
        playerInput.CharacterControls.Move.performed += context => onMovementInput(context);
        
        //Koşma callbackleri
        playerInput.CharacterControls.Run.started += context => onRun(context);
        playerInput.CharacterControls.Run.canceled += context => onRun(context);
        
    }


    // Update is called once per frame
    void Update()
    {
        HandleAnimation();
        HandleRotation();
        if (isRunPressed)
        {
            characterController.Move(currentRunMovement*Time.deltaTime); //Koşma tuşuna basıldıysa bu hareketi yap
        }
        else
        {
            characterController.Move(currentMovement*Time.deltaTime); //Koşma tuşuna basılmadıysa bu hareketi yap.
        }
        
    }

    private void onMovementInput(InputAction.CallbackContext context)
    {   
        //Input sisteminden vector 2'nin değerlerini al, bu değerler doğrultusunda vector3'ün x ve z eksenlerini değiştir.
        currentMovementInput = context.ReadValue<Vector2>();
        //Debug.Log(context.ReadValue<Vector2>());
        currentMovement.x = currentMovementInput.x * walkSpeed;
        currentMovement.z = currentMovementInput.y * walkSpeed;
        currentRunMovement.x = currentMovementInput.x * runSpeed;
        currentRunMovement.z = currentMovementInput.y * runSpeed;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
        
    }

    private void HandleAnimation()
    {   
        //Animatörden bool değerleri al
        bool isWalking = animator.GetBool("isWalking");
        bool isRunning = animator.GetBool("isRunning");

        if (isMovementPressed && !isWalking)
        {
            animator.SetBool("isWalking", true);
        }
        else if (!isMovementPressed && isWalking)
        {
            animator.SetBool("isWalking", false);
        }

        if ((isMovementPressed && isRunPressed) && !isRunning)
        {
            animator.SetBool("isRunning", true);
        }
        else if ((!isMovementPressed || !isRunPressed) && isRunning)
        {
            animator.SetBool("isRunning",false);
        }
    }

    private void HandleRotation()
    {
        Vector3 positionToLookAt;
        //karakterimizin yönelmesi geregen pozisyon değişimi
        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;
        //karakterin o anki rotasyonu
        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed)
        {
            //karakterimizin bastığı tuşa göre yeni rotasyonu oluştur.
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }

    }

    private void onRun(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }
     
    private void OnEnable() //Her action mapin çalışması enabled ve disabled fonksiyonlarının olması gerek.
    {
        playerInput.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        playerInput.CharacterControls.Disable();
    }
    
    
}
