using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    private Material monsterMaterial;

    public bool isFinished;

    // Start is called before the first frame update
    void Start()
    {
        monsterMaterial = GetComponent<Renderer>().material;
        monsterMaterial.color = new Color(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (GetDistanceToPlayer().magnitude <= 5 && isFinished)
        {
            Destroy(this);
        }
    }

    public void SetColor(Color _color)
    {
        monsterMaterial.color = _color;
    }

    private Vector3 GetDistanceToPlayer()
    {
        return GameObject.Find("FPSController").transform.position - transform.position;
    }
}
