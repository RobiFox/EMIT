using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingCube : MonoBehaviour {
    void Start() {
        float scale = Random.Range(0.1f, 0.6f);
        transform.localScale = new Vector3(scale, scale, scale);
        transform.Rotate(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        var position = transform.position;
        position = new Vector3(position.x + Random.Range(-2, 2), position.y + Random.Range(-2, 2), position.z + Random.Range(-2, 2));
        transform.position = position;
        GetComponent<Rigidbody>().AddForce(Random.onUnitSphere * Random.Range(0.1f, 10));
    }
}
