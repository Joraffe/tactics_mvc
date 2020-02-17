using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tactics.Models
{
    public class FormaSet : MonoBehaviour
    {
        public List<Forma> formas;

        public Forma GetActiveForma()
        {
            foreach(Forma forma in this.formas)
            {
                if (forma.active)
                {
                    return forma;
                }
            }

            return null;
        }
    }
}
