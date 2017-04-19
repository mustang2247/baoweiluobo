using UnityEngine;
using System.Collections;

public class TowerPopup : View 
{
    #region 常量
    #endregion

    #region 事件
    void Awake()
    {
        m_Instance = this;
    }

    void Start()
    {
        HideAllPanels();
    }

    private static TowerPopup m_Instance = null;
    public static TowerPopup Instance
    {
        get 
        {
            return m_Instance;
        }
    }

    #endregion

    #region 字段
    public SpawnPanel CreatePanel;
    public UpgradePanel UpgradePanel;
    #endregion

    #region 属性
    public override string Name
    {
        get { return Consts.V_TowerPopup; }
    }

    public bool IsPopShow
    {
        get
        {
            foreach (Transform child in transform)
            {
                if (child.gameObject.activeSelf)
                    return true;
            }
            return false;
        }
    }

    #endregion

    #region 方法
    public void ShowCreatePanel(Vector3 position, bool upSide)
    {
        HideAllPanels();

        GameModel gm = GetModel<GameModel>();
        CreatePanel.Show(gm, position, upSide);

        
    }

    public void ShowUpgradePanel(Tower tower)
    {
        HideAllPanels();

        GameModel gm = GetModel<GameModel>();
        UpgradePanel.Show(gm, tower);
    }

    public void HideAllPanels()
    {
        CreatePanel.Hide();
        UpgradePanel.Hide();   
    }

    public override void RegisterEvents()
    {
        AttentionEvents.Add(Consts.E_ShowCreate);
        AttentionEvents.Add(Consts.E_ShowUpgrade);
        AttentionEvents.Add(Consts.E_HidePopup);
    }

    public override void HandleEvent(string eventName, object data)
    {
        switch (eventName)
        {
            case Consts.E_ShowCreate:
                ShowCreateArgs e1 = data as ShowCreateArgs;
                ShowCreatePanel(e1.Position, e1.UpSide);
                break;
            case Consts.E_ShowUpgrade:
                ShowUpgradeArgs e2 = data as ShowUpgradeArgs;
                ShowUpgradePanel(e2.Tower);
                break;
            case Consts.E_HidePopup:
                HideAllPanels();
                break;
        }
    }

    void SpawnTower(SpawnTowerArgs e)
    {
        //HideAllPanels();
        SendEvent(Consts.E_SpawnTower, e);
    }

    void UpgradeTower(UpgradeTowerArgs e)
    {
        //HideAllPanels();
        SendEvent(Consts.E_UpgradeTower, e);
    }
	
    void SellTower(SellTowerArgs e)
    {
        //HideAllPanels();
        SendEvent(Consts.E_SellTower, e);
    }
    #endregion

    #region Unity回调
    #endregion

    #region 帮助方法
    #endregion
}
