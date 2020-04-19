using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Tree : MonoBehaviour
{
    public GameObject Normal;
    public GameObject Dire;
    public bool IsSpecial;

    public static bool GameOver = false;

    private static List<Tree> _trees = new List<Tree>();
    public static List<Tree> Trees => _trees;

    // Use this for initialization
    void Start()
    {
        _trees.Add(this);

        Dire.SetActive(false);

        transform.eulerAngles = new Vector3(0f, Random.Range(0, 360), 0f);
        transform.localScale = new Vector3(Random.Range(0.8f, 1.2f), Random.Range(0.8f, 1.2f), Random.Range(0.8f, 1.2f));
    }

    public static void Paint(Vector3 position, bool dire)
    {
        foreach (var tree in Trees)
        {
            var maxDistance = (dire ? 30f : 50f);

            if (Vector3.Distance(tree.transform.position, position) <= maxDistance)
            {
                if (tree.IsSpecial && dire)
                    GameOver = true;

                tree.Normal.SetActive(!dire);
                tree.Dire.SetActive(dire);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameOver && IsSpecial && Input.GetKeyDown(KeyCode.Return))
        {
            GameOver = false;
            _trees.Clear();
            Enemy.Active = 0;
            Enemy.Score = 0;

            SceneManager.LoadScene(0);
        }
    }
}
