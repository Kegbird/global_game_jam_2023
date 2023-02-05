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
        private Image  _background_canvas;
        [SerializeField]
        private GameObject _index;
        [SerializeField]
        private GameObject _description;
        [SerializeField]
        private GameObject _info;
        [SerializeField]
        private GameObject _seed;


        [SerializeField]
        private TextMeshProUGUI  _index_counter;

        [SerializeField]
        private TextMeshProUGUI  _seed_name;

        [SerializeField]        
        private TextMeshProUGUI  _seed_basic_infos;

        [SerializeField]
        private TextMeshProUGUI  _seed_description;

        [SerializeField]
        private Image  _seed_dex_sprite;

        private List<PickupScriptableObject> _seeds;

        HashSet<PickupEnum> _known_seeds;

        private int _current_index;

        private bool displayed;

        private void Start()
        {
            _known_seeds = new HashSet<PickupEnum>();
            _seeds = new List<PickupScriptableObject>();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Tab) && !displayed && _known_seeds.Count>0)
            {
                displayed = true;
                DisplayDex();
            }
            else if(Input.GetKeyDown(KeyCode.Escape) && displayed)
            {
                displayed = false;
                HideDex();
            }

            if (_current_index < _known_seeds.Count - 1 && Input.GetKeyDown(KeyCode.DownArrow))
            {
                _current_index += 1;
                RefreshFocussedSeed();
            }

            if (_current_index > 0 && Input.GetKeyDown(KeyCode.UpArrow))
            {
                _current_index -= 1;
                RefreshFocussedSeed();

            }
        }

        public void DisplayDex()
        {
            _current_index = 0;
            RefreshFocussedSeed();
            _background_canvas.gameObject.SetActive(true);
            _index.gameObject.SetActive(true);
            _description.gameObject.SetActive(true);
            _info.gameObject.SetActive(true);
            _seed.gameObject.SetActive(true);
        }

        public void HideDex()
        {
            _background_canvas.gameObject.SetActive(false);
            _index.gameObject.SetActive(false);
            _description.gameObject.SetActive(false);
            _info.gameObject.SetActive(false);
            _seed.gameObject.SetActive(false);
        }
          
        public void AddSeed(PickupScriptableObject seed) {
            if (seed._type == PickupEnum.WATER || seed._type == PickupEnum.ENERGY)
                return;
            if (_known_seeds.Contains(seed._type)) {
                return;
            }
            _seeds.Add(seed);
            _known_seeds.Add(seed._type);
        }

        private void RefreshFocussedSeed() {
            _background_canvas.color = GetSeedTypeColor(_seeds[_current_index]);
            _index_counter.SetText( $"{_current_index  + 1} / {_known_seeds.Count}");
            _seed_name.SetText(_seeds[_current_index]._name);
            _seed_description.SetText(_seeds[_current_index]._description);
            _seed_basic_infos.SetText(_seeds[_current_index]._basic_infos);
            _seed_dex_sprite.sprite = _seeds[_current_index]._dex_sprite;
        }

        private Color32 GetSeedTypeColor(PickupScriptableObject seed)
        {
            switch (seed._type)
            {
                default:
                case PickupEnum.LAVANDULA_X: return new Color32(176,161,206, 230);
                case PickupEnum.CUCURBITA_X: return new Color32(176,161,206, 230);
                case PickupEnum.CORYLUS_X: return new Color32(176,161,206, 230);

                case PickupEnum.SNOWHEAP: return new Color32(196,229,248, 230);
                case PickupEnum.HAILTREE: return new Color32(196,229,248, 230);

                case PickupEnum.MEDUSA_FLYTRAP: return new Color32(155,205,160, 230);
                case PickupEnum.LION_FLOWER: return new Color32(155,205,160, 230);

                case PickupEnum.FERE_MOSS: return new Color32(218,176,115, 230);
                case PickupEnum.GEHENNA: return new Color32(218,176,115, 230);

                case PickupEnum.WHITELION: return new Color32(66,86,162, 230);
                case PickupEnum.GLACIPILA: return new Color32(66,86,162, 230);
                case PickupEnum.IRONGIANT: return new Color32(66,86,162, 230);

                case PickupEnum.CATNIP_2_0: return new Color32(41,148,81, 230);
                case PickupEnum.PITFALL_GOURD: return new Color32(41,148,81, 230);
                case PickupEnum.DRAGONBORN: return new Color32(41,148,81, 230);

                case PickupEnum.LIFE_HERB: return new Color32(113,81,36, 230);
                case PickupEnum.LIBRA_DE_FOCUS: return new Color32(113,81,36, 230);
                case PickupEnum.PYROPHITE: return new Color32(113,81,36, 230);

                case PickupEnum.HELLFLOWER: return new Color32(111,36,102, 230);
                case PickupEnum.GHIDORAH: return new Color32(111,36,102, 230);

                case PickupEnum.SACRED_LIFE: return new Color32(230,74,74, 230);
            }
        }
    }
}

