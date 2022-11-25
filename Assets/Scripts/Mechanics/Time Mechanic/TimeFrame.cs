using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFrame {
    public Vector3 position;
    public Quaternion rotation;

    public TimeFrame(Vector3 position, Quaternion rotation) {
        this.position = position;
        this.rotation = rotation;
    }
}
