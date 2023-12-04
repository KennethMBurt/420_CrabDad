using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    public bool detected { get; private set; }
    public Vector2 directionToPlayer => target.transform.position - anchor.position;

    public Transform anchor;
    public Vector2 eyesight = Vector2.one;
    public LayerMask detectorLayer;
    public float delay = 0.3f;

    private GameObject target;

    public GameObject Target
    {
        get => target;
        private set
        {
            target = value;
            detected = target != null;
        }
    }

    public bool showDetection = true;
    public Color idleColor = Color.green;
    public Color foundColor = Color.red;


    private void Start()
    {
        StartCoroutine(DetectionCoroutine());
    }

    IEnumerator DetectionCoroutine()
    {
        yield return new WaitForSeconds(delay);
        PerformDetection();
        StartCoroutine(DetectionCoroutine());
    }

    public void PerformDetection()
    {
        Collider2D collider = Physics2D.OverlapBox((Vector2)anchor.position, eyesight, 0, detectorLayer);
        if (collider != null)
        {
            Target = collider.gameObject;
        }
        else
            Target = null;
    }

    private void OnDrawGizmos()
    {
        if (showDetection && anchor != null)
        {
            Gizmos.color = idleColor;
            if (detected)
                Gizmos.color = foundColor;
            Gizmos.DrawCube((Vector2)anchor.position, eyesight);
        }
    }
}
