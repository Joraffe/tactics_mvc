using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tactics.Constants
{
    public class SpritesConstants : MonoBehaviour
    {
        public static SpritesConstants Instance;
        public TerraSprites terraSprites;
        public DangerOverlaySprites dangerOverlaySprites;
        public PathOverlaySprites pathOverlaySprites;
        public SelectOverlaySprites selectOverlaySprites;
        public TerraformOverlaySprites terraformOverlaySprites;
        public MoveOverlaySprites moveOverlaySprites;

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }
    }
}


