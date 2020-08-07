using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPDrop : MonoBehaviour
{
    [SerializeField]
    private float lifeTime;
    [SerializeField]
    private float slowShimmerStartTime, fastShimmerStartTime;

    private float currExistanceTime = 0f;

    private Renderer rend;
    private Color currMatCol;

    private float colourAlpha;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        currMatCol = rend.material.color;
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
        rend.material.color = new Color(currMatCol.r, currMatCol.g, currMatCol.b, colourAlpha);
    }

    private void OnTriggerEnter(Collider col)
    {
        switch (col.gameObject.tag)
        {
            case "Player":
                PlayerHealthSystem playerHealth = col.gameObject.GetComponent<PlayerHealthSystem>();
                if (!playerHealth.IsHealthFull())
                {
                    playerHealth.ChangeHealth(1);
                    Destroy(this.gameObject);
                }
                break;
        }
    }
}
