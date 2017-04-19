using UnityEngine;
using System.Collections;

public class SpawnIcon : MonoBehaviour
{
    SpriteRenderer m_Render;
    TowerInfo m_Info;
    Vector3 m_CreatePosition;
    bool m_Enough = false;

    void Awake()
    {
        m_Render = GetComponent<SpriteRenderer>();
    }

    public void Load(GameModel gm, TowerInfo info, Vector3 createPostion, bool upSide)
    {
        m_Info = info;

        m_CreatePosition = createPostion;

        //是否足够
        m_Enough = gm.Gold > info.BasePrice;

        //图标
        string path = "Res/Roles/" + (m_Enough ? info.NormalIcon : info.DisabledIcon);
        m_Render.sprite = Resources.Load<Sprite>(path);

        //本地位置
        Vector3 pos = transform.localPosition;
        pos.y = upSide ? Mathf.Abs(pos.y) : -Mathf.Abs(pos.y);
        transform.localPosition = pos;
    }

    void OnMouseDown()
    {
        //if (!m_Enough)
        //return;

        SpawnTowerArgs e = new SpawnTowerArgs()
        {
            Position = m_CreatePosition,
            TowerID = m_Info.ID
        };
        SendMessageUpwards("SpawnTower", e, SendMessageOptions.DontRequireReceiver);
    }
}