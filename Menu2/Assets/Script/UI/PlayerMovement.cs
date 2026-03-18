using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float maxHealth = 10f;

    [Header("Referencias UI (Opcional)")]
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI scoreText;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private Vector2 moveInput;
    private float currentHealth;
    private int score = 0;
    private bool isDead = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        currentHealth = maxHealth;
        UpdateUI();

        rb.gravityScale = 0f;
        rb.freezeRotation = true;
    }

    void Update()
    {
        if (isDead) return;
        if (Keyboard.current == null) return;

        float moveX = 0f; float moveY = 0f;
        if (Keyboard.current.aKey.isPressed) moveX -= 1f;
        if (Keyboard.current.dKey.isPressed) moveX += 1f;
        if (Keyboard.current.wKey.isPressed) moveY += 1f;
        if (Keyboard.current.sKey.isPressed) moveY -= 1f;

        moveInput = new Vector2(moveX, moveY).normalized;

        if (moveInput.magnitude > 0.1f)
        {
            anim.SetFloat("moveX", moveInput.x);
            anim.SetFloat("moveY", moveInput.y);
            anim.SetBool("isWalking", true);
        }
        else { anim.SetBool("isWalking", false); }
    }

    void FixedUpdate()
    {
        if (isDead) { rb.linearVelocity = Vector2.zero; return; }
        rb.linearVelocity = moveInput * moveSpeed;
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;
        currentHealth -= damage;
        UpdateUI();
        StartCoroutine(FlashRed());
        if (currentHealth <= 0) Die();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (healthText != null) healthText.text = "Vida: " + currentHealth;
        if (scoreText != null) scoreText.text = "Puntos: " + score;
    }

    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        spriteRenderer.color = Color.white;
    }

    void Die()
    {
        isDead = true;
        anim.SetTrigger("isDead");
        FindObjectOfType<UIStatemanager>()?.ChangeState(UIStatemanager.UIState.Dead);
        FindObjectOfType<TimerManager>()?.StopTimer();
    }
}