using UnityEngine;
using System.Collections;

public class Weapon : EquippableItem {
    public bool automatic; //does the weapon continue firing when the fire button is held
    public float cooldown; //how long between each shot can another shot be fired (in seconds)
    public float damage; //how much each shot does
    public float spread; //how much random spread to apply to bullets on prolonged shooting

    public string atlasName; //the gun atlas name that this weapon uses
    public Object projectile; //the bullet prefab to use
    public GameObject parent; //the parent object
    public Player playerScript;

    public float bulletSpeed; //how fast the bullet moves
    public float bulletLife; //how long the bullet lasts
    public Color bulletColor;

    private float _cooldownTimer = 0;
    private bool _mouseReleased = true;

    public override void OnEquip(GameObject parent)
    {
        this.parent = parent;
        Player playerScript = parent.GetComponent<Player>();
        this.playerScript = playerScript;
        playerScript.SetWeaponAtlasByName(atlasName);
        
    }

    public override void OnUnequip(GameObject parent)
    {
        Player playerScript = parent.GetComponent<Player>();
        playerScript.SetWeaponAtlasByName("noGun");
    }

    public override void Update()
    {
        if (_cooldownTimer > 0)
        {
            _cooldownTimer -= Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _mouseReleased = true;
        }
    }

    public override void OnUse()
    {
        if (_cooldownTimer <= 0 && _mouseReleased)
        {
            Transform gunTransform = playerScript.playerBoneAnimation.GetBoneTransform("gun");

            Vector2 directionVector = playerScript.aimVector;

            GameObject bullet = MonoBehaviour.Instantiate(projectile) as GameObject;
            bullet.transform.position = gunTransform.position;
            bullet.rigidbody.velocity = directionVector * bulletSpeed;
            bullet.GetComponent<Bullet>().life = bulletLife;

            if(bulletColor != null)
                bullet.GetComponent<SpriteRenderer>().color = bulletColor;
            Physics.IgnoreCollision(bullet.collider, parent.collider);

            _cooldownTimer = cooldown;
            if (!automatic)
            {
                _mouseReleased = false;
            }
        }

    }

	
}
