using System;
using JamOff.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

public class Player_OtherActions : MonoBehaviour
{
    [HideInInspector] public InteractObject interactObject;

    [SerializeField] private GameObject BluePortal;
    [SerializeField] private GameObject RedPortal;

    [SerializeField] private Transform BulletSpawn;

    [SerializeField] private float timeToChangeShoes;

    // Attacks cooldown
    [Header("Attacks Cooldown")]
    [SerializeField] private float basicAttackCooldown;
    [SerializeField] private float marioBrossCooldown;
    [SerializeField] private float portalCooldown;
    
    private float lastAttackTime;
    private bool ChangingShoes;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) GamePlayManager.Instance.CommonUI.InventoryUI.Interact();

        if (Input.GetKeyDown(KeyCode.T) && interactObject != null) interactObject.MakeInteract();

        if (Input.GetKeyDown(KeyCode.Mouse0)) LeftClick();
        if (Input.GetKeyDown(KeyCode.Mouse1)) RightClick();

        //Change Shoes
        if (Input.GetKeyDown(KeyCode.Alpha1)) GetActualShoe(1);

        if (Input.GetKeyDown(KeyCode.Alpha2)) GetActualShoe(2);

        if (Input.GetKeyDown(KeyCode.Alpha3)) GetActualShoe(3);
    }


    private void LeftClick()
    {
        switch (GamePlayManager.Instance.Player_Inventory.actualShoes)
        {
            case ConstantsManager.ShoesTypes.None:
            {
                // Check if attack is in cooldown
                if (Time.time < lastAttackTime + basicAttackCooldown) break;
                lastAttackTime = Time.time;
                
                // Perform attack
                GamePlayManager.Instance.PlayerManager.Attack();
                break;
            }

            case ConstantsManager.ShoesTypes.MarioBross:
            {
                // Check if attack is in cooldown
                if (Time.time < lastAttackTime + marioBrossCooldown) break;
                lastAttackTime = Time.time;

                // Perform attack
                var bulletSpawnPosition = BulletSpawn.position;
                GamePlayManager.Instance.PoolSystem.SpawnFromPool("Pool.MaroShoots", new Vector3(bulletSpawnPosition.x,
                        bulletSpawnPosition.y, bulletSpawnPosition.z - 0.1f), Quaternion.Euler(-90, 0, 0),
                    new Vector3(0.03f, 1, 0.03f));
                break;
            }

            case ConstantsManager.ShoesTypes.Portal:
            {
                // Check if attack is in cooldown
                if (Time.time < lastAttackTime + portalCooldown) break;
                lastAttackTime = Time.time;
                
                // Perform attack
                GamePlayManager.Instance.PortalsManager.MakePortal(true);
                break;
            }

            case ConstantsManager.ShoesTypes.Minecraft: break;

            default: throw new ArgumentOutOfRangeException();
        }
    }

    private void RightClick()
    {
        switch (GamePlayManager.Instance.Player_Inventory.actualShoes)
        {
            case ConstantsManager.ShoesTypes.None: break;

            case ConstantsManager.ShoesTypes.MarioBross: break;

            case ConstantsManager.ShoesTypes.Portal:
            {
                // Check if attack is in cooldown
                if (Time.time < lastAttackTime + portalCooldown) break;
                lastAttackTime = Time.time;
                
                // Perform attack
                GamePlayManager.Instance.PortalsManager.MakePortal(false);
                break;
            }

            case ConstantsManager.ShoesTypes.Minecraft: break;

            default: throw new ArgumentOutOfRangeException();
        }
    }


    private void GetActualShoe(int btnPressed)
    {
        if (GamePlayManager.Instance.Player_Inventory.backpackInventory.Count == 0) return;

        switch (btnPressed)
        {
            case 1:
            {
                if (GamePlayManager.Instance.Player_Inventory.backpackInventory[0] != null)
                    EquipShoe(GamePlayManager.Instance.Player_Inventory.backpackInventory[0].myShoeType);
                break;
            }
            case 2:
            {
                if (GamePlayManager.Instance.Player_Inventory.backpackInventory[1] != null)
                    EquipShoe(GamePlayManager.Instance.Player_Inventory.backpackInventory[1].myShoeType);
                break;
            }
            case 3:
            {
                if (GamePlayManager.Instance.Player_Inventory.backpackInventory[2] != null)
                    EquipShoe(GamePlayManager.Instance.Player_Inventory.backpackInventory[2].myShoeType);
                break;
            }
        }
    }

    private void EquipShoe(ConstantsManager.ShoesTypes shoeType)
    {
        if (ChangingShoes) return;
        if (GamePlayManager.Instance.Player_MovementController.isGrounded == false) return;


        ChangingShoes = true;
        GamePlayManager.Instance.Player_Inventory.ChangeShoes.handleRect.GetComponent<Image>().color = Color.green;
        GamePlayManager.Instance.Player_Inventory.ChangeShoes.size = 0;
        GamePlayManager.Instance.Player_Inventory.ChangeShoes.gameObject.SetActive(true);

        LeanTween.value(GamePlayManager.Instance.Player_Inventory.ChangeShoes.gameObject, 0, 1, timeToChangeShoes)
            .setOnUpdate(value => { GamePlayManager.Instance.Player_Inventory.ChangeShoes.size = value; })
            .setOnComplete(() =>
            {
                ChangingShoes = false;
                GamePlayManager.Instance.Player_Inventory.actualShoes = shoeType;
                GamePlayManager.Instance.Player_Inventory.ChangeShoes.gameObject.SetActive(false);


                if (shoeType == ConstantsManager.ShoesTypes.Minecraft)
                    GamePlayManager.Instance.WaterColliders.ActiveWaterColliders();
                else
                    GamePlayManager.Instance.WaterColliders.DesactiveWaterColliders();
            });
    }


    public void CancelAllActions()
    {
        if (ChangingShoes) CancelChangeShoes();
    }

    private void CancelChangeShoes()
    {
        LeanTween.cancel(GamePlayManager.Instance.Player_Inventory.ChangeShoes.gameObject);
        GamePlayManager.Instance.Player_Inventory.ChangeShoes.handleRect.GetComponent<Image>().color = Color.red;
        ChangingShoes = false;
        LeanTween.scale(GamePlayManager.Instance.Player_Inventory.ChangeShoes.gameObject,
            new Vector3(0.0125f, 0.0175f, 1), 0.25f);
        LeanTween.color(GamePlayManager.Instance.Player_Inventory.ChangeShoes.handleRect, new Color(255, 0, 0, 0),
            0.25f).setOnComplete(() =>
        {
            GamePlayManager.Instance.Player_Inventory.ChangeShoes.gameObject.SetActive(false);
            GamePlayManager.Instance.Player_Inventory.ChangeShoes.gameObject.transform.localScale =
                new Vector3(0.01f, 0.01f, 1);
            GamePlayManager.Instance.Player_Inventory.ChangeShoes.handleRect.GetComponent<Image>().color = Color.green;
        });
    }
}