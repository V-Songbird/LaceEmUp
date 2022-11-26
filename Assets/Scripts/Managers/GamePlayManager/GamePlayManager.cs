using LaceEmUp.Units;
using UnityEngine;

namespace JamOff.Scripts.Managers
{
    public class GamePlayManager : MonoBehaviour
    {
        public static GamePlayManager Instance { get; private set; }

        private void Awake()
        {
            MakeItSingleton();
            GetReferences();

            PlayerManager = FindObjectOfType<PlayerManager>();
            Player_MovementController = FindObjectOfType<Player_MovementController>();
            Player_Inventory = FindObjectOfType<Player_Inventory>();
            Player_CutActions = FindObjectOfType<Player_CutActions>();
            Player_OtherActions = FindObjectOfType<Player_OtherActions>();
        }

        private void Start()
        {
            WaterColliders = FindObjectOfType<WaterColliders>();
        }


        private void MakeItSingleton()
        {
            //GamePlay Manager is a singleton, only allow one instance
            if (Instance == null) Instance = this;
            else if (Instance != this) Destroy(gameObject);
        }


        private void GetReferences()
        {
            AdditiveScenesControl = GetComponentInChildren<AdditiveScenesControl>();
            CommonUI = GetComponentInChildren<CommonUI>();
            PortalsManager = GetComponentInChildren<PortalsManager>();
            PoolSystem = GetComponentInChildren<PoolSystem>();
        }

        #region Variables

        [HideInInspector] private AdditiveScenesControl AdditiveScenesControl { get; set; }
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

        //Others

        [HideInInspector] public PoolSystem PoolSystem;

        #endregion
    }
}