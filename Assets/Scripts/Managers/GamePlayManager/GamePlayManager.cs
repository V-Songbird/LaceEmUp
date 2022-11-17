using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LaceEmUp.Units;

namespace JamOff.Scripts.Managers
{
    public class GamePlayManager : MonoBehaviour
    {
        private static GamePlayManager _instance = null;
        public static  GamePlayManager Instance => _instance;

        #region vars
        [HideInInspector] public AdditiveScenesControl AdditiveScenesControl { get; private set; }
        [HideInInspector] public CommonUI CommonUI { get; private set; }
        [HideInInspector] public WaterColliders WaterColliders { get; private set; }

        //Player
        public GameObject Player;
        [HideInInspector] public Player_CutActions Player_CutActions { get; private set; }
        [HideInInspector] public PlayerManager PlayerManager;
        [HideInInspector] public Player_MovementController Player_MovementController;
        [HideInInspector] public Player_Inventory Player_Inventory;
        [HideInInspector] public Player_OtherActions Player_OtherActions;

        [HideInInspector] public PortalsManager PortalsManager;


        #endregion

        private void Awake()
        {
            MakeItSingleton();
            GetReferences();

            PlayerManager             = FindObjectOfType<PlayerManager>();
            Player_MovementController = FindObjectOfType<Player_MovementController>();
            Player_Inventory          = FindObjectOfType<Player_Inventory>();
            Player_CutActions         = FindObjectOfType<Player_CutActions>();
            Player_OtherActions       = FindObjectOfType<Player_OtherActions>();

        }

        private void Start()
        {
            WaterColliders = FindObjectOfType<WaterColliders>();
        }


        void MakeItSingleton()
        {
            //GamePlay Manager is a singleton, only allow one instance
            if (_instance == null)
            {
                _instance = this;
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
                return;
            }
        }


        void GetReferences()
        {
            AdditiveScenesControl = GetComponentInChildren<AdditiveScenesControl>();
            CommonUI              = GetComponentInChildren<CommonUI>();
            PortalsManager        = GetComponentInChildren<PortalsManager>();
        }


    }
}

