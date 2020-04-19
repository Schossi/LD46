using UnityEngine;
using System.Collections;

public class Holy : MonoBehaviour
{
    private static Holy _instance;
    public static Holy Instance => _instance;

    public Holy()
    {
        _instance = this;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
