using UnityEngine;

public class animController : MonoBehaviour
{

    
    //this script is just to play the animation when you hit the melee key
    public Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.Play("punch");

        }
        
    }

   
    
}
