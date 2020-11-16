using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject pivot;
    
    private Rigidbody rb;
    private Vector3 startingPos;
    private HingeJoint joint;
    private LineRenderer line;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startingPos = transform.position;
        pivot.SetActive(false);
        joint = pivot.GetComponent<HingeJoint>();
        line = pivot.GetComponent<LineRenderer>();
    }

    void Update()
    {
        RaycastHit hit;

        // click down
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                // 
                joint.connectedAnchor = pivot.transform.position - this.transform.position;
                pivot.transform.position = new Vector3(hit.point.x, hit.point.y, this.transform.position.z);

                // draw line between pivot and ball
                line.SetPosition(0, pivot.transform.position);
                line.SetPosition(1, this.transform.position);

                // activate pivot
                pivot.SetActive(true);
            }
        }

        // click held
        if (Input.GetMouseButton(0))
        {
            if (pivot.activeSelf)
            {
                line.SetPosition(1, this.transform.position);
            }
        }

        // click release
        if (Input.GetMouseButtonUp(0))
        {
            pivot.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Danger"))
        {
            LevelFail();
        }

        if (col.gameObject.CompareTag("Goal"))
        {
            LevelComplete();
        }
    }

    void LevelFail()
    {
        // respawn
        rb.velocity = Vector3.zero;
        transform.position = startingPos;

        // remove pivot
        pivot.SetActive(false);
    }

    void LevelComplete()
    {
        Debug.Log("Level Completed");
    }
}
