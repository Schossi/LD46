using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    public Transform Head;
    public Rigidbody Rigidbody;
    public float Speed;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Rigidbody.AddForce(new Vector3(Head.right.x, 0f, Head.right.z).normalized * moveHorizontal * Speed, ForceMode.VelocityChange);
        Rigidbody.AddForce(new Vector3(Head.forward.x, 0f, Head.forward.z).normalized * moveVertical * Speed, ForceMode.VelocityChange);
    }
}
