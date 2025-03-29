using UnityEngine;

public class Door : Interactable
{
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public override void OnLookAt()
    {
        // base.OnLookAt();
        // Debug.Log("Door Look");
    }

    public override void OnLookAway()
    {
        // base.OnLookAway();
        // Debug.Log("Door Lookaway");
    }

    public override void OnInteract()
    {
        // base.OnInteract();
        // Debug.Log("Door Interacted");
        animator.SetBool("open", !animator.GetBool("open"));
    }
}
