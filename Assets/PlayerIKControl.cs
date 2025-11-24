using UnityEngine;

public class PlayerIKControl : MonoBehaviour
{
    public Animator animator;

    public bool useIK = false;        // activar/desactivar IK
    public Transform lookTarget;      // punto de mirada
    public Transform rightHandTarget; // punto donde apunta la mano

    void OnAnimatorIK(int layerIndex)
    {
        if (!useIK || animator == null)
            return;

        // ----- IK de mirada -----
        if (lookTarget != null)
        {
            animator.SetLookAtWeight(1);
            animator.SetLookAtPosition(lookTarget.position);
        }

        // ----- IK de mano derecha -----
        if (rightHandTarget != null)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);

            animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandTarget.position);
            animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandTarget.rotation);
        }
    }
}
