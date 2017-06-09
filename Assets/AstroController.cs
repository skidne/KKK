using UnityEngine;

public class AstroController : MonoBehaviour {

    public float speed;
    public float rotOffset;
    Vector3 _initPos;

    void Start()
    {
        _initPos = transform.position;
    }

	void Update () {
        //transform.Translate(Input.GetAxis("Horizontal") * transform.right * speed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x + (Input.GetAxis("Horizontal") * speed * Time.deltaTime), transform.position.y, transform.position.z);
        transform.localEulerAngles = new Vector3(_initPos.x, _initPos.y,  _initPos.z + -Input.GetAxis("Horizontal") * Mathf.Lerp(0, rotOffset, Time.deltaTime * 25));
    }
}
