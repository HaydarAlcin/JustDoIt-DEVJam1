using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target, player; // Hedef karakterin Transform bile�eni
    public float distance; // Kamera ile hedef aras�ndaki mesafe
    public float height; // Kameran�n y�ksekli�i
    public float rotationSpeed = 5f; // Kamera ve karakter d�n�� h�z�
    public float cameraRotationSpeed = 2f; // Kamera d�n�� h�z�
    public float cameraDelay = 0.1f; // Kameran�n karaktere g�re gecikme s�resi

    private float currentRotation = 0f; // Mevcut kamera yatay d�n���
    private float currentVerticalRotation = 0f; // Mevcut kamera dikey d�n���

    private Vector3 previousTargetPosition; // �nceki hedef pozisyonu
    private Quaternion initialRotation; // �lk kamera d�n�� a��s�
    private Quaternion targetRotation; // Hedef kamera d�n�� a��s�

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Fareyi ortaya sabitle
        Cursor.visible = false; // Farenin g�r�n�rl���n� kapat
        previousTargetPosition = target.position; // �nceki hedef pozisyonunu ba�lang��ta g�ncelle
        initialRotation = transform.rotation; // �lk kamera d�n�� a��s�n� kaydet
    }

    private void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X"); // Fare hareketinin yatay eksendeki de�erini al
        float mouseY = Input.GetAxis("Mouse Y"); // Fare hareketinin dikey eksendeki de�erini al

        currentRotation += mouseX * rotationSpeed; // Mevcut yatay d�n�� a��s�n� fare hareketine g�re g�ncelle
        currentVerticalRotation -= mouseY * cameraRotationSpeed; // Mevcut dikey d�n�� a��s�n� fare hareketine g�re g�ncelle
        currentVerticalRotation = Mathf.Clamp(currentVerticalRotation, -60f, 60f); // Dikey d�n�� a��s�n� s�n�rla

        Quaternion rotation = Quaternion.Euler(currentVerticalRotation, currentRotation, 0f); // Yeni kamera d�n�� a��s�n� hesapla

        Vector3 targetVelocity = (target.position - previousTargetPosition) / Time.deltaTime; // Hedefin anl�k h�z�n� hesapla
        if (targetVelocity == Vector3.zero)
        {
            targetVelocity = target.forward; // E�er hedefin anl�k h�z� s�f�rsa, hedefin ileri y�nde bir vekt�r� kullan
        }
        targetRotation = Quaternion.LookRotation(targetVelocity.normalized, Vector3.up); // Hedefe g�re kamera d�n�� a��s�n� hesapla

        previousTargetPosition = target.position; // �nceki hedef pozisyonunu g�ncelle

        Quaternion desiredRotation = Quaternion.Lerp(rotation, targetRotation, cameraDelay); // Hedefe g�re kamera d�n���n� geciktir

        transform.position = target.position - desiredRotation * (Vector3.back * distance); // Kameran�n pozisyonunu g�ncelle
        transform.LookAt(target.position + new Vector3(0, height, 0)); // Kameran�n hedefi s�rekli olarak takip etmesini sa�la

        player.rotation = Quaternion.Euler(0f, currentRotation, 0f); // Karakterin yatay d�n�� a��s�n� g�ncelle
    }
}