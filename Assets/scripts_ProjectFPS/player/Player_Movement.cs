using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    // Variáveis públicas para ajustar a velocidade do movimento e a sensibilidade da câmera
    public float moveSpeed = 5f;
    public float mouseSensitivity = 100f;

    // Variáveis privadas para armazenar a rotação acumulada da câmera no eixo X
    private float xRotation = 0f;
    // Referência à câmera principal
    public Transform playerCamera;

    void Start()
    {
        // Obter e armazenar referência à câmera do jogador
        playerCamera = Camera.main.transform;

        // Trava o cursor no centro da tela e o torna invisível
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Captura a entrada do mouse para rotação da câmera
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Atualiza a rotação acumulada no eixo X (vertical) e limita para evitar a rotação completa
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Aplica a rotação no eixo X à câmera e a rotação no eixo Y (horizontal) ao corpo do jogador
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // Captura a entrada de teclas WASD para movimento
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Calcula a direção do movimento com base na entrada e na orientação do jogador
        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;

        // Move o jogador diretamente usando a transformação
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }
}
