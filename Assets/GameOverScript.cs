using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    private TMPro.TextMeshProUGUI _text;



    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TMPro.TextMeshProUGUI>();
        _text.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        _text.enabled = Tree.GameOver;
    }
}
