using UnityEngine;

public class UI : MonoBehaviour
{
    public Menu Menu;
    public HUD HUD;

    public void SetMenuMode(Menu.MenuType menuType)
    {
        Menu.gameObject.SetActive(true);
        Menu.Init(menuType);
        
        HUD.gameObject.SetActive(false);
        HUD.DeInit();
    }

    public void SetHUDMode()
    {
        Menu.gameObject.SetActive(false);
        Menu.DeInit();
        
        HUD.gameObject.SetActive(true);
        HUD.Init();
    }
}
