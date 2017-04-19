using System.Collections.Generic;
using System.Text;
using UnityEngine;

public abstract class Tower : ReusbleObject
{
    public int ID { get; private set; }
    public int Level
    {
        get { return m_Level; }
        set
        {
            m_Level = Mathf.Clamp(value, 0, MaxLevel);
            transform.localScale = Vector3.one * (1 + m_Level * 0.25f);
        }
    }
    public int MaxLevel { get; private set; }
    public bool IsTopLevel { get { return Level >= MaxLevel; } }
    public float ShotRate { get; private set; }
    public float GuardRange { get; private set; }
    public int BasePrice { get; private set; }
    public int UseBulletID { get; private set; }
    public int Price { get { return BasePrice * Level; } }
    public Tile Tile { get; private set; }
    public Rect MapRect { get; private set; }

    Monster m_Target;
    Animator m_Animator;
    int m_Level;
    float m_LastShotTime = 0;

    protected virtual void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (m_Target == null)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Monster");
            foreach (GameObject go in objects)
            {
                Monster monster = go.GetComponent<Monster>();

                if (!monster.IsDead && Vector3.Distance(transform.position, monster.transform.position) <= GuardRange)
                {
                    m_Target = monster;
                    break;
                }
            }
        }
        else
        {
            if (m_Target.IsDead || Vector3.Distance(transform.position, m_Target.transform.position) > GuardRange)
            {
                m_Target = null;
                LookAt(null);
                return;
            }

            //朝向目标
            LookAt(m_Target);

            //发射判断
            if (Time.time >= m_LastShotTime + 1f / ShotRate)
            {
                //创建子弹
                Shot(m_Target);

                //记录发射时间
                m_LastShotTime = Time.time;
            }
        }
    }

    public void Load(int towerID, Tile tile,Rect mapRect)
    {
        TowerInfo info = Game.Instance.StaticData.GetTowerInfo(towerID);
        MaxLevel = info.MaxLevel;
        ShotRate = info.ShotRate;
        BasePrice = info.BasePrice;
        GuardRange = info.GuardRange;
        UseBulletID = info.UseBulletID;
        Level = 1;

        Tile = tile;
        MapRect = mapRect;
    }

    public virtual void Shot(Monster monster)
    {
        m_Animator.SetTrigger("IsAttack");
    }

    void LookAt(Monster target)
    {
        if (target == null)
        {
            Vector3 eulerAngles = transform.eulerAngles;
            eulerAngles.z = 0;
            transform.eulerAngles = eulerAngles;
        }
        else
        {
            Vector3 vec = (target.Position - transform.position).normalized;
            float angle = Mathf.Atan2(vec.y, vec.x);
            Vector3 eulerAngles = transform.eulerAngles;
            eulerAngles.z = angle * Mathf.Rad2Deg - 90;
            transform.eulerAngles = eulerAngles;
        }
    }

    public override void OnSpawn()
    {
        
    }

    public override void OnUnspawn()
    {
        m_Animator.ResetTrigger("IsAttack");
        m_Animator.Play("Idle");
        m_Target = null;
        m_LastShotTime = 0;

        Tile = null;

        Level = 0;
        MaxLevel = 0;
        ShotRate = 0;
        BasePrice = 0;
    }
}