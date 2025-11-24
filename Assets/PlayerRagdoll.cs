using UnityEngine;

public class PlayerRagdoll : MonoBehaviour
{
    public Animator animator;
    private Rigidbody[] rigidbodies;
    private Collider[] colliders;

    void Awake()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();

        DisableRagdoll();
    }

    public void EnableRagdoll()
    {
        animator.enabled = false;

        foreach (var rb in rigidbodies)
            rb.isKinematic = false;

        foreach (var col in colliders)
            col.enabled = true;
    }

    public void DisableRagdoll()
    {
        animator.enabled = true;

        foreach (var rb in rigidbodies)
            rb.isKinematic = true;

        foreach (var col in colliders)
        {
            if (col.gameObject != this.gameObject)
                col.enabled = false;
        }
    }
}
