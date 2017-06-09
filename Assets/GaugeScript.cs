using UnityEngine;
using System.Collections;

public class GaugeScript : MonoBehaviour {

    Transform nail_tranform;
    float init;

    void Awake()
    {
        nail_tranform = transform.FindChild("NailPivot");
        init = nail_tranform.localEulerAngles.z;
    }
    void Update()
    {
        float newz = 0;
        newz = nail_tranform.localEulerAngles.z - 30 * Time.deltaTime;
        if (nail_tranform.localEulerAngles.z <= 60 && nail_tranform.localEulerAngles.z > 0)
            newz = Mathf.Clamp(newz, 60, 0);
        else if (nail_tranform.localEulerAngles.z < 0 && nail_tranform.localEulerAngles.z > -90)
            newz = Mathf.Clamp(newz, 0, -90);
        Vector3 test = new Vector3(0, 0, newz);
        nail_tranform.localEulerAngles = test;
    }
}