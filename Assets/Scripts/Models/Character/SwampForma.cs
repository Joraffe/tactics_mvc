using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tactics.Models
{
    public class SwampForma : Forma
    {

        public override List<FormaTile> GetUpFormaTiles()
        {
            List<FormaTile> formaTiles = new List<FormaTile>();
            formaTiles.Add(new FormaTile(0, 1, TerraTypes.Swamp));

            return formaTiles;
        }

        public override List<FormaTile> GetDownFormaTiles()
        {
            List<FormaTile> formaTiles = new List<FormaTile>();
            formaTiles.Add(new FormaTile(0, -1, TerraTypes.Swamp));

            return formaTiles;
        }

        public override List<FormaTile> GetLeftFormaTiles()
        {
            List<FormaTile> formaTiles = new List<FormaTile>();
            formaTiles.Add(new FormaTile(-1, 0, TerraTypes.Swamp));

            return formaTiles;
        }

        public override List<FormaTile> GetRightFormaTiles()
        {
            List<FormaTile> formaTiles = new List<FormaTile>();
            formaTiles.Add(new FormaTile(1, 0, TerraTypes.Swamp));

            return formaTiles;
        }
    }

}
