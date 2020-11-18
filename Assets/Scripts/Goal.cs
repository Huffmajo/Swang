using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public float forceMin;
    public float forceMax;
    public GameObject fracturedPrefab;
    public bool isBroken;

    void Start()
    {
        isBroken = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            BreakGoal();
        }
    }

    void OnTriggerEnter(Collider col)
    {
       

        Debug.Log("Goal hit");
    }

    void BreakGoal()
    {
        // generate fractured object
        GameObject frac = Instantiate(fracturedPrefab, transform.position, transform.rotation);

        // add small force to fractured parts
        foreach (Rigidbody rb in frac.GetComponentsInChildren<Rigidbody>())
        {
            if (rb != null)
            {
                Vector3 dir = rb.transform.position - transform.position.normalized;
                rb.AddForce(dir * Random.Range(forceMin, forceMax));
            }
        }

        // remove undamaged goal
        // gameObject.SetActive(false);
    }
}
