using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;          // D�nd�rmek istedi�in obje (�rne�in odak noktas�)
    public float rotationSpeed = 5f;  // D�nd�rme h�z�
    public float distance = 5f;       // Objeye olan mesafe

    private float yaw = 0f;  // Yatay a��
    private float pitch = 0f; // Dikey a��

    void Start()
    {
        if (target == null)
        {
            Debug.LogError("Target atanmad�!");
            enabled = false;
            return;
        }

        // Ba�lang�� pozisyonu
        Vector3 direction = new Vector3(0, 0, -distance);
        transform.position = target.position + direction;
        transform.LookAt(target);
    }

    void Update()
    {
        if (Input.GetMouseButton(1)) // Sa� t�k bas�l� m�
        {
            // Fare hareketi
            yaw += Input.GetAxis("Mouse X") * rotationSpeed;
            pitch -= Input.GetAxis("Mouse Y") * rotationSpeed;

            // Pitch a��s�n� s�n�rla (mesela -80 ile 80 derece aras�)
            pitch = Mathf.Clamp(pitch, -80f, 80f);

            // Hesaplanan a��larla yeni pozisyonu ayarla
            Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
            Vector3 offset = rotation * new Vector3(0, 0, -distance);

            transform.position = target.position + offset;
            transform.LookAt(target);
        }
    }
}
