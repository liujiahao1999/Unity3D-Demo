using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public float power;
    public Rigidbody2D rig;
    public Animator ani;
    public GameObject obj;
    public GameObject deathobj;

    private bool Death;
    private Vector3 definePos;

    public delegate void DeathNotify();
    public event DeathNotify OnDeath;

    public UnityAction<int> OnScore;
    // Start is called before the first frame update
    void Start()
    {
        definePos = this.transform.position;
        this.Idel();
    }

    public void Init()
    {
        obj.SetActive(true);
        deathobj.SetActive(false);
        this.Death = false;
        this.transform.position = definePos;
        this.Idel();
    }

    // Update is called once per frame
    void Update()
    {
        if (Death)
        return;
        
        if (Input.GetMouseButtonDown(0))
        {
            rig.velocity = Vector2.zero;
            rig.AddForce(new Vector2(0, power), ForceMode2D.Impulse);
        }
        
    }
    public void Fly()
    {
        rig.simulated = true;
        rig.WakeUp();
        ani.SetTrigger("Fly");
    }
    public void Idel()
    {
        rig.Sleep();
        rig.simulated = false;
        ani.SetTrigger("Idel");
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerDie();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("ScoreArea"))
        {

        }
        else
        {
            PlayerDie();
        }

    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("ScoreArea"))
        {
            if (this.OnScore != null)
            {
                this.OnScore(10);
            }
        }
    }

    //死亡逻辑
    public void PlayerDie()
    {
        this.Death = true;
        obj.SetActive(false);
        deathobj.SetActive(true);
        if (this.OnDeath!=null)
        {
            this.OnDeath();
        }
    }

}
