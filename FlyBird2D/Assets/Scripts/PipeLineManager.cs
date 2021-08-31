
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeLineManager : MonoBehaviour
{

    public GameObject pipeObj;
    List<PipeLine> PList = new List<PipeLine>();
    public float speed;
    Coroutine runner;
    public void Init()
    {
        for (int i = 0; i < PList.Count; i++)
        {
            Destroy(PList[i].gameObject);
        }
        PList.Clear();
        Debug.Log("This List is Clear");
    }

    
    public void StartRun()
    {
        runner = StartCoroutine(GeneratePipeLines());
    }

    public void StopCreate()
    {
        StopCoroutine(runner);
        for (int i = 0; i < PList.Count; i++)
        {
            PList[i].enabled = false;
        }
    }
    //生成管道，数量不超过4个
    IEnumerator GeneratePipeLines()
    {
        for (int i=0;i<4;i++)
        {
            if (PList.Count<4)
            {
                GeneratePipeLine();
            }
            else
            {
                PList[i].enabled = true;
                PList[i].Init();
            }
            yield return new WaitForSeconds(speed);
        }
    }

    void GeneratePipeLine()
    {
        if (PList.Count<4)
        {
            GameObject obj = Instantiate(pipeObj, this.transform);
            PipeLine p = obj.GetComponent<PipeLine>();
            SetSpeed(p);
            PList.Add(p);
        }
    }
    public void SetSpeed(PipeLine p)
    {
        p.speed = speed+3;
    }
}
