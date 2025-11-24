using UnityEngine;

public class PlayerAnimatorControl : MonoBehaviour
{
    public Animator animator;

    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;

    private float smoothSpeed = 0f;

    void Update()
    {
        // Entrada del jugador
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(h, 0, v).normalized;

        // -------------------------------
        // ROTACIÓN
        // -------------------------------
        if (dir.magnitude > 0f)
        {
            Quaternion targetRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
        }

        // -------------------------------
        // VELOCIDAD DEL ANIMATOR (SUAVIZADA)
        // -------------------------------
        float targetSpeed = dir.magnitude;          // 0 o 1 inmediato
        smoothSpeed = Mathf.Lerp(smoothSpeed, targetSpeed, Time.deltaTime * 20f);

        // -------------------------------
        // CORRER
        // -------------------------------
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        // -------------------------------
        // MODO SIGILO
        // -------------------------------
        int mode = Input.GetKey(KeyCode.LeftControl) ? 1 : 0;

        // Enviar parámetros
        animator.SetFloat("Speed", smoothSpeed);   // ← suavizado, fluido, exacto
        animator.SetBool("IsRunning", isRunning);
        animator.SetInteger("Mode", mode);

        // -------------------------------
        // MOVIMIENTO FÍSICO
        // -------------------------------
        float realMove = (isRunning ? moveSpeed * 1.7f : moveSpeed);
        transform.Translate(Vector3.forward * smoothSpeed * realMove * Time.deltaTime);
    }
}
