using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _playerRigidbody2D;
    public float _playerSpeed;
    public float jumpForce = 5.0f; // For�a do pulo
    private Vector2 _playerDirection;
    private bool isGrounded; // Verifica��o se est� no ch�o

    // Start is called before the first frame update
    void Start()
    {
        _playerRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Verifica se o jogador est� no ch�o e se a tecla de pulo (espa�o) foi pressionada
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
        // Adiciona uma for�a para cima ao Rigidbody2D
        _playerRigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false; // Marca que o jogador n�o est� mais no ch�o
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se o jogador colidiu com o ch�o
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Est� no ch�o");
            isGrounded = true; // Marca que o jogador est� no ch�o
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Verifica se o jogador saiu do contato com o ch�o
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false; // Marca que o jogador n�o est� mais no ch�o
        }
    }
}
