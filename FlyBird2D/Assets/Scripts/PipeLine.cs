using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeLine : MonoBehaviour
{
    public float speed;
    public float min;
    public float max;

    private float t=0;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    public void Init()
    {
        float y = Random.Range(min, max);

        this.transform.localPosition = new Vector3(0, y, 0);
    }
    // Update is called once per frame
    void Update()
    {
        this.transform.position +=new Vector3(-speed,0)*Time.deltaTime;
        
        t += Time.deltaTime;
        //一段时间后初始化管道位置
        if (t>(speed-3)*4f)
        {
            t = 0;
            Init();
        }

    }
}
