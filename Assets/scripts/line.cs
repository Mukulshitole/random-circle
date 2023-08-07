using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class line : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCol;
    public float lineDuration = 3.0f; // Duration of the line in seconds
    public GameObject prefabToDestroy; // Assign the prefab in the Inspector

    public AnimationCurve fadeCurve; // Curve for fading out the line's alpha

    private List<Vector2> points;
    private float timeElapsed = 0.0f;
    private float startAlpha; // Initial alpha of the line's color

    private void Start()
    {
        startAlpha = lineRenderer.material.color.a; // Store the initial alpha value
    }

    private void Update()
    {
        if (points != null)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed >= lineDuration)
            {
                FadeOutLine();
            }
        }
    }

    public void UpdateLine(Vector2 mousePos)
    {
        if (points == null)
        {
            points = new List<Vector2>();
            SetPoint(mousePos);
            return;
        }

        if (Vector2.Distance(points.Last(), mousePos) > .1f)
            SetPoint(mousePos);
    }

    void SetPoint(Vector2 point)
    {
        points.Add(point);

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPosition(points.Count - 1, point);

        if (points.Count > 1)
            edgeCol.points = points.ToArray();
    }

    void FadeOutLine()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0.0f;
        Color startColor = lineRenderer.material.color;

        while (elapsedTime < lineDuration)
        {
            float normalizedTime = elapsedTime / lineDuration;
            float fadeAlpha = Mathf.Lerp(startAlpha, 0.0f, fadeCurve.Evaluate(normalizedTime));

            Color newColor = startColor;
            newColor.a = fadeAlpha;

            lineRenderer.material.color = newColor;

            // Calculate alpha for each line segment
            for (int i = 1; i < points.Count; i++)
            {
                Color segmentColor = lineRenderer.startColor; // Use startColor for the alpha of the line segment
                segmentColor.a = Mathf.Lerp(startAlpha, 0.0f, fadeCurve.Evaluate(normalizedTime));

                lineRenderer.SetColors(segmentColor, segmentColor);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject); // Destroy the line object after fading out
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == prefabToDestroy)
        {
            Destroy(other.gameObject); // Destroy the collided prefab GameObject
        }
    }
}
