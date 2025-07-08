using UnityEngine;
using System.Collections.Generic;

public class ObstacleTransparency : MonoBehaviour
{
    public Transform player;
    public LayerMask obstacleLayer;
    public float transparentAlpha = 0.3f;

    private List<FadeObject> hitObjects = new List<FadeObject>();
    private List<FadeObject> previousObjects = new List<FadeObject>();

    void Update()
    {
        Vector3 direction = player.position - transform.position;
        float distance = Vector3.Distance(transform.position, player.position);
        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, distance, obstacleLayer);

        hitObjects.Clear();

        foreach (RaycastHit hit in hits)
        {
            FadeObject fadeObj = hit.collider.GetComponent<FadeObject>();
            if (fadeObj != null)
            {
                fadeObj.FadeTo(transparentAlpha);
                hitObjects.Add(fadeObj);
            }
        }

        // Geri eski haline getirme
        foreach (FadeObject obj in previousObjects)
        {
            if (!hitObjects.Contains(obj))
            {
                obj.Restore();
            }
        }

        previousObjects = new List<FadeObject>(hitObjects);
    }
}
