using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoorTrigger : MonoBehaviour
{
    [System.Serializable]
    public class RotatingObject
    {
        public Transform target;
        public float zRotation;

        [HideInInspector] public Quaternion originalRotation;
    }

    [Header("Döndürülecek Objeler ve Açýlar")]
    public List<RotatingObject> rotatingObjects = new List<RotatingObject>();

    [Header("Animasyon Süresi")]
    public float rotationDuration = 1f;

    private Coroutine rotationCoroutine;

    private void Start()
    {
        foreach (var obj in rotatingObjects)
        {
            if (obj.target != null)
            {
                obj.originalRotation = obj.target.localRotation;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsAllowedTag(other.tag))
        {
            Debug.Log($"{other.tag} trigger'a girdi.");

            if (rotationCoroutine != null)
                StopCoroutine(rotationCoroutine);

            rotationCoroutine = StartCoroutine(RotateObjects());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsAllowedTag(other.tag))
        {
            Debug.Log($"{other.tag} trigger'dan çýktý.");

            if (rotationCoroutine != null)
                StopCoroutine(rotationCoroutine);

            rotationCoroutine = StartCoroutine(RotateObjectsBack());
        }
    }

    private bool IsAllowedTag(string tag)
    {
        return tag == "Player" || tag == "Chief";
    }

    IEnumerator RotateObjects()
    {
        float elapsed = 0f;

        List<Quaternion> startRotations = new List<Quaternion>();
        List<Quaternion> targetRotations = new List<Quaternion>();

        foreach (var obj in rotatingObjects)
        {
            if (obj.target != null)
            {
                Quaternion startRot = obj.target.localRotation;
                Quaternion targetRot = startRot * Quaternion.Euler(0, 0, obj.zRotation);
                startRotations.Add(startRot);
                targetRotations.Add(targetRot);
            }
            else
            {
                startRotations.Add(Quaternion.identity);
                targetRotations.Add(Quaternion.identity);
            }
        }

        while (elapsed < rotationDuration)
        {
            float t = elapsed / rotationDuration;
            for (int i = 0; i < rotatingObjects.Count; i++)
            {
                if (rotatingObjects[i].target != null)
                    rotatingObjects[i].target.localRotation = Quaternion.Lerp(startRotations[i], targetRotations[i], t);
            }
            elapsed += Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < rotatingObjects.Count; i++)
        {
            if (rotatingObjects[i].target != null)
                rotatingObjects[i].target.localRotation = targetRotations[i];
        }
    }

    IEnumerator RotateObjectsBack()
    {
        float elapsed = 0f;

        List<Quaternion> startRotations = new List<Quaternion>();

        foreach (var obj in rotatingObjects)
        {
            if (obj.target != null)
            {
                startRotations.Add(obj.target.localRotation);
            }
            else
            {
                startRotations.Add(Quaternion.identity);
            }
        }

        while (elapsed < rotationDuration)
        {
            float t = elapsed / rotationDuration;
            for (int i = 0; i < rotatingObjects.Count; i++)
            {
                if (rotatingObjects[i].target != null)
                    rotatingObjects[i].target.localRotation = Quaternion.Lerp(startRotations[i], rotatingObjects[i].originalRotation, t);
            }
            elapsed += Time.deltaTime;
            yield return null;
        }

        foreach (var obj in rotatingObjects)
        {
            if (obj.target != null)
                obj.target.localRotation = obj.originalRotation;
        }
    }
}