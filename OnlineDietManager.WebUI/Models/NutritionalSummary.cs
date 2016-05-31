using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineDietManager.WebUI.Models
{
    public class NutritionalSummary
    {
        public string   PanelCaption    { get; set; }
        public float    Protein         { get; set; }
        public float    Fat             { get; set; }
        public float    Carbohydrates   { get; set; }
        public float    Caloricity      { get; set; }
        public float    Weight          { get; set; }
    }
}