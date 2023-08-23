using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target, player; // Hedef karakterin Transform bileþeni
    public float distance; // Kamera ile hedef arasýndaki mesafe
    public float height; // Kameranýn yüksekliði
    public float rotationSpeed = 5f; // Kamera ve karakter dönüþ hýzý
    public float cameraRotationSpeed = 2f; // Kamera dönüþ hýzý
    public float cameraDelay = 0.1f; // Kameranýn karaktere göre gecikme süresi

    private float currentRotation = 0f; // Mevcut kamera yatay dönüþü
    private float currentVerticalRotation = 0f; // Mevcut kamera dikey dönüþü

    private Vector3 previousTargetPosition; // Önceki hedef pozisyonu
    private Quaternion initialRotation; // Ýlk kamera dönüþ açýsý
    private Quaternion targetRotation; // Hedef kamera dönüþ açýsý

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Fareyi ortaya sabitle
        Cursor.visible = false; // Farenin görünürlüðünü kapat
        previousTargetPosition = target.position; // Önceki hedef pozisyonunu baþlangýçta güncelle
        initialRotation = transform.rotation; // Ýlk kamera dönüþ açýsýný kaydet
    }

    private void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X"); // Fare hareketinin yatay eksendeki deðerini al
        float mouseY = Input.GetAxis("Mouse Y"); // Fare hareketinin dikey eksendeki deðerini al

        currentRotation += mouseX * rotationSpeed; // Mevcut yatay dönüþ açýsýný fare hareketine göre güncelle
        currentVerticalRotation -= mouseY * cameraRotationSpeed; // Mevcut dikey dönüþ açýsýný fare hareketine göre güncelle
        currentVerticalRotation = Mathf.Clamp(currentVerticalRotation, -60f, 60f); // Dikey dönüþ açýsýný sýnýrla

        Quaternion rotation = Quaternion.Euler(currentVerticalRotation, currentRotation, 0f); // Yeni kamera dönüþ açýsýný hesapla

        Vector3 targetVelocity = (target.position - previousTargetPosition) / Time.deltaTime; // Hedefin anlýk hýzýný hesapla
        if (targetVelocity == Vector3.zero)
        {
            targetVelocity = target.forward; // Eðer hedefin anlýk hýzý sýfýrsa, hedefin ileri yönde bir vektörü kullan
        }
        targetRotation = Quaternion.LookRotation(targetVelocity.normalized, Vector3.up); // Hedefe göre kamera dönüþ açýsýný hesapla

        previousTargetPosition = target.position; // Önceki hedef pozisyonunu güncelle

        Quaternion desiredRotation = Quaternion.Lerp(rotation, targetRotation, cameraDelay); // Hedefe göre kamera dönüþünü geciktir

        transform.position = target.position - desiredRotation * (Vector3.back * distance); // Kameranýn pozisyonunu güncelle
        transform.LookAt(target.position + new Vector3(0, height, 0)); // Kameranýn hedefi sürekli olarak takip etmesini saðla

        player.rotation = Quaternion.Euler(0f, currentRotation, 0f); // Karakterin yatay dönüþ açýsýný güncelle
    }
}