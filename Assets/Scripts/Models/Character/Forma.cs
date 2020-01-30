
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tactics.Models
{
    public class FormaTile
    {
        public int relativeX;
        public int relativeY;
        public string terraType;
        public string teamName;
        public int auraAmount;

        public FormaTile(int relativeX, int relativeY, string terraType, string teamName, int auraAmount)
        {
            this.relativeX = relativeX;
            this.relativeY = relativeY;
            this.terraType = terraType;
            this.teamName = teamName;
            this.auraAmount = auraAmount;
        }
    }

    public class Forma : MonoBehaviour
    {
        public bool castable = false;
        public string teamName;

        public virtual int GetAuraAmount()
        {
             throw new NotImplementedException();
        }

        public virtual string GetTerraType()
        {
            throw new NotImplementedException();
        }

        public virtual List<FormaTile> GetUpFormaTiles()
        {
            throw new NotImplementedException();
        }

        public virtual List<FormaTile> GetDownFormaTiles()
        {
            throw new NotImplementedException();
        }

        public virtual List<FormaTile> GetLeftFormaTiles()
        {
            throw new NotImplementedException();
        }

        public virtual List<FormaTile> GetRightFormaTiles()
        {
            throw new NotImplementedException();
        }

        public void SetCastable()
        {
            this.castable = true;
        }

        public void ResetCastable()
        {
            this.castable = false;
        }
    }
}
