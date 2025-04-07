using UnityEngine;

public class fuse_handle : Interactable
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void OnLookAt()
    {
        
    }

    public override void OnLookAway()
    {
        
    }

    public override void OnInteract()
    {
        animator.SetBool("shutdown", !animator.GetBool("shutdown"));
    }
}
