using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 moveInput;
    private bool isDead = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // En top-down la gravedad debe ser 0
        rb.gravityScale = 0f;
        rb.freezeRotation = true;
    }

    void Update()
    {
        if (isDead) return;

        if (Keyboard.current == null) return;

        // 1. Leer entradas
        float moveX = 0f;
        float moveY = 0f;

        if (Keyboard.current.aKey.isPressed) moveX -= 1f;
        if (Keyboard.current.dKey.isPressed) moveX += 1f;
        if (Keyboard.current.wKey.isPressed) moveY += 1f;
        if (Keyboard.current.sKey.isPressed) moveY -= 1f;

        moveInput = new Vector2(moveX, moveY).normalized;

        // 2. Lógica de Animación (8 Direcciones)
        if (moveInput.magnitude > 0.1f)
        {
            // Enviamos los valores al Blend Tree
            anim.SetFloat("moveX", moveInput.x);
            anim.SetFloat("moveY", moveInput.y);
            anim.SetBool("isWalking", true);
        }
        else
        {
            // Al dejar de movernos, isWalking es falso pero 
            // NO reseteamos moveX/moveY a cero para que el 
            // dinosaurio se quede mirando en la última dirección.
            anim.SetBool("isWalking", false);
        }
    }

    void FixedUpdate()
    {
        if (isDead)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        rb.linearVelocity = moveInput * moveSpeed;
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;
        moveInput = Vector2.zero;
        rb.linearVelocity = Vector2.zero;

        anim.SetTrigger("isDead");
        Debug.Log("Game Over: El dinosaurio ha caído.");
    }
    // --- Añade estas variables al principio de tu script ---
    private int score = 0;

    // --- Añade este método al final de tu script (antes de la última llave) ---
    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Puntaje actual: " + score);

        // Aquí podrías actualizar un texto de la interfaz (UI) más adelante
    }
}