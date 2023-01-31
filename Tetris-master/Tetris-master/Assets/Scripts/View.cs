using UnityEngine;

public class View : MonoBehaviour {
    public GameObject menuTabbar;
    public GameObject restartButton;
    public BasePanel[] panels;
    public GameObject updateRoolTip;
    private BasePanel mCurrentPanel;

    private bool mIsGameOver;
    /// <summary>
    /// ボタンを押したときのindex, indexパネルと配列中のpanels, Inspector windows view
    /// </summary>
    /// <param name="index"></param>
    public void TabbarOnTab(int index) {
        HidePanel(mCurrentPanel);
        //もしrestartかplayが押されたら、
        if (index == 0 || index == -1) {
            bool isRestart;
            //restart
            if (index == 0) {
                isRestart = true;
                GameManager.Instance.ResetSpeed();
            }
            //start or continue
            else {
                if (mIsGameOver) {
                    isRestart = true;
                    mIsGameOver = false;
                }
                else {
                    isRestart = false;
                }
            }
            mCurrentPanel = UIManager.Instance.ShowOne(panels[0]);
            HideMenuTabbar();
            EventManager.Instance.Fire(UIEvent.ENTER_PLAY_STATE, isRestart);
        }
        else {
            mCurrentPanel = UIManager.Instance.ShowOne(panels[index]);
        }
        AudioManager.Instance.PlayCursor();
    }

    public void UpdatePanelInfo(int panelType, int[] info) {
        mCurrentPanel = panels[panelType];
        if (mCurrentPanel!=null)
        {
            mCurrentPanel.UpdatePanelInfo(info);
        }
    }

    public void PauseGame() {
        ShowMenuTabbar();
        HidePanel(panels[0]);
        AudioManager.Instance.PlayCursor();
    }

    public void ShowMenuTabbar() {
        restartButton.SetActive(!mIsGameOver);
        menuTabbar.SetActive(true);
    }

    public void HideMenuTabbar() {
        menuTabbar.SetActive(false);
    }

    public void GameOver() {
        //hide start button
        mIsGameOver = true;
        ShowMenuTabbar();
        TabbarOnTab(3);

    }

    public void ShowUpdateRoolTip() {
        if (!updateRoolTip.activeSelf) {
            updateRoolTip.SetActive(true);
        }
    }

    public void ShowAlert() {
        UIManager.Instance.ShowOne(panels[4]);
    }

    public void ShowDifficultyPanel() {
        UIManager.Instance.ShowOne(panels[6]);
    }

    public void ShowDefinedButtonPanel() {
        UIManager.Instance.ShowOne(panels[5]);
    }

    private static void HidePanel(BasePanel targetPanel) {
        if (targetPanel != null) {
            UIManager.Instance.SetClose(targetPanel);
        }
    }
}
