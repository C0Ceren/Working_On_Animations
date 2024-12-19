using UnityEngine;

public class CatAnimationStateController : MonoBehaviour
{    Animator catAnimator;
  void Start()
  {
      catAnimator = GetComponent<Animator>();
  }


    void Update()
    {
       bool forwardPressed = Input.GetKey(KeyCode.UpArrow);

       if (forwardPressed)
       {
           catAnimator.SetBool("isCatWalking",true);
       }

       if (!forwardPressed)
       {
           catAnimator.SetBool("isCatWalking",false);
       }
    }
}
