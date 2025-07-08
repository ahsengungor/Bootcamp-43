using System;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public float sunAngle;
    public Light sun;                // Directional Light referansý
    public float dayDuration = 120f; // Bir gün kaç saniyede geçecek?
    private float time;             // 0-1 arasýnda normalized zaman (0 = gece yarýsý, 0.5 = öðlen, 1 = gece yarýsý)

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

        // Güneþi döndür
        sunAngle = time * 360f - 90f; // -90 sabah, 90 akþam
        sun.transform.rotation = Quaternion.Euler(sunAngle, 170f, 0f);

        // Güneþin yoðunluðu ve rengi
        sun.intensity = Mathf.Clamp01(Mathf.Sin(time * Mathf.PI * 2f)); // 0-1 arasý
        sun.color = Color.Lerp(Color.black, Color.white, sun.intensity);
    }
}
