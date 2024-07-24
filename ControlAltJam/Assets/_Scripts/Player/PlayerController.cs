using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _playerRigidbody2D;
    public float _playerSpeed;
    public float jumpForce = 5.0f; // Força do pulo
    private Vector2 _playerDirection;
    private bool isGrounded; // Verificação se está no chão

    // Start is called before the first frame update
    void Start()
    {
        _playerRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Verifica se o jogador está no chão e se a tecla de pulo (espaço) foi pressionada
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Pulo!");
            Jump();
        }
    }

    private void FixedUpdate()
    {
        _playerRigidbody2D.MovePosition(_playerRigidbody2D.position + _playerDirection * _playerSpeed * Time.deltaTime);
    }

    void Jump()
    {
        // Adiciona uma força para cima ao Rigidbody2D
        _playerRigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false; // Marca que o jogador não está mais no chão
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se o jogador colidiu com o chão
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Está no chão");
            isGrounded = true; // Marca que o jogador está no chão
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Verifica se o jogador saiu do contato com o chão
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false; // Marca que o jogador não está mais no chão
        }
    }
}
