using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTrigger : MonoBehaviour
{
    private bool collided = false;
    public MonsterManager monster;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!collided)
        {
            monster.SetColor(Color.white);
            monster.isFinished = true;
            monster.transform.position = new Vector3(73.0f, 1.5f, 20.0f);

            collided = true;
        }    
    }
}
