using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using UnityEngine;

public class CoinDrop : MonoBehaviour
{
    [SerializeField]
    private float lifeTime;
    [SerializeField]
    private float shimmerStartTime;

    private void Awake()
    {
        StartCoroutine(WaitAndDestroy());
    }

    private void Start()
    {
        GameObject.Find("Score Manager").GetComponent<ScoreManager>().AddMaxScore();
    }

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision col)
    {
        switch (col.gameObject.tag)
        {
            case "Player":
                GameObject.Find("Score Manager").GetComponent<ScoreManager>().AddCurrentScore();
                Destroy(this.gameObject);
                break;
        }
    }
}
