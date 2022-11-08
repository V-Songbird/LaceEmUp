using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JamOff.Scripts.Managers
{
    public class GamePlayManager : MonoBehaviour
    {
        private static GamePlayManager _instance = null;
        public static GamePlayManager Instance => _instance;



        #region vars
        public AdditiveScenesControl AdditiveScenesControl { get; private set; }
        public CommonUI CommonUI { get; private set; }

        //Player
        [HideInInspector] public Player_MovementController Player_MovementController;
        [HideInInspector] public Player_Inventory Player_Inventory;

        #endregion

        private void Awake()
        {
            MakeItSingleton();
            GetReferences();

            Player_MovementController = FindObjectOfType<Player_MovementController>();
            Player_Inventory = FindObjectOfType<Player_Inventory>();
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
            CommonUI = GetComponentInChildren<CommonUI>();
        }
    }
}

