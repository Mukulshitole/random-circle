using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCreator : MonoBehaviour
{
    public GameObject linePrefab;
    private line activeLine;

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            StartNewLine();
        }

        if (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            EndActiveLine();
        }

        if (activeLine != null)
        {
            Vector2 inputPosition = GetInputPosition();
            activeLine.UpdateLine(inputPosition);
        }
    }

    private void StartNewLine()
    {
        Vector2 inputPosition = GetInputPosition();
        GameObject lineGO = Instantiate(linePrefab);
        activeLine = lineGO.GetComponent<line>();
        activeLine.UpdateLine(inputPosition);
    }

    private void EndActiveLine()
    {
        activeLine = null;
    }

    private Vector2 GetInputPosition()
    {
        Vector2 inputPosition = Vector2.zero;

        if (Input.touchCount > 0)
        {
            inputPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        }
        else if (Input.GetMouseButton(0))
        {
            inputPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        return inputPosition;
    }
}
