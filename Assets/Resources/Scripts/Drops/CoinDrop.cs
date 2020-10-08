using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using UnityEngine;

public class CoinDrop : MonoBehaviour
{
    private SoundController soundController;

    [SerializeField]
    private float lifeTime;
    [SerializeField]
    private float slowShimmerStartTime, fastShimmerStartTime;

    private float currExistanceTime = 0f;

    public Renderer renderer;
    private Color currMatCol;

    private float colourAlpha;

    private void Awake()
    {
        currMatCol = renderer.material.color;
    }

    private void Start()
    {
        GameObject.Find("Score Manager").GetComponent<ScoreManager>().AddMaxScore();
        soundController = GameObject.Find("Sound Controller").GetComponent<SoundController>();
    }

    private void Update()
    {
        if (currExistanceTime < slowShimmerStartTime)
            colourAlpha = 1f;
        if (currExistanceTime >= slowShimmerStartTime && currExistanceTime < fastShimmerStartTime)
            colourAlpha = Mathf.PingPong(Time.time, 0.75f) * 1.333f;
        else if (currExistanceTime >= fastShimmerStartTime && currExistanceTime < lifeTime)
            colourAlpha = Mathf.PingPong(Time.time, 0.25f) * 4f;
        else if (currExistanceTime >= lifeTime)
            Destroy(this.gameObject);

        currExistanceTime += Time.deltaTime;
        renderer.material.color = new Color(currMatCol.r, currMatCol.g, currMatCol.b, colourAlpha);
    }

    private void OnTriggerEnter(Collider col)
    {
        switch (col.gameObject.tag)
        {
            case "Player":
                GameObject.Find("Score Manager").GetComponent<ScoreManager>().AddCurrentScore();
                soundController.PlayCoinPickUpSound();
                Destroy(this.gameObject);
                break;
        }
    }
}