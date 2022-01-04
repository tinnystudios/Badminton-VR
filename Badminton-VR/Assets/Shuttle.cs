using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuttle : MonoBehaviour
{
    public bool Done;
    public Rigidbody Rigidbody;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        transform.forward = Vector3.Lerp(transform.forward, Rigidbody.velocity.normalized, 5 * Time.deltaTime);
    }
}
