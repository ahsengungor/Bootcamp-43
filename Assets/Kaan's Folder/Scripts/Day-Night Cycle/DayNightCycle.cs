using System;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public float sunAngle;
    public Light sun;                // Directional Light referans�
    public float dayDuration = 120f; // Bir g�n ka� saniyede ge�ecek?
    private float time;             // 0-1 aras�nda normalized zaman (0 = gece yar�s�, 0.5 = ��len, 1 = gece yar�s�)

    void Update()
    {
        CycleHandler();
        TimeScaleHandler();
    }

    private void TimeScaleHandler()
    {
        if (Input.GetKey(KeyCode.LeftShift))     Time.timeScale = 2f;
        else                                     Time.timeScale = 1f;
    }

    private void CycleHandler()
    {
        time += Time.deltaTime / dayDuration;
        if (time > 1f) time -= 1f;

        // G�ne�i d�nd�r
        sunAngle = time * 360f - 90f; // -90 sabah, 90 ak�am
        sun.transform.rotation = Quaternion.Euler(sunAngle, 170f, 0f);

        // G�ne�in yo�unlu�u ve rengi
        sun.intensity = Mathf.Clamp01(Mathf.Sin(time * Mathf.PI * 2f)); // 0-1 aras�
        sun.color = Color.Lerp(Color.black, Color.white, sun.intensity);
    }
}
