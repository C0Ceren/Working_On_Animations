using Unity.VisualScripting;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    private Animator animator;
    private float velocityX = 0.0f;
    private float velocityZ = 0.0f;
    [SerializeField] float acceleration = 0.01f;
    [SerializeField] private float deceleration = 0.5f;
    void Start()
    {
        animator = GetComponent<Animator>(); 
     
    }

  
    void Update()
    {
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);

        if (forwardPressed && velocityZ < 5.0f) //W tuşuna bastığımızda yürümeye ve hızlanmaya başlar. Ancak sürat en fazla 2 olabilir.
        {
            velocityZ += Time.deltaTime * acceleration;
        }
        
        else if (!forwardPressed && velocityZ > 0.0f) //W tuşuna basılmadığında sürat 0'dan büyük ise(bunu velocity -'lere düşmesin diye yapıyoruz) yavaşlamaya başlar ve durur.
         {
             velocityZ -= Time.deltaTime * deceleration;
         }

         else if (!forwardPressed && velocityZ < 0.0f)//Yavaşlama sırasında sürat hasbel kader 0'ın altına düştüyse onu geri 0'a yükseltsin.
         {
             velocityZ = 0.0f;
         }

        if (rightPressed && forwardPressed && velocityX < 5.0f) //D tuşuna bastığımızda VelocityX yavaş artmaya başlayacak ve oyuncu sağa hareket edecek.
        {
            velocityX += Time.deltaTime * acceleration;
        }

        else if (leftPressed && forwardPressed && velocityX > -5.0f )//A tuşuna bastığımızda VelocityX azaltmaya başlayacak ve oyuncu sola hareket edecek
        {
            velocityX -= Time.deltaTime * deceleration;
        }

        
        
        else if (((!leftPressed || !rightPressed) && (velocityZ == 0)) && (velocityX != 0.0f)) //karakter durduğunda düz dursun, saga veya sola dönük olmasın diye bu şartı yazıyoruz.
        {
            velocityX = 0.0f;
        }
        
         
         animator.SetFloat("VelocityX", velocityX);
         animator.SetFloat("VelocityZ", velocityZ);
    }
}
