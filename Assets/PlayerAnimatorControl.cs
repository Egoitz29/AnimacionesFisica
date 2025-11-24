using UnityEngine;

public class PlayerAnimatorControl : MonoBehaviour
{
    public Animator animator;
    public PlayerRagdoll ragdoll;
    public PlayerIKControl ikControl;

    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;

    private float smoothSpeed = 0f;

    void Update()
    {
        // Entrada del jugador
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(h, 0, v).normalized;

        // ROTACIÓN NORMAL
        if (dir.magnitude > 0f)
        {
            Quaternion targetRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
        }

        // VELOCIDAD SUAVIZADA
        float targetSpeed = dir.magnitude;
        smoothSpeed = Mathf.Lerp(smoothSpeed, targetSpeed, Time.deltaTime * 20f);

        // CORRER
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        // MODO SIGILO
        int mode = Input.GetKey(KeyCode.LeftControl) ? 1 : 0;

        // ---------- GIRO 90º (E) ----------
        bool turnPressed = Input.GetKeyDown(KeyCode.E);
        animator.SetBool("Turn90", turnPressed);

        // ---------- POINTING SOLO EN IDLE + R ----------
        bool isIdle = (smoothSpeed < 0.1f && mode == 0);

        if (isIdle && Input.GetKey(KeyCode.R))
        {
            animator.SetLayerWeight(1, 1);
        }
        else
        {
            animator.SetLayerWeight(1, 0);
        }

        // Enviar parámetros
        animator.SetFloat("Speed", smoothSpeed);
        animator.SetBool("IsRunning", isRunning);
        animator.SetInteger("Mode", mode);

        // MOVIMIENTO FÍSICO
        float realMove = (isRunning ? moveSpeed * 1.7f : moveSpeed);

        transform.Translate(Vector3.forward * smoothSpeed * realMove * Time.deltaTime);

        // Si la animación de giro ha empezado, forzamos rotación al final
        if (IsInTurnAnimation())
        {
            transform.Rotate(0, -90 * Time.deltaTime, 0);
        }

        // ---------- RAGDOLL (K/L) ----------
        if (Input.GetKeyDown(KeyCode.K))
            ragdoll.EnableRagdoll();

        if (Input.GetKeyDown(KeyCode.L))
            ragdoll.DisableRagdoll();

        // ---------- ACTIVAR/DESACTIVAR IK ----------
        if (Input.GetKeyDown(KeyCode.I))
        {
            ikControl.useIK = !ikControl.useIK;  // alternar IK
        }

    }

    private bool IsInTurnAnimation()
    {
        // OJO: Layer 0 = Base Layer
        // Layer 1 = UpperBody Layer
        // Crouch Turn está en Layer 0
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        return info.IsName("Crouch Turn Left 90");
    }
}
