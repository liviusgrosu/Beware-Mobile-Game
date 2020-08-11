using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDrop : MonoBehaviour
{
    private SoundController soundController;

    [SerializeField]
    private float lifeTime;
    [SerializeField]
    private float slowShimmerStartTime, fastShimmerStartTime;

    private float currExistanceTime = 0f;

    private Renderer rend;
    private Color currMatCol;

    private float colourAlpha;

    [SerializeField]
    private EnumDefinitions.WeaponType weaponType;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        currMatCol = rend.material.color;
    }

    private void Start()
    {
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
        rend.material.color = new Color(currMatCol.r, currMatCol.g, currMatCol.b, colourAlpha);
    }

    private void OnTriggerEnter(Collider col)
    {
        switch (col.gameObject.tag)
        {
            case "Player":
                col.gameObject.GetComponent<WeaponController>().SwitchWeapons(weaponType);
                soundController.PlayWeaponPickUpSound();
                Destroy(this.gameObject);
                break;
        }
    }
}
