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

        #endregion

        private void Awake()
        {
            MakeItSingleton();
            GetReferences();
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
        }
    }
}

