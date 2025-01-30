using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwordThrowPoint : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Camera mainCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;
        lineRenderer.sharedMaterial.SetColor("_Color", Color.gray);
        mainCamera = Camera.main;
    }
    private Vector3 initialPosition;
    private Vector3 currentPosition;
    private Vector3 cat;
    private Vector3 cat2;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("yo");
            initialPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            //initialPosition = Camera.main.ScreenToWorldPoint( new Vector3(Input.mousePosition.x, 2, Input.mousePosition.y));
        }
        if (Input.GetMouseButton(0))
        {
            currentPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            //currentPosition = Camera.main.ScreenToWorldPoint( new Vector3(Input.mousePosition.x, 2, Input.mousePosition.y));
            //cat = new Vector3 (initialPosition.x, initialPosition.y, 10f);
            //cat2 = new Vector3(currentPosition.x, currentPosition.y, 10f);

            lineRenderer.SetPosition(0, initialPosition);
            lineRenderer.SetPosition(1, currentPosition);
            Debug.Log(currentPosition);
            lineRenderer.enabled = true;

        }
        if (Input.GetMouseButtonUp(0))
        {
            lineRenderer.enabled = false;

        }
    }
        /*if (Input.GetMouseButtonDown(0))
        {
            initialPosition = GetCurrentMousePosition().GetValueOrDefault();
            lineRenderer.SetVertexCount(1);
            lineRenderer.SetPosition(0, initialPosition);

            lineRenderer.enabled = true;
        }
        else if (Input.GetMouseButton(0))
        {
            currentPosition = GetCurrentMousePosition().GetValueOrDefault();
            lineRenderer.SetVertexCount(2);
            Debug.Log(currentPosition);
            lineRenderer.SetPosition(1, currentPosition);
        }
        

    }*/

   /* private Vector3? GetCurrentMousePosition()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var plane = new Plane(Vector3.forward, Vector3.zero);

        float rayDistance;
        if (plane.Raycast(ray, out rayDistance))
        {
            return ray.GetPoint(rayDistance);

        }

        return null;
    }*/
}
