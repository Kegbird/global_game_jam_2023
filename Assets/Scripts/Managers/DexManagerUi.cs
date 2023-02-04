using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utility;
using TMPro;
namespace Managers
{
    public class DexManagerUi : MonoBehaviour
    {
        [SerializeField]
        private PickupScriptableObject  _test_seed;

        [SerializeField]
        private PickupScriptableObject  _seed1;
        [SerializeField]
        private PickupScriptableObject  _seed2;
        [SerializeField]
        private PickupScriptableObject  _seed3;
        [SerializeField]
        private PickupScriptableObject  _seed4;
        [SerializeField]
        private PickupScriptableObject  _seed5;

        [SerializeField]
        private TextMeshProUGUI  _index_counter;

        [SerializeField]
        private TextMeshProUGUI  _seed_name;

        [SerializeField]        
        private TextMeshProUGUI  _seed_basic_infos;

        [SerializeField]
        private TextMeshProUGUI  _seed_description;

        [SerializeField]
        private TextMeshProUGUI  _seed_usage_tip;

        [SerializeField]
        private Image  _seed_dex_sprite;

        private List<PickupScriptableObject> _seeds;

        HashSet<PickupEnum> _known_seeds;

        private int _current_index;
      

        

        private void Awake() {
            _seeds = new List<PickupScriptableObject>();
            _known_seeds =  new HashSet<PickupEnum>();
            _current_index = 1;

            //MOCK --
            AddSeed(_seed1);
            AddSeed(_seed2);
            AddSeed(_seed3);
            AddSeed(_seed4);
            AddSeed(_seed5);
            AddSeed(_seed5);
            AddSeed(_seed5);
            
            if (_seeds.Count > 0) {
                RefreshFocussedSeed();
            }
            //-- END MOCK
        }

        private void Start() {
            if (_seeds.Count > 0) DisplayDex();

            Debug.Log("Known Seeds: " + _known_seeds.Count);
            Debug.Log("Owned Seeds" + _seeds.Count);
        }

        private void Update() {
            if (_current_index < _known_seeds.Count - 1 && Input.GetKeyDown("up"))
            {
                _current_index += 1;
                RefreshFocussedSeed();
            }

            if ( _current_index > 0 && Input.GetKeyDown("down"))
            {
                _current_index -= 1;
                RefreshFocussedSeed();

            }
                
            }


        public void DisplayDex()
        {
            _current_index = 0;
            RefreshFocussedSeed();
        }

        void HideDex()
        {

        }

          
        void AddSeed(PickupScriptableObject seed=null) {
            if(! _known_seeds.Contains(seed._type)) {
                _seeds.Add(seed ? seed : _test_seed);
            }
            _known_seeds.Add(seed ? seed._type : _test_seed._type);
        }


        private void RefreshFocussedSeed() {
            _index_counter.SetText( $"{_current_index  + 1} / {_known_seeds.Count}");
            _seed_name.SetText(_seeds[_current_index]._name);
            _seed_description.SetText(_seeds[_current_index]._description);
            _seed_basic_infos.SetText(_seeds[_current_index]._basic_infos);
            _seed_dex_sprite.sprite = _seeds[_current_index]._dex_sprite;
        }
    }
}

