using UnityEngine;

public class PlayerIKControl : MonoBehaviour
{
    public Animator animator;

    public bool useIK = false;
    public Transform lookTarget;
    public Transform rightHandTarget;

    void OnAnimatorIK(int layerIndex)
    {
        Debug.Log("IK RUNNING");

        if (!useIK || animator == null)
            return;

        // Mirada
        if (lookTarget != null)
        {
            animator.SetLookAtWeight(1f, 1f, 1f, 1f, 1f);
            animator.SetLookAtPosition(lookTarget.position);
        }

        // Mano derecha
        if (rightHandTarget != null)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);

            animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandTarget.position);
            animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandTarget.rotation);
        }
    }
}
