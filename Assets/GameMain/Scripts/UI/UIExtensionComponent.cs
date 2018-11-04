using GameFramework;
using GameFramework.ObjectPool;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameFramework.Entity;
using UnityEngine.UI;

namespace CollisionBall
{
    public class UIExtensionComponent : GameFrameworkComponent
    {
        [SerializeField]
        public Material defaultGreyMaterial;

        public void SetImageGrey(Image image, bool begrey)
        {
            if (begrey)
            {
                image.material = defaultGreyMaterial;
            }
            else
            {
                image.material = null;
            }
        }

        public void LoadUIComponent(GameObject subUI,Transform parent,string name = null) {
            Instantiate<GameObject>(subUI, parent);
            if (!string.IsNullOrEmpty(name))
            {
                Sprite img = Resources.Load<Sprite>("Image" + name);
                subUI.GetComponent<Image>().sprite = img;
            }
        }
    }
}
