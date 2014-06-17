using UnityEngine;


public class ViewModel : MonoBehaviour
{
    public NguiRootContext View;
    public ArmoryContext Context;
    public Transform SelectionDisplay;
    public UIGrid Grid;

    void Awake()
    {
        Context = new ArmoryContext(SelectionDisplay, Grid);
        View.SetContext(Context);

        GenGun("gun1", "Screw", Color.blue, true, .15f);
        GenGun("gun2", "Emoticon - Skull", Color.red, true, .5f);

    }

    void GenGun(string atlasName, string iconName, Color color, bool auto, float cd)
    {
        Weapon weapon = new Weapon()
        {
            Icon = iconName,
            bulletSpeed = 20,
            bulletLife = 1,
            automatic = auto,
            cooldown = cd,
            atlasName = atlasName,
            usable = true,
            projectile = Resources.Load("DynamicPrefabs/basicBullet"),
            bulletColor = color,
            Description = "This is some text about this item, isn't it great?"
        };
        Context.AddItem(weapon);
    }

    void Update()
    {
        
    }

    public void OnClick()
    {
        GenGun("gun3", "Emoticon - Skull", Color.blue, true, .5f);
        //Context.SelectedItemDescription = "BLA BLA BLA";
    }
}
