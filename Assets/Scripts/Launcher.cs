using UnityEngine;
using System.Collections;

public class Launcher : MonoBehaviour
{
    public float Force = 50f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Launch(Enemy enemy)
    {
        var position = Random.Range(0, 600);

        enemy.transform.position = transform.position + transform.forward * position;
        enemy.GetComponent<Rigidbody>().AddForce(transform.up * Random.Range(Force - 5, Force + 5), ForceMode.Impulse);
    }
}
