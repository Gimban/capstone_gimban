using UnityEngine;

public class LightSwitch : Interactable
{
    private Animator animator;
    public GameObject lights;
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
        animator.SetBool("light_toggle", !animator.GetBool("light_toggle"));

        if (lights != null)
        {
            lights.SetActive(animator.GetBool("light_toggle"));
        }
    }
}
